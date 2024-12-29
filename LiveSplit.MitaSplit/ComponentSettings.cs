using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Reflection;

namespace LiveSplit.MitaSplit
{
    public partial class ComponentSettings : UserControl
    {
        public ComponentSettings()
        {
            InitializeComponent();
        }

        private void SetDefaultSettings()
        {
            EnableAutoSplitCheckbox.Checked = true;
            EnableAutoResetCheckbox.Checked = true;
            EnableAutoStartCheckbox.Checked = true;
        }

        private static void AppendElement<T>(XmlDocument document, XmlElement parent, string name, T value)
        {
            XmlElement el = document.CreateElement(name);
            el.InnerText = value.ToString();
            parent.AppendChild(el);
        }

        public XmlNode GetSettings(XmlDocument document)
        {
            XmlElement settingsNode = document.CreateElement("Settings");

            AppendElement(document, settingsNode, "Version", Assembly.GetExecutingAssembly().GetName().Version);

            AppendElement(document, settingsNode, "EnableAutoSplit", EnableAutoSplitCheckbox.Checked);
            AppendElement(document, settingsNode, "EnableAutoReset", EnableAutoResetCheckbox.Checked);
            AppendElement(document, settingsNode, "EnableAutoStart", EnableAutoStartCheckbox.Checked);

            return settingsNode;
        }

        private bool FindSetting(XmlNode node, string name, bool previous)
        {
            var element = node[name];
            if (element == null)
                return previous;

            bool b;
            if (bool.TryParse(element.InnerText, out b))
                return b;

            return previous;
        }

        public void SetSettings(XmlNode settings)
        {
            SetDefaultSettings();
            SetAutoSplitCheckboxColors();

            var versionElement = settings["Version"];
            if (versionElement == null)
                return;
            Version ver;
            if (!Version.TryParse(versionElement.InnerText, out ver))
                return;
            if (ver.Major != 2 || ver.Minor != 0)
                return;

            EnableAutoSplitCheckbox.Checked = FindSetting(settings, "EnableAutoSplit", EnableAutoSplitCheckbox.Checked);
            EnableAutoResetCheckbox.Checked = FindSetting(settings, "EnableAutoReset", EnableAutoResetCheckbox.Checked);
            EnableAutoStartCheckbox.Checked = FindSetting(settings, "EnableAutoStart", EnableAutoStartCheckbox.Checked);

            SetAutoSplitCheckboxColors();
        }

        public bool ShouldSplitOn(string map)
        {
            if (!IsAutoSplitEnabled())
                return false;

            return false;
        }

        public bool ShouldSplitOnGameEnd()
        {
            return IsAutoSplitEnabled();
        }

        public bool ShouldSplitOnSecondGameEndTrigger()
        {
            return GameEndSecondTriggerCheckBox.Checked;
        }

        public bool IsAutoSplitEnabled()
        {
            return EnableAutoSplitCheckbox.Checked;
        }

        public bool IsAutoResetEnabled()
        {
            return EnableAutoResetCheckbox.Checked;
        }

        public bool IsAutoStartEnabled()
        {
            return EnableAutoStartCheckbox.Checked;
        }

        public bool IsILAutoStartEnabled()
        {
            return ILAutoStartCheckbox.Checked;
        }

        private void SetAutoSplitCheckboxColors()
        {

        }

        private void EnableAutoSplitCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            SetAutoSplitCheckboxColors();
        }
    }
}
