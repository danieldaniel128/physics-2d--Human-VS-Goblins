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
            CheckForUpgrades();
        } 
    }
    private static void CheckForUpgrades()
    {
        foreach (UpgradeData upgradeData in UIManager.Instance.UpgradeDatas)
            if (upgradeData.CurrentUpgradeLevel != LevelUpgradeEnum.High && CanUpgrade(upgradeData.UpgradeCost))
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
        if (CurrentCoins < 0)
            CurrentCoins = 0;
        UIManager.Instance.UpgradeDatas[BtnIndex].UpgradeTheLevelUpgrade();
        //LevelUpgradeUp
    }

    //public static void LevelUpgradeUp(ref LevelUpgradeEnum levelUpgrade)
    //{
    //    levelUpgrade += 1;
    //}

    public static void UpgradeTheLevelUpgrade(this UpgradeData upgradeData)
    {
        if((int)upgradeData.CurrentUpgradeLevel <= 3)
            upgradeData.CurrentUpgradeLevel += 1;
        UpgradeLevelCost(upgradeData);
    }
    public static void UpgradeLevelCost(UpgradeData upgradeData)
    {
        upgradeData.UpgradeCost += 5 * (int)upgradeData.CurrentUpgradeLevel;
    }


}


    public enum LevelUpgradeEnum
    {
        Low = 1,
        Medium = 2,
        High = 3
    }
