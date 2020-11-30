using Assets.Scripts.Unity.UI_New.Main;
using Harmony;

namespace Gurren_Core.Patches._MainMenu
{
    [HarmonyPatch(typeof(MainMenu), "OnEnable")]
    internal class MainMenuOnEnable_Patch
    {
        [HarmonyPostfix]
        internal static void Postfix()
        {
            var patch = new MainMenuOnEnable_Patch();
            patch.CheckForUpdates();
        }

        public void CheckForUpdates()
        {

        }
    }
}
