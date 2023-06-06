using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeData
{
    Guid Upgrade_BTN_ID { get; set; }
    LevelUpgrade CurrentUpgradeLevel { get; set; }
    int UpgradeCost { get; set; }


    public UpgradeData() 
    {
        Upgrade_BTN_ID = Guid.NewGuid();
        UpgradeCost = 0;
        CurrentUpgradeLevel = LevelUpgrade.Low;
    }

}