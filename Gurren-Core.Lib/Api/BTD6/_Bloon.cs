using Assets.Scripts.Unity;

namespace Gurren_Core.Api.BTD6
{
    public class _Bloon
    {

        /// <summary>
        /// Get the number ID of the bloon. Mainly used to get the numeric position of bloon in the list of DefaultBloonIds
        /// </summary>
        /// <param name="bloonId">bloon name of bloon you want Id for</param>
        /// <returns></returns>
        public static int GetBloonIdNum(string bloonId)
        {
            var allBloons = Game.instance.model.bloons;
            for (int i = 0; i < allBloons.Count; i++)
            {
                if (allBloons[i].name.ToLower() != bloonId.ToLower())
                    continue;
                return i;
            }
            return -1;
        }
    }
}