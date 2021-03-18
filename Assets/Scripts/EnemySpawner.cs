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

     
    [SerializeField, Tooltip("Enemy types to spawn")]
    private GameObject[] enemyUnits;

    [SerializeField] private int currentActiveEnemies;
    [SerializeField] private int maxActiveEnemies;
    private Health enemyHealth;

    // Start is called before the first frame update
    void Start()
    {
        tf = gameObject.GetComponent<Transform>();
        nextSpawnTime = Time.time + spawnDelay;
        InvokeRepeating("SpawnEnemy", 0f, spawnDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnEnemy()
    {
        //Debug.LogFormat("Spawning enemy");
        if (currentActiveEnemies >= maxActiveEnemies)
        {
           //Debug.LogFormat("Reached enemy limit!");
            CancelInvoke("SpawnEnemy");
            return;
        }        

        // Spawn an enemy          
        spawnedEnemy = Instantiate(enemy, tf.position, Quaternion.identity) as Enemy;
        nextSpawnTime = Time.time + spawnDelay;
        currentActiveEnemies++;
        //enemy.Health.onDie.AddListener(HandleEnemyDeath);
        enemyHealth = enemy.GetComponent<Health>();
        enemyHealth.onDie.AddListener(HandleEnemyDeath);        
    }

    private void HandleEnemyDeath()
    {
        currentActiveEnemies--;
    }
}
