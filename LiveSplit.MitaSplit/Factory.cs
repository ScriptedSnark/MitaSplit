using System;
using System.Reflection;
using LiveSplit.Model;
using LiveSplit.UI.Components;

[assembly: ComponentFactory(typeof(LiveSplit.MitaSplit.Factory))]

namespace LiveSplit.MitaSplit
{
    public class Factory : IComponentFactory
    {
        public ComponentCategory Category => ComponentCategory.Control;
        public string ComponentName => "MitaSplit";
        public string Description => "Auto-Split component that works with BepInEx plugin - MitaSplit.";

        public string UpdateName => ComponentName;
        public string UpdateURL => "";
        public Version Version => Assembly.GetExecutingAssembly().GetName().Version;
        public string XMLURL => "";

        public IComponent Create(LiveSplitState state) => new Component(state);
    }
}
