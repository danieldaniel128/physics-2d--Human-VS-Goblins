using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeEntityEnum
{
    Knight,
    Catapult,
    Weapon,
    Miner
}
public class UpgradeData
{
    public Guid Upgrade_BTN_ID { get; set; }
    public int BTNIndex { get; set; }
    public LevelUpgradeEnum CurrentUpgradeLevel { get; set; }
    public UpgradeEntityEnum UpgradeEntity { get; set; }
    public int UpgradeCost { get; set; }


    public UpgradeData(int btnIndex, UpgradeEntityEnum upgradeEntity,int upgradeCost = 30) 
    {
        Upgrade_BTN_ID = Guid.NewGuid();
        BTNIndex = btnIndex;
        UpgradeCost = upgradeCost;
        CurrentUpgradeLevel = LevelUpgradeEnum.Low;
        UpgradeEntity = upgradeEntity;
    }

}