using Assets.Scripts.Models.Bloons;
using Assets.Scripts.Models.Profile;
using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Upgrades;
using Assets.Scripts.Unity;
using Assets.Scripts.Unity.Player;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Gurren_Core.Extensions
{
    public static class GameExt
    {
        public static ProfileModel GetProfileModel(this Game game) => Patches._ProfileModel.Patch.profileModel;
        public static Btd6Player GetBtd6Player(this Game game) => Patches._BTD6Player.Patch.playerModel;

        public static double GetMonkeyMoney(this Game game)
        {
            return game.playerService.Player.Data.monkeyMoney.Value;
        }

        public static void SetMonkeyMoney(this Game game, double newMM)
        {
            game.playerService.Player.Data.monkeyMoney.Value = newMM;
        }

        public static BloonModel GetBloonModel(this Game game, string bloonName)
        {
            return game.model.GetBloon(bloonName);
        }

        public static List<BloonModel> GetAllBloonModels(this Game game)
        {
            return game.model.bloons.ToList<BloonModel>();
        }

        public static TowerModel GetTowerModel(this Game game, string towerName, [Optional] int pathOneUpgrade, 
            [Optional] int pathTwoUpgrade, [Optional] int pathThreeUpgrade)
        {
            return game.model.GetTower(towerName, pathOneUpgrade, pathTwoUpgrade, pathThreeUpgrade);
        }

        public static List<TowerModel> GetAllTowerModels(this Game game)
        {
            return game.model.towers.ToList<TowerModel>();
        }

        public static UpgradeModel GetUpgradeModel(this Game game, string upgradeName)
        {
            return game.model.GetUpgrade(upgradeName);
        }
    }
}
