using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UpgradeSystem
{
   
    public static bool CanUpgrade(int cost,int currentMoney) 
    {
        return cost <= currentMoney;
    }




}
