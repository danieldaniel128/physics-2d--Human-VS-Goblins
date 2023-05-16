using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float _spawnTimer;
    [SerializeField] float _spawnCooldown;
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] Transform _enemiesParent;
    // Update is called once per frame
    void Update()
    {
        if(_spawnTimer < _spawnCooldown)
            _spawnTimer += Time.deltaTime;
        else
        {
            _spawnTimer = 0;
            SpawnGoblin();
        }
    }
    void SpawnGoblin() 
    {
        Instantiate(_enemyPrefab,transform.position, _enemyPrefab.transform.rotation, _enemiesParent);
    }

}
