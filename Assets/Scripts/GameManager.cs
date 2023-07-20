using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Health PlayerCastleHealth { get; set; }//can get damage through health
    [SerializeField] private Image _castleHealthBarImage;
    public Health EnemyCastleHealth { get; set; }//can get damage through health
    [SerializeField] private Image _enemyCastleHealthBarImage;

    [SerializeField] private float _playerDamage;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        PlayerCastleHealth = new Health(_castleHealthBarImage, _playerDamage);
        EnemyCastleHealth = new Health(_enemyCastleHealthBarImage);
        PlayerCastleHealth.MaxHP = 100;
        PlayerCastleHealth.CurrnetHP = 100;
        EnemyCastleHealth.MaxHP = 100;
        EnemyCastleHealth.CurrnetHP = 100;
    }

}
