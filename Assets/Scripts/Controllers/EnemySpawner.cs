using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] int EnemiesNumber;
    [SerializeField] float _enemyDamage;
    int countEnemies;
    [SerializeField] float _spawnTimer;
    [SerializeField] float _spawnCooldown;
    [SerializeField] GameObject _enemyPrefab;//healthBarPrefab
    [SerializeField] GameObject healthBarPrefab;
    [SerializeField] Transform _enemiesParent;
    [SerializeField] Transform enemiesHealthBarsParents;


    [SerializeField] float xOffset;
    [SerializeField] float yOffset;



    private void Start()
    {
        UIManager.Instance.UpdateHealthBarImageEvent+=UIManager.Instance.UpdateHealthBarUI;
    }

    void Update()
    {
        if(countEnemies<EnemiesNumber)
        if(_spawnTimer < _spawnCooldown)
            _spawnTimer += Time.deltaTime;
        else
        {
            countEnemies++;
            _spawnTimer = 0;
            SpawnGoblin();
        }
    }
    void SpawnGoblin() 
    {
        GameObject enemyInstance = Instantiate(_enemyPrefab,transform.position, _enemyPrefab.transform.rotation, _enemiesParent);
        GameObject enemyHealthBar = Instantiate(healthBarPrefab, enemiesHealthBarsParents);
        HealthBarFollow healthBarFollow = enemyHealthBar.GetComponent<HealthBarFollow>();
        UIManager.Instance.EnemyHealthBarAdded(healthBarFollow.HealthBarImage);

        EnemyController enemyController = enemyInstance.GetComponent<EnemyController>();
        enemyController.EnemyHealth = new Health(healthBarFollow.HealthBarImage, _enemyDamage);
        enemyController.HealthBarFollow = healthBarFollow;
        healthBarFollow.TargetTransform = enemyInstance.transform;
        healthBarFollow.yOffset = yOffset;
        healthBarFollow.yOffset = yOffset;
    }

}
