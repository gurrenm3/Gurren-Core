using Assets.Scripts.Unity;
using MelonLoader;
using System.Runtime.InteropServices;
using Gurren_Core.Extensions;
using Assets.Scripts.Models.Bloons;

namespace Gurren_Core.BTD6
{
    public class _BloonModel
    {
        public static BloonModel SetBloonStatus(string bloonId, [Optional] bool setCamo, [Optional] bool setFortified, [Optional] bool setRegrow)
        {
            string camoText = "";
            string fortifiedText = "";
            string regrowText = "";
            string bloonBase = bloonId.Replace("Camo", "").Replace("Fortified", "").Replace("Regrow", "");

            if (setCamo)
            {
                if (Game.instance.GetBloonModel(bloonBase + "Camo") != null)
                    camoText = "Camo";
            }
            if (setFortified)
            {
                if (Game.instance.GetBloonModel(bloonBase + "Fortified") != null)
                    fortifiedText = "Fortified";
            }
            if (setRegrow)
            {
                if (Game.instance.GetBloonModel(bloonBase + "Regrow") != null)
                    regrowText = "Regrow";
            }

            string newBloonID = bloonBase + regrowText + fortifiedText + camoText;
            var newBloon = Game.instance.GetBloonModel(newBloonID);
            return newBloon;
        }


        public static BloonModel RemoveBloonStatus(string bloonId, bool removeCamo, bool removeFortified, bool removeRegrow, bool ignoreException = true)
        {
            if (bloonId.Contains("Camo") && removeCamo)
                bloonId = bloonId.Replace("Camo", "");
            if (bloonId.Contains("Fortified") && removeFortified)
                bloonId = bloonId.Replace("Fortified", "");
            if (bloonId.Contains("Regrow") && removeRegrow)
                bloonId = bloonId.Replace("Regrow", "");

            var bloon = Game.instance.GetBloonModel(bloonId);
            if (bloon == null)
            {
                if (!ignoreException)
                    MelonLogger.Log("Failed to remove status from bloon. It returned null");
                return null;
            }

            return bloon;
        }
    }
}
