using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // Define GameManager instance as Singleton
    public static GameManager instance;
    public PlayerController playerPrefab;
    public Transform playerSpawnPoint;
    [SerializeField] private PlayerController player;
    [SerializeField] private float playerRespawnDelay;
    public int lives;
    private bool Paused;

    // Start is called before the first frame update
    void Start()
    {
        // Sets this instance of GameManager as the valid (and only) instance - If another instance is created, it will replace the existing one.
        if (instance == null)
        {
            // Set GameManager instance to this instance if none exists
            instance = this;
            // Set initial lives + 1 (one gets subtracted on initial spawn)
            lives = instance.lives + 1;
        }
        else
        {
            // Show error message if another instance of GameManager exists
            Debug.LogError("Error: There can be only one!...GameManager.");
            // Destroy the existing object to make way for the new one
            Destroy(gameObject);
        }
         
        SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (Paused)
            return;
    }

    private void SpawnPlayer()
    {
        // Update lives
        GameManager.instance.lives--;
        if (GameManager.instance.lives > 0)
        {
            Debug.Log("SPAWN PLAYER");
            // Define player
            player = Instantiate(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation) as PlayerController;

            // Add onDie listener
            player.health.onDie.AddListener(HandlePlayerDeath);
        }               
    }

    private void HandlePlayerDeath()
    {
        player.health.onDie.RemoveListener(HandlePlayerDeath);

        if (lives > 0)
        {
            Invoke("SpawnPlayer", playerRespawnDelay);
        }
        else
        {
            // Game over - show menu

            // Pause game
            Pause();
        }        
    }

    public static void Pause()
    {
        instance.Paused = true;
        // Set timescale to 0. This is like "bullet-time", slowing player perception.
        Time.timeScale = 0f;
    }
}
