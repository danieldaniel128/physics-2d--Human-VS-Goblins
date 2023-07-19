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
            IsUpgradeable();
        } 
    }
    private static void IsUpgradeable()
    {
        foreach (UpgradeData upgradeData in UIManager.Instance.UpgradeDatas)
            if (CanUpgrade(upgradeData.UpgradeCost))
            {
                UIManager.Instance.SwitchBTNInteractToOn(upgradeData.BTNIndex);
            }
            else
            {
                UIManager.Instance.SwitchBTNInteractToOff(upgradeData.BTNIndex);
            }
    }
    public static bool CanUpgrade(int cost) 
    {
        return cost <= CurrentCoins;
    }

    public static void BuyUpgrade(int BtnIndex) 
    {
        CurrentCoins -= UIManager.Instance.UpgradeDatas[BtnIndex].UpgradeCost;
        //LevelUpgradeUp
    }

    public static void LevelUpgradeUp(ref LevelUpgradeEnum levelUpgrade)
    {
        levelUpgrade += 1;
    }

    public static void UpgradeTheLevelUpgrade(this LevelUpgradeEnum levelUpgrade)
    {
        if((int)levelUpgrade<3)
            levelUpgrade += 1;
    }


}
    public enum LevelUpgradeEnum
    {
        Low = 1,
        Medium = 2,
        High = 3
    }
