using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float spawnTimer;
    [SerializeField] float spawnCooldown;

    // Update is called once per frame
    void Update()
    {
        if(spawnTimer < spawnCooldown)
            spawnTimer+=Time.deltaTime;
        else
        {
            spawnTimer = 0;
            //do something
        }
    }
}
