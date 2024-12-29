using BepInEx.Unity.IL2CPP;
using System;
using System.IO;
using System.IO.Pipes;
using System.Text;

namespace MitaSplit
{
    public static class Interprocess
    {
        private static NamedPipeServerStream _pipe;

        public static void Initialize()
        {
            try
            {
                _pipe = new NamedPipeServerStream("MitaSplit", PipeDirection.InOut, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous);
                Plugin.Log.LogInfo("Waiting for a client to connect to MitaSplit pipe...");
                _pipe.WaitForConnection();
                Plugin.Log.LogInfo("Client connected to MitaSplit pipe.");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogInfo($"Failed to initialize MitaSplit server pipe: {ex.Message}");
                _pipe = null;
            }
        }

        public static void Shutdown()
        {
            try
            {
                _pipe?.Close();
                _pipe?.Dispose();
                Plugin.Log.LogInfo("Closed MitaSplit server pipe.");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogInfo($"Error closing MitaSplit server pipe: {ex.Message}");
            }
            finally
            {
                _pipe = null;
            }
        }

        public static void WriteGameEnd()
        {
            WriteEvent(MessageType.Event, EventType.GameEnd);
        }

        public static void WriteMapChange(String map)
        {
            using (var ms = new MemoryStream())
            {
                using (var writer = new BinaryWriter(ms, Encoding.UTF8))
                {
                    writer.Write((byte)MessageType.Event);
                    writer.Write((byte)EventType.MapChange);

                    var mapBytes = Encoding.UTF8.GetBytes(map);
                    writer.Write(mapBytes.Length);
                    writer.Write(mapBytes);
                }
                WriteToPipe(ms.ToArray());
            }
        }

        public static void WriteTimerReset()
        {
            WriteEvent(MessageType.Event, EventType.TimerReset);
        }

        public static void WriteTimerStart()
        {
            WriteEvent(MessageType.Event, EventType.TimerStart);
        }

        public static void WriteILTimerStart()
        {
            WriteEvent(MessageType.Event, EventType.ILTimerStart);
        }

        private static void WriteEvent(MessageType messageType, EventType eventType)
        {
            using (var ms = new MemoryStream())
            {
                using (var writer = new BinaryWriter(ms, Encoding.UTF8))
                {
                    writer.Write((byte)messageType);
                    writer.Write((byte)eventType);
                }
                WriteToPipe(ms.ToArray());
            }
        }

        private static void WriteToPipe(byte[] data)
        {
            if (_pipe == null || !_pipe.IsConnected)
            {
                Plugin.Log.LogInfo("Pipe is not connected.");
                return;
            }

            try
            {
                _pipe.Write(data, 0, data.Length);
                _pipe.Flush();
            }
            catch (Exception ex)
            {
                Plugin.Log.LogInfo($"Failed to write to pipe: {ex.Message}");
            }
        }
    }

    public enum MessageType : byte
    {
        Event = 0
    }

    public enum EventType : byte
    {
        GameEnd = 1,
        MapChange = 2,
        TimerReset = 3,
        TimerStart = 4,
        ILTimerStart = 5
    }
}
