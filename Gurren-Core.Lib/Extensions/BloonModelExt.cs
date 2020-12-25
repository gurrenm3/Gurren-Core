using Assets.Scripts.Models.Bloons;
using Assets.Scripts.Unity;
using Gurren_Core.Api.BTD6;
using MelonLoader;

namespace Gurren_Core.Extensions
{
    public static class BloonModelExt
    {

        public static void SetBloonModel(this BloonModel bloonModel, bool isCamo, bool isFortified, bool isRegrow)
        {
            bloonModel = _BloonModel.SetBloonStatus(bloonModel.name, isCamo, isFortified, isRegrow);
        }

        public static void SetCamo(this BloonModel bloonModel, bool isCamo)
        {
            bloonModel = _BloonModel.SetBloonStatus(bloonModel.name, isCamo, bloonModel.isFortified, bloonModel.isGrow);
        }

        public static void SetFortified(this BloonModel bloonModel, bool isFortified)
        {
            bloonModel = _BloonModel.SetBloonStatus(bloonModel.name, bloonModel.isCamo, isFortified, bloonModel.isGrow);
        }

        public static void SetRegrow(this BloonModel bloonModel, bool isRegrow)
        {
            bloonModel = _BloonModel.SetBloonStatus(bloonModel.name, bloonModel.isCamo, bloonModel.isFortified, isRegrow);
        }

        public static void RemoveBloonStatus(this BloonModel bloonModel, bool removeCamo, bool removeFortify, bool removeRegrow)
        {
            bloonModel = _BloonModel.RemoveBloonStatus(bloonModel.name, removeCamo, removeFortify, removeRegrow);
        }

        public static int GetBloonIdNum(this BloonModel bloonModel)
        {
            var allBloons = Game.instance.model.bloons;
            for (int i = 0; i < allBloons.Count; i++)
            {
                if (allBloons[i].name.ToLower() != bloonModel.name.ToLower())
                    continue;
                return i;
            }
            return -1;
        }


        public static BloonModel GetNextStrongest(this BloonModel bloonModel, bool allowCamo = false, bool allowFortified = false, bool allowRegrow = false)
        {
            var allBloonTypes = Game.instance.GetAllBloonModels();

            int max = allBloonTypes.Count - 1; // subtract 1 more here to avoid test bloon
            int currentBloonNum = bloonModel.GetBloonIdNum();
            BloonModel nextBloonModel = null;

            if (allowCamo || allowFortified || allowRegrow)
            {
                nextBloonModel = _BloonModel.RemoveBloonStatus(allBloonTypes[currentBloonNum + 1].name, !allowCamo, !allowFortified, !allowRegrow);
            }
            else
            {
                string baseBloon = bloonModel.id.Replace("Camo", "").Replace("Fortified", "").Replace("Regrow", "");
                for (int a = bloonModel.GetBloonIdNum(); a < max; a++)
                {
                    if (allBloonTypes[a].name.Contains(baseBloon))
                        continue;

                    nextBloonModel = _BloonModel.RemoveBloonStatus(allBloonTypes[a].name, true, true, true);
                    break;
                }
            }

            return nextBloonModel;
        }
        public static BloonModel GetNextWeakest(this BloonModel bloonModel, bool allowCamo = false,
            bool allowFortified = false, bool allowRegrow = false)
        {
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
