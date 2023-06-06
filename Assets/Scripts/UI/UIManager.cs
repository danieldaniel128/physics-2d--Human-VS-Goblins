using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField] Image CastleHealthBarImage;

    public static UIManager Instance;

    public event Action< Image, float, float> UpdateHealthBarImageEvent;
    [SerializeField] private int _weaponCost, _catapultCost, _knightCost, _minerCost;
    [SerializeField] private int _weaponUpdateLevel , _catapultUpdateLevel, _knightUpdateLevel, _minerUpdateLevel;


    [SerializeField] private TextMeshProUGUI Coins_TXT;

    public void UpdateCoinsTextUI(int coins)
    {
        Coins_TXT.text = $"Coins: {coins}";
    }


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }


    


    public void InvokeUpdateHealthBar(Image image, float newHealth , float maxHealth)
    {
        UpdateHealthBarImageEvent?.Invoke(image,newHealth, maxHealth);
    }
    public void UpdateHealthBarUI(Image healthBar, float newHealth, float maxHealth) 
    {
        healthBar.fillAmount = newHealth/maxHealth;
    }


    public void UpgradeWeapon() 
    {
        UpgradeSystem.CanUpgrade(_weaponCost,0);
    }
    public void UpgradeCatapolt()
    {
        UpgradeSystem.CanUpgrade(_catapultCost,0);
    }
    public void UpgradeKnight()
    {
        UpgradeSystem.CanUpgrade(_knightCost,0);
    }
    public void UpgradeMiner()
    {
        UpgradeSystem.CanUpgrade(_knightCost,0);
    }

}
