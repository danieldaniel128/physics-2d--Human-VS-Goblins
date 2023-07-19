using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting.Dependencies.NCalc;
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
        if((int)upgradeData.CurrentUpgradeLevel < 3)
            upgradeData.CurrentUpgradeLevel += 1;
        switch (upgradeData.UpgradeEntity)
        {
            case UpgradeEntityEnum.Weapon:
                // code block
                break;
            case UpgradeEntityEnum.Knight:
                // code block
                break;
            case UpgradeEntityEnum.Catapult:
                // code block
                break;
            case UpgradeEntityEnum.Miner:
                // code block
                break;
            default:
                // code block
                break;
        }
    }


}
    public enum LevelUpgradeEnum
    {
        Low = 1,
        Medium = 2,
        High = 3
    }
