using Il2CppSystem.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MitaSplit
{
    public class SceneLoad
    {
        public static bool m_bSwitchedScene = false;
        public static String m_sOldSceneName = null;
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
            m_sOldSceneName = m_sSceneName;
            m_sSceneName = next.name;

            if (m_sOldSceneName == null)
                return;

            if (m_sOldSceneName.Equals("SceneLoading", StringComparison.Ordinal) && !next.name.Equals("SceneMenu", StringComparison.Ordinal))
            {
                Interprocess.WriteILTimerStart();
            }

            if (next.name.Equals("Scene 9 - ChibiMita", StringComparison.Ordinal))
            {
                Interprocess.WriteMapChange(next.name);
            }

            if (next.name.Equals("Scene 1 - RealRoom", StringComparison.Ordinal)) // SPECIFIC LOGIC
            {
                Plugin.PatchUIText();
            }
            else
            {
                Plugin.UnpatchUIText();
            }
        }
    }
}
