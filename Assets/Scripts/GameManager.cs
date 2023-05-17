using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Health PlayerCastleHealth { get; set; }//can get damage through health
    [SerializeField] private Image _castleHealthBarImage;
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
    }

}
