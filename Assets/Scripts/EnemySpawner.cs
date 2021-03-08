using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //public List<GameObject> enemies;
    [SerializeField] private GameObject prefabDummy;
    public float spawnDelay;
    private GameObject spawnedEnemy;
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
                    spawnedEnemy = Instantiate(prefabDummy, tf.position, Quaternion.identity) as GameObject;
                    nextSpawnTime = Time.time + spawnDelay;
                }
               
            }
        }
        else
        {
            // Object exists, so postpone the spawn
            nextSpawnTime = Time.time + spawnDelay;
        }
    }
}
