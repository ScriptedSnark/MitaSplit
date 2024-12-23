using Il2CppSystem.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MitaSplit
{
    public class SceneLoad
    {
        public static bool m_bSwitchedScene = false;
        public static String m_sSceneName = null;
        public static List<GameObject> m_rootGameObjects = null;

        public static void RegisterEvent()
        {
            m_rootGameObjects = new List<GameObject>();
            SceneManager.activeSceneChanged += (UnityEngine.Events.UnityAction<Scene, Scene>)OnSceneLoaded;
        }

        private static void OnSceneLoaded(Scene current, Scene next)
        {
            m_bSwitchedScene = true;
            m_sSceneName = next.name;

            if (next.name == "Scene 9 - ChibiMita") // SPECIFIC LOGIC
            {
                Interprocess.WriteMapChange(next.name);
            }

            if (next.name == "Scene 1 - RealRoom") // SPECIFIC LOGIC
            {
                Plugin.PatchUIText();
            }
            else
            {
                Plugin.UnpatchUIText();
            }

            //Plugin.Log.LogInfo($"{next.name}");
        }
    }
}
