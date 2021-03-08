using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //public List<GameObject> enemies;
    [SerializeField] private Enemy enemy;
    public float spawnDelay;
    private Enemy spawnedEnemy;
    private float nextSpawnTime;
    private Transform tf;
    // Start is called before the first frame update
    void Start()
    {
        tf = gameObject.GetComponent<Transform>();
        nextSpawnTime = Time.time + spawnDelay;
    }

    // Update is called once per frame
    void Update()
    {
        // If null then spawn
        if (spawnedEnemy == null)
        {
            // Enough time elapsed to spawn?
            if (Time.time > nextSpawnTime)
            {
                // Spawn & set next spawn time
                if (!spawnedEnemy)
                {
                    spawnedEnemy = Instantiate(enemy, tf.position, Quaternion.identity) as Enemy;
                    nextSpawnTime = Time.time + spawnDelay;
                }
            }
        }
        else
        {
            //// Object exists
            //GameObject enemyInWorld = GameObject.FindWithTag("Enemy");

            //if (!enemyInWorld)
            //{
            //    // Destroy and respawn
            //    Destroy(spawnedEnemy);
            //    spawnedEnemy = Instantiate(enemy, tf.position, Quaternion.identity) as Enemy;
            //}

            // Object exists and is active, so postpone the spawn
            nextSpawnTime = Time.time + spawnDelay;
        }
    }
}
