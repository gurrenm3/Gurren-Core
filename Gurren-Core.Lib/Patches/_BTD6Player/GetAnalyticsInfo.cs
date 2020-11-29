using Harmony;
using Assets.Scripts.Unity.Player;

namespace Gurren_Core.Patches._BTD6Player
{
    [HarmonyPatch(typeof(Btd6Player), "GetAnalyticsInfo")]
    internal class Patch
    {
        internal static Btd6Player playerModel;
        [HarmonyPostfix]
        internal static void Postfix(Btd6Player __instance)
        {
            playerModel = __instance;
        }
    }
}
