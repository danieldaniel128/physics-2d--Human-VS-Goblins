using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{

    public static UIManager Instance;
    public event Action< Image, float, float> UpdateHealthBarImageEvent;
    public List<UpgradeData> UpgradeDatas;

    [SerializeField] Image CastleHealthBarImage;
    
    [SerializeField] private int _weaponCost, _catapultCost, _knightCost, _minerCost;
    [SerializeField] private int _weaponUpdateLevel , _catapultUpdateLevel, _knightUpdateLevel, _minerUpdateLevel;
    //[SerializeField] Button _catapultBtn , _weaponBtn , _knightBtn, _minerBtn;
    [SerializeField] List<Button> _upgradeBtns;
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


    private void Start()
    {
        UpgradeDataInit();
    }

    public void BuyUpgrade(int btnIndex)
    {
        UpgradeSystem.BuyUpgrade(btnIndex);
    }

    public void SwitchBTNInteractToOn(int buttonIndex)
    {
        _upgradeBtns[buttonIndex].interactable = true;
    }
    public void SwitchBTNInteractToOff(int buttonIndex)
    {
        _upgradeBtns[buttonIndex].interactable = false;
    }
    public void SwitchBTNInteractToToggle(int buttonIndex)//Optional
    {
        _upgradeBtns[buttonIndex].interactable = !_upgradeBtns[buttonIndex].interactable;
    }

    private void UpgradeDataInit()
    {
        UpgradeDatas = new List<UpgradeData>();
        UpgradeDatas.Add(new UpgradeData(0,UpgradeEntityEnum.Weapon));
        UpgradeDatas.Add(new UpgradeData(1,UpgradeEntityEnum.Knight));
        UpgradeDatas.Add(new UpgradeData(2,UpgradeEntityEnum.Miner));
        UpgradeDatas.Add(new UpgradeData(3,UpgradeEntityEnum.Catapult));
    }

    public void InvokeUpdateHealthBar(Image image, float newHealth , float maxHealth)
    {
        UpdateHealthBarImageEvent?.Invoke(image,newHealth, maxHealth);
    }
    public void UpdateHealthBarUI(Image healthBar, float newHealth, float maxHealth) 
    {
        healthBar.fillAmount = newHealth/maxHealth;
    }
}
