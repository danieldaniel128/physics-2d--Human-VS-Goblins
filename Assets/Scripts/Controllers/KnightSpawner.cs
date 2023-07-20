using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightSpawner : MonoBehaviour
{
    [SerializeField] int KnightsNumber;
    [SerializeField] float _knightDamage;
    int countKnights;
    [SerializeField] float _spawnTimer;
    [SerializeField] float _spawnCooldown;
    [SerializeField] GameObject _knightPrefab;//healthBarPrefab
    [SerializeField] GameObject healthBarPrefab;
    [SerializeField] Transform _knightsParent;
    [SerializeField] Transform _knightsHealthBarsParents;


    [SerializeField] float xOffset;
    [SerializeField] float yOffset;



    private void Start()
    {
        UIManager.Instance.UpdateHealthBarImageEvent += UIManager.Instance.UpdateHealthBarUI;
    }

    void Update()
    {
        if (countKnights < KnightsNumber)
            if (_spawnTimer < _spawnCooldown)
                _spawnTimer += Time.deltaTime;
            else
            {
                countKnights++;
                KnightsNumber++;//decided to spawn the knights forever
                _spawnTimer = 0;
                SpawnKnight();
            }
    }
    void SpawnKnight()
    {
        GameObject knightInstance = Instantiate(_knightPrefab, transform.position, _knightPrefab.transform.rotation, _knightsParent);
        GameObject knightHealthBar = Instantiate(healthBarPrefab, _knightsHealthBarsParents);
        HealthBarFollow healthBarFollow = knightHealthBar.GetComponent<HealthBarFollow>();

        KnightConroller knightController = knightInstance.GetComponent<KnightConroller>();
        knightController.KnightHealth = new Health(healthBarFollow.HealthBarImage, _knightDamage);
        InitKnightData(knightController);
        knightController.KnightHealthBarFollow = healthBarFollow;
        healthBarFollow.TargetTransform = knightInstance.transform;
        healthBarFollow.yOffset = yOffset;
        healthBarFollow.yOffset = yOffset;
    }
    void InitKnightData(KnightConroller knightController)
    {
        knightController.KnightHealth.MaxHP = UIManager.Instance.KnightData.KnightHealth;
        knightController.KnightHealth.CurrnetHP = UIManager.Instance.KnightData.KnightHealth;
        knightController.KnightHealth.Damage = UIManager.Instance.KnightData.KnightDamage;
        knightController.KnightSpeed = UIManager.Instance.KnightData.MoveSpeed;
    }
}
