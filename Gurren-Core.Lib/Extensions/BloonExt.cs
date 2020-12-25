using Assets.Scripts.Models.Bloons;
using Assets.Scripts.Simulation.Bloons;
using Gurren_Core.Api.BTD6;

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

        public static void RemoveBloonStatus(this Bloon bloon, bool removeCamo, bool removeFortify, bool removeRegrow)
        {
            bloon.bloonModel = _BloonModel.RemoveBloonStatus(bloon.bloonModel.name, removeCamo, removeFortify, removeRegrow);
        }
    }
}
