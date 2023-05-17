using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField] Image CastleHealthBarImage;
    [SerializeField] List<Image> _enemiesHealthBarImages;

    public static UIManager Instance;

    public event Action< Image, float, float> UpdateHealthBarImageEvent;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }


    public void EnemyHealthBarAdded(Image enemyHealthBarImage)
    {
        _enemiesHealthBarImages.Add(enemyHealthBarImage);
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
