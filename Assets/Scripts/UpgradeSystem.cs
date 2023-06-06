using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UpgradeSystem
{
   
    public static int CurrentCoins { get; set; }

    public static bool CanUpgrade(int cost,int currentMoney) 
    {
        return cost <= currentMoney;
    }

    public static void LevelUpgradeUp(ref LevelUpgrade levelUpgrade)
    {
        levelUpgrade += 1;
    }
}
    public enum LevelUpgrade
    {
        Low = 1,
        Medium = 2,
        High = 3
    }
