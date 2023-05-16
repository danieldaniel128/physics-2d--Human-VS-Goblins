using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float _spawnTimer;
    [SerializeField] float _spawnCooldown;
    [SerializeField] GameObject _enemyPrefab;//healthBarPrefab
    [SerializeField] GameObject healthBarPrefab;
    [SerializeField] Transform _enemiesParent;
    [SerializeField] RectTransform canvasRectTransform;
    [SerializeField] Canvas canvas;
    [SerializeField] Transform enemiesHealthBarsParents;
    [SerializeField] float yOffset;
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
        GameObject enemyInstance = Instantiate(_enemyPrefab,transform.position, _enemyPrefab.transform.rotation, _enemiesParent);
        GameObject healthBar = Instantiate(healthBarPrefab, enemiesHealthBarsParents);
        healthBar.GetComponent<EnemyHealthBarFollow>().enemyTransform = enemyInstance.transform;
        RectTransform healthBarRectTransform = healthBar.GetComponent<RectTransform>();

        // Get the canvas's RectTransform
        canvasRectTransform = canvas.GetComponent<RectTransform>();

        // Position the health bar relative to the enemy
        Vector3 healthBarPosition = Camera.main.WorldToScreenPoint(enemyInstance.transform.position + Vector3.up * yOffset);
        healthBarRectTransform.position = healthBarPosition - new Vector3((canvasRectTransform.sizeDelta / 2f).x, (canvasRectTransform.sizeDelta / 2f).y);
    }

}
