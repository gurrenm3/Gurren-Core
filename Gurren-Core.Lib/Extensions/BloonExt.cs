using Assets.Scripts.Models.Bloons;
using Assets.Scripts.Simulation.Bloons;
using Assets.Scripts.Unity;

namespace Gurren_Core.Extensions
{
    public static class BloonExt
    {
        public static string GetId(this Bloon bloon)
        {
            return bloon.bloonModel.id;
        }

        public static void SetBloonModel(this Bloon bloon, BloonModel model)
        {
            bloon.model = model;
        }

        public static float GetDamage(this Bloon bloon)
        {
            return bloon.GetModifiedTotalLeakDamage();
        }

        public static bool IsCamo(this Bloon bloon)
        {
            return bloon.bloonModel.IsCamo();
        }

        public static void SetCamo(this Bloon bloon, bool isCamo)
        {
            bloon.bloonModel.SetCamo(isCamo);
        }

        public static bool IsRegrow(this Bloon bloon)
        {
            return bloon.bloonModel.IsRegrow();
        }

        public static void SetRegrow(this Bloon bloon, bool isRegrow)
        {
            bloon.bloonModel.SetRegrow(isRegrow);
        }

        public static bool IsFortified(this Bloon bloon)
        {
            return bloon.bloonModel.IsFortified();
        }

        public static void SetFortified(this Bloon bloon, bool isFortified)
        {
            bloon.bloonModel.SetFortified(isFortified);
        }
    }
}
