using HarmonyLib;
using Il2CppSystem.Reflection;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx.Unity.IL2CPP.UnityEngine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MitaSplit.Patches
{
    // TODO: fix potential crashes here (REMOVE UNPATCH/PATCH METHODS FOR UITEXT)
    [HarmonyPatch(typeof(Localization_UIText))]
    [HarmonyPatch("OnEnable")]
    public class UITextPatch
    {
        static void Postfix(Localization_UIText __instance)
        {
            if (__instance.NameFile == String.Empty)
                return;

            if (__instance.NameFile == "Location 1" && __instance.StringNumber == 28) // Find TextPresent
            {
                Interprocess.WriteTimerReset();
                Interprocess.WriteTimerStart();
            }
        }
    }


    [HarmonyPatch(typeof(GameObject))]
    [HarmonyPatch("SetActive")]
    public class SetActivePatch
    {
        static void Postfix(GameObject __instance, bool value)
        {
            if (value && __instance.name == "CutSceneEnding")
            {
                Plugin.Log.LogInfo($"GameObject '{__instance.name}' has been activated.");
                Interprocess.WriteGameEnd();
            }
        }
    }

    [HarmonyPatch(typeof(World))]
    public class WorldPatch
    {
        [HarmonyPatch("Update")]
        [HarmonyPrefix]
        static void UpdatePatch()
        {
            if (SceneLoad.m_bSwitchedScene)
            {
                SceneManager.GetActiveScene().GetRootGameObjects(SceneLoad.m_rootGameObjects);

                //GameObject[] objects = UnityEngine.Object.FindObjectsOfType<GameObject>(true);
                foreach (GameObject obj in SceneLoad.m_rootGameObjects)
                {
                    if (obj.name.StartsWith("InterfaceSave", StringComparison.Ordinal))
                    {
                        Interprocess.WriteMapChange(SceneLoad.m_sSceneName);
                        SceneLoad.m_bSwitchedScene = false;
                    }
                }
            }
        }
    }
}