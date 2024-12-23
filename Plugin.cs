using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using MitaSplit.Patches;
using UnityEngine;
namespace MitaSplit;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BasePlugin
{
    private static bool m_bPatchedUIText = false;
    internal static new ManualLogSource Log;

    private static System.Reflection.MethodInfo UIText_OnEnable;
    private static readonly Harmony harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);

    public override void Load()
    {
        // Plugin startup logic
        Log = base.Log;

        Log.LogInfo("Initializing interprocess...");
        Interprocess.Initialize();

        UIText_OnEnable = AccessTools.Method(typeof(Localization_UIText), "OnEnable");

        harmony.PatchAll(typeof(Plugin));
        harmony.PatchAll(typeof(SetActivePatch));
        harmony.PatchAll(typeof(WorldPatch));

        SceneLoad.RegisterEvent();

        Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
    }

    public static void PatchUIText()
    {
        if (m_bPatchedUIText)
            return;

        UIText_OnEnable = AccessTools.Method(typeof(UnityEngine.UI.Text), "OnEnable");
        harmony.PatchAll(typeof(UITextPatch));
        m_bPatchedUIText = true;
    }
        
    public static void UnpatchUIText()
    {
        if (!m_bPatchedUIText)
            return;

        harmony.Unpatch(UIText_OnEnable, HarmonyPatchType.Postfix);
        m_bPatchedUIText = false;
    }

    public override bool Unload()
    {
        Interprocess.Shutdown();
        return true;
    }
}
