using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UpgradeSystem
{
    static int _currentCoins;
    public static int CurrentCoins 
    {
        get => _currentCoins;
        set
        {
            _currentCoins = value;
            UIManager.Instance.UpdateCoinsTextUI(_currentCoins);
        } 
    }

    public static bool CanUpgrade(int cost,int currentMoney) 
    {
        return cost <= currentMoney;
    }

    public static void LevelUpgradeUp(ref LevelUpgrade levelUpgrade)
    {
        levelUpgrade += 1;
    }

    public static void UpgradeTheLevelUpgrade(this LevelUpgrade levelUpgrade)
    {
        if((int)levelUpgrade<3)
            levelUpgrade += 1;
    }


}
    public enum LevelUpgrade
    {
        Low = 1,
        Medium = 2,
        High = 3
    }
