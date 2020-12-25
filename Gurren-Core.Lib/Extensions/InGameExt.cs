using Assets.Scripts.Models;
using Assets.Scripts.Models.Rounds;
using Assets.Scripts.Simulation.Towers.Projectiles;
using Assets.Scripts.Unity;
using Assets.Scripts.Unity.Bridge;
using Assets.Scripts.Unity.UI_New.InGame;
using Il2CppSystem.Collections.Generic;
using MelonLoader;
using System;
using UnhollowerBaseLib;
using static Assets.Scripts.Simulation.Simulation;

namespace Gurren_Core.Extensions
{
    public static class InGameExt
    {
        public static double GetCash(this InGame inGame)
        {
            CashManager cashManager = inGame.bridge.simulation.cashManagers.entries[0].value;
            return cashManager.cash.Value;
        }

        public static void AddCash(this InGame inGame, double newCash)
        {
            CashManager cashManager = inGame.bridge.simulation.cashManagers.entries[0].value;
            cashManager.cash.Value += newCash;
        }

        public static void SetCash(this InGame inGame, double newCash)
        {
            CashManager cashManager = inGame.bridge.simulation.cashManagers.entries[0].value;
            cashManager.cash.Value = newCash;
        }

        public static double GetHealth(this InGame inGame)
        {
            return inGame.bridge.simulation.health.Value;
        }

        public static void AddHealth(this InGame inGame, double newHealth)
        {
            inGame.bridge.simulation.health.Value += newHealth;
        }

        public static void SetHealth(this InGame inGame, double newHealth)
        {
            inGame.bridge.simulation.health.Value = newHealth;
        }

        public static List<BloonToSimulation> GetBloons(this InGame inGame)
        {
            return inGame.bridge.GetAllBloons();
        }

        public static void SpawnBloons(this InGame inGame, Il2CppReferenceArray<BloonEmissionModel> bloonEmissionModels)
        {
            int round = InGame.instance.bridge.GetCurrentRound();
            inGame.bridge.SpawnBloons(bloonEmissionModels, round, 0);
        }

        public static void SpawnBloons(this InGame inGame, string bloonName, float spacing, int number)
        {
            Il2CppReferenceArray<BloonEmissionModel> bloonEmissionModels = new Il2CppReferenceArray<BloonEmissionModel>(number);
            float time = 0;
            for (int i = 0; i < number; i++)
            {
                time += spacing;
                bloonEmissionModels[i] = (new BloonEmissionModel(bloonName, time, bloonName));
            }

            inGame.SpawnBloons(bloonEmissionModels);
        }

        public static void SpawnBloons(this InGame inGame, int round)
        {
            GameModel model = Game.instance.model;
            if (round < 100)
            {
                var rounds = model.GetRoundSet().rounds;
                var emissions = rounds[round - 1].emissions;
                inGame.SpawnBloons(emissions);
            }
            if (round > 100)
            {
                var emissions = model.freeplayGroups[round - 100].bloonEmissions;
                inGame.SpawnBloons(emissions);
            }
        }

        public static List<TowerToSimulation> GetTowers(this InGame inGame)
        {
            return inGame.bridge.GetAllTowers();
        }

        public static List<AbilityToSimulation> GetAbilities(this InGame inGame)
        {
            return inGame.bridge.GetAllAbilities(true);
        }

        public static List<Projectile> GetProjectiles(this InGame inGame)
        {
            return inGame.bridge.GetAllProjectiles();
        }
    }
}
