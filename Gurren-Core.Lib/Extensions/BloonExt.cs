using Assets.Scripts.Models.Bloons;
using Assets.Scripts.Simulation.Bloons;

namespace Gurren_Core.Extensions
{
    public static class BloonExt
    {
        public static void SetBloonModel(this Bloon bloon, BloonModel model)
        {
            bloon.model = model;
        }

        public static float GetDamage(this Bloon bloon)
        {
            return bloon.GetModifiedTotalLeakDamage();
        }
    }
}
