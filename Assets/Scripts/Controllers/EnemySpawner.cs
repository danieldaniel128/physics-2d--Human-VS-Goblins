using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] int EnemiesNumber;
    int countEnemies;
    [SerializeField] float _spawnTimer;
    [SerializeField] float _spawnCooldown;
    [SerializeField] GameObject _enemyPrefab;//healthBarPrefab
    [SerializeField] GameObject healthBarPrefab;
    [SerializeField] Transform _enemiesParent;
    [SerializeField] RectTransform canvasRectTransform;
    [SerializeField] Canvas canvas;
    [SerializeField] Transform enemiesHealthBarsParents;
    [SerializeField] float xOffset;
    [SerializeField] float yOffset;
    // Update is called once per frame
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
        healthBarFollow.TargetTransform = enemyInstance.transform;
        healthBarFollow.yOffset = yOffset;
        healthBarFollow.yOffset = yOffset;
    }

}
