using System.Windows.Forms;
using System.Xml;

using LiveSplit.Model;
using LiveSplit.UI;
using LiveSplit.UI.Components;
using System.IO.Pipes;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LiveSplit.MitaSplit
{
    class Component : LogicComponent
    {
        public override string ComponentName => "MitaSplit";
        private const string PIPE_NAME = "MitaSplit";
        public enum MessageType : byte
        {
            Event = 0
        }

        public enum EventType : byte
        {
            GameEnd = 1,
            MapChange = 2,
            TimerReset = 3,
            TimerStart = 4
        }

        private interface IEvent
        {
            TimeSpan Time { get; }
        }
        private struct GameEndEvent : IEvent
        {
            public TimeSpan Time { get; set; }
        }
        private struct MapChangeEvent : IEvent
        {
            public TimeSpan Time { get; set; }
            public string Map { get; set; }
        }
        private struct TimerResetEvent : IEvent
        {
            public TimeSpan Time { get; set; }
        }
        private struct TimerStartEvent : IEvent
        {
            public TimeSpan Time { get; set; }
        }

        private ComponentSettings settings = new ComponentSettings();
        private NamedPipeClientStream pipe = new NamedPipeClientStream(
          ".",
          PIPE_NAME,
          PipeDirection.InOut,
          PipeOptions.None,
          System.Security.Principal.TokenImpersonationLevel.None,
          System.IO.HandleInheritability.None);
        private CancellationTokenSource cts = new CancellationTokenSource();
        private Thread pipeThread;
        private TimerModel model;
        private TimeSpan currentTime = new TimeSpan();
        private object currentTimeLock = new object();
        private List<IEvent> events = new List<IEvent>();
        private object eventsLock = new object();
        private HashSet<string> visitedMaps = new HashSet<string>();

        private DateTime lastTime = DateTime.Now;

        public Component(LiveSplitState state)
        {
            state.OnStart += OnStart;
            model = new TimerModel() { CurrentState = state };
            pipeThread = new Thread(PipeThreadFunc);
            pipeThread.Start();
        }

        public override void Dispose()
        {
            cts.Cancel();
            pipeThread.Join();
        }


        public override XmlNode GetSettings(XmlDocument document)
        {
            return settings.GetSettings(document);
        }

        public override Control GetSettingsControl(LayoutMode mode)
        {
            return settings;
        }

        public override void SetSettings(XmlNode settings)
        {
            this.settings.SetSettings(settings);
        }

        public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
        {
            state.IsGameTimePaused = true;

            bool start = false;
            lock (eventsLock)
            {
                foreach (var ev in events)
                {
                    if (ev is GameEndEvent)
                    {
                        if (state.CurrentPhase == TimerPhase.Running && settings.ShouldSplitOnGameEnd())
                        {
                            model.Split();
                        }
                    }
                    else if (ev is MapChangeEvent)
                    {
                        var e = (MapChangeEvent)ev;
                        if (visitedMaps.Add(e.Map) && settings.ShouldSplitOnGameEnd())
                        {
                            model.Split();
                        }
                    }
                    else if (ev is TimerResetEvent)
                    {
                        if (settings.IsAutoResetEnabled())
                        {
                            model.Reset();
                        }
                    }
                    else if (ev is TimerStartEvent)
                    {
                        if (settings.IsAutoStartEnabled())
                        {
                            state.SetGameTime(ev.Time);
                            start = true;
                        }
                    }

                }
                events.Clear();
            }
            if (start)
                model.Start();

            TimeSpan curTime;
            lock (currentTimeLock)
            {
                curTime = currentTime;
            }
            state.SetGameTime(curTime);
        }

        private void OnStart(object sender, EventArgs e)
        {
            lock (eventsLock)
            {
                events.Clear();
            }

            visitedMaps.Clear();
        }

        private TimeSpan ParseTime(byte[] buf, int offset)
        {
            var hours = BitConverter.ToUInt32(buf, offset);
            var minutes = buf[offset + 4];
            var seconds = buf[offset + 5];
            var milliseconds = BitConverter.ToUInt16(buf, offset + 6);
            return new TimeSpan((int)(hours / 24), (int)(hours % 24u), minutes, seconds, milliseconds);
        }

        private void ParseMessage(byte[] buf)
        {
            switch (buf[0])
            {
                case (byte)MessageType.Event:
                    {
                        var eventType = buf[1];

                        switch (eventType)
                        {
                            case (byte)EventType.GameEnd:
                                {
                                    var ev = new GameEndEvent() { };
                                    lock (eventsLock)
                                    {
                                        events.Add(ev);
                                    }
                                    //Debug.WriteLine("Received a timer reset event: {0}:{1}:{2}.{3}.", time.Hours, time.Minutes, time.Seconds, time.Milliseconds);
                                }
                                break;

                            case (byte)EventType.MapChange:
                                {
                                    var len = BitConverter.ToInt32(buf, 2); // Длина строки начинается с buf[2]
                                    string map = System.Text.Encoding.ASCII.GetString(buf, 6, len); // Строка начинается с buf[6]
                                    var ev = new MapChangeEvent { Map = map };
                                    lock (eventsLock)
                                    {
                                        events.Add(ev);
                                    }
                                }
                                break;

                            case (byte)EventType.TimerReset:
                                {
                                    var ev = new TimerResetEvent() {  };
                                    lock (eventsLock)
                                    {
                                        events.Add(ev);
                                    }
                                    //Debug.WriteLine("Received a timer reset event: {0}:{1}:{2}.{3}.", time.Hours, time.Minutes, time.Seconds, time.Milliseconds);
                                }
                                break;

                            case (byte)EventType.TimerStart:
                                {
                                    var ev = new TimerStartEvent() {  };
                                    lock (eventsLock)
                                    {
                                        events.Add(ev);
                                    }
                                    //Debug.WriteLine("Received a timer start event: {0}:{1}:{2}.{3}.", time.Hours, time.Minutes, time.Seconds, time.Milliseconds);
                                }
                                break;

                            default:
                                Debug.WriteLine("Received an unknown event type: " + buf[2]);
                                break;
                        }
                    }
                    break;

                default:
                    Debug.WriteLine("Received an unknown message type: " + buf[0]);
                    break;
            }
        }

        private void PipeThreadFunc()
        {
            while (!cts.IsCancellationRequested)
            {
                if (!pipe.IsConnected)
                {
                    Debug.WriteLine("Connecting to the pipe.");
                    try
                    {
                        pipe.Connect(0);
                        Debug.WriteLine("Connected to the pipe. Readmode: " + pipe.ReadMode);
                        pipe.ReadMode = PipeTransmissionMode.Message;
                        Debug.WriteLine("Set the message read mode.");
                    }
                    catch (Exception e) when (e is TimeoutException || e is IOException)
                    {
                        Debug.WriteLine("Idling for 1 second.");
                        cts.Token.WaitHandle.WaitOne(TimeSpan.FromSeconds(1));
                        continue;
                    }
                }

                // Connected to the pipe.
                try
                {
                    var buf = new byte[256];
                    var task = pipe.ReadAsync(buf, 0, 256, cts.Token);
                    task.Wait();

                    if (task.Result == 0)
                    {
                        // The pipe was closed.
                        Debug.WriteLine("Pipe end of stream reached.");
                        continue;
                    }

                    ParseMessage(buf);
                }
                catch (AggregateException e)
                {
                    foreach (var ex in e.InnerExceptions)
                    {
                        if (ex is TaskCanceledException)
                        {
                            return;
                        }
                    }

                    Debug.WriteLine("Error reading from the pipe:");
                    foreach (var ex in e.InnerExceptions)
                    {
                        Debug.WriteLine("- " + ex.GetType().Name + ": " + ex.Message);
                    }
                    Debug.WriteLine("Idling for 1 second.");
                    cts.Token.WaitHandle.WaitOne(TimeSpan.FromSeconds(1));
                    continue;
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Error reading from the pipe: " + e.Message);
                    Debug.WriteLine("Idling for 1 second.");
                    cts.Token.WaitHandle.WaitOne(TimeSpan.FromSeconds(1));
                    continue;
                }
            }
        }
    }
}