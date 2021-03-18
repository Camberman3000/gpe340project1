using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private Text livesText;
     

    // Start is called before the first frame update
    void Start()
    {
        // Sets this instance of GameManager as the valid (and only) instance - If another instance is created, it will replace the existing one.
        if (instance == null)
        {
            // Set GameManager instance to this instance if none exists
            instance = this;
        }
        else
        {
            // Show error message if another instance of GameManager exists
            Debug.LogError("Error: There can be only one!...GameManager.");
            // Destroy the existing object to make way for the new one
            Destroy(gameObject);
        }

        //lives = lives + 1;
        livesText.text = lives.ToString();
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
        player = Instantiate(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation) as PlayerController;

        player.health.onDie.AddListener(HandlePlayerDeath);        
    }

    private void HandlePlayerDeath()
    {
        player.health.onDie.RemoveListener(HandlePlayerDeath);
        lives--;
        if (lives > 0)
        {
            
            livesText.text = lives.ToString();
            Invoke("SpawnPlayer", playerRespawnDelay);
        }
        else
        {
            // Game over
            Debug.Log("GAME OVER!!");
            livesText.text = "0";
        }
        
    }

    public static void Pause()
    {
        instance.Paused = true;
        Time.timeScale = 0f;
    }
}
