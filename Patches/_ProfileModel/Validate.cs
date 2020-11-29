using Assets.Scripts.Models.Profile;
using Harmony;

namespace Gurren_Core.Patches._ProfileModel
{
    [HarmonyPatch(typeof(ProfileModel), "Validate")]
    internal class Patch
    {
        internal static ProfileModel profileModel;
        [HarmonyPostfix]
        internal static void Postfix(ProfileModel __instance)
        {
            profileModel = __instance;
        }
    }
}
