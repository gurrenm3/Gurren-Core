using Assets.Scripts.Models.Bloons;
using Assets.Scripts.Unity;
using Gurren_Core.BTD6;
using System;

namespace Gurren_Core.Extensions
{
    public static class BloonModelExt
    {
        private static void ThrowIfBloonModelIsNull(BloonModel bloonModel, string exceptionMsg)
        {
            if (bloonModel == null)
                throw new ArgumentException(exceptionMsg, "bloonModel");
        }

        public static bool IsCamo(this BloonModel bloonModel)
        {
            ThrowIfBloonModelIsNull(bloonModel, "Failed to check if bloonModel is camo because it was null");

            return bloonModel.isCamo;
        }

        public static void SetCamo(this BloonModel bloonModel, bool isCamo)
        {
            ThrowIfBloonModelIsNull(bloonModel, "Failed to Set bloonModel to camo because it was null");

            bloonModel = _BloonModel.SetBloonStatus(bloonModel.name, isCamo, bloonModel.IsFortified(), bloonModel.IsRegrow());
        }

        public static bool IsRegrow(this BloonModel bloonModel)
        {
            ThrowIfBloonModelIsNull(bloonModel, "Failed to check if bloonModel is regrow because it was null");

            return bloonModel.isGrow;
        }

        public static void SetRegrow(this BloonModel bloonModel, bool isRegrow)
        {
            ThrowIfBloonModelIsNull(bloonModel, "Failed to set bloonModel to regrow because it was null");

            bloonModel = _BloonModel.SetBloonStatus(bloonModel.name, bloonModel.IsCamo(), bloonModel.IsFortified(), isRegrow);
        }

        public static bool IsFortified(this BloonModel bloonModel)
        {
            ThrowIfBloonModelIsNull(bloonModel, "Failed to check if bloonModel is fortified because it was null");

            return bloonModel.isFortified;
        }

        public static void SetFortified(this BloonModel bloonModel, bool isFortified)
        {
            ThrowIfBloonModelIsNull(bloonModel, "Failed to set bloonModel to fortified because it was null");

            bloonModel = _BloonModel.SetBloonStatus(bloonModel.name, bloonModel.IsCamo(), isFortified, bloonModel.IsRegrow());
        }

        public static BloonModel GetNextStrongest(this BloonModel bloonModel, bool allowCamo = false, bool allowFortified = false, bool allowRegrow = false)
        {
            ThrowIfBloonModelIsNull(bloonModel, "Failed to Get next strongest bloon because the base bloonModel was null");

            string bloonId = bloonModel.id;
            BloonModel nextBloonModel = null;
            var allBloonTypes = Game.instance.GetAllBloonModels();

            int max = allBloonTypes.Count - 1; // subtract 1 more here to avoid test bloon
            int currentBloonNum = _Bloon.GetBloonIdNum(bloonId);

            if (!allowCamo && !allowFortified && !allowRegrow)
            {
                string baseBloon = bloonId.Replace("Camo", "").Replace("Fortified", "").Replace("Regrow", "");
                for (int a = _Bloon.GetBloonIdNum(baseBloon); a < max; a++)
                {
                    if (allBloonTypes[a].name.Contains(baseBloon))
                        continue;

                    nextBloonModel = _BloonModel.RemoveBloonStatus(allBloonTypes[a].name, true, true, true);
                    break;
                }
            }
            else
            {
                nextBloonModel = _BloonModel.RemoveBloonStatus(allBloonTypes[currentBloonNum + 1].name, !allowCamo, !allowFortified, !allowRegrow);
            }

            return nextBloonModel;
        }
        public static BloonModel GetNextWeakest(this BloonModel bloonModel, bool allowCamo = false,
            bool allowFortified = false, bool allowRegrow = false)
        {
            ThrowIfBloonModelIsNull(bloonModel, "Failed to Get next weakest bloon because the base bloonModel was null");

            var allBloonTypes = Game.instance.GetAllBloonModels();

            string bloonId = bloonModel.id;
            string nextBloon = bloonId;
            int max = allBloonTypes.Count - 1; // subtract 1 more here to avoid test bloon
            for (int i = 0; i < max; i++)
            {
                if (bloonId.ToLower() != allBloonTypes[i].name.ToLower())
                    continue;

                if (i == 0)
                {
                    nextBloon = allBloonTypes[0].name;
                    break;
                }
                nextBloon = allBloonTypes[i - 1].name;
                break;
            }

            var nextWeakestBloon = _BloonModel.SetBloonStatus(nextBloon, allowCamo, allowFortified, allowRegrow);
            return nextWeakestBloon;
        }
    }
}
