using Assets.Scripts.Models.Towers;
using Assets.Scripts.Unity;
using System.Runtime.InteropServices;

namespace Gurren_Core.Api.BTD6
{
    public class _TowerModel
    {
        public static TowerModel GetTower(TowerType baseId, [Optional]int tier1, [Optional]int tier2, [Optional]int tier3)
            => GetTower(baseId.ToString(), tier1, tier2, tier3);

        public static TowerModel GetTower(string baseId, [Optional]int tier1, [Optional]int tier2, [Optional]int tier3)
            => Game.instance.model.GetTower(baseId, tier1, tier2, tier3);

    }
}
