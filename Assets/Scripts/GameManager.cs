using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
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
    private GameObject menu;
    public Text menuText;
    public Text applyQuitText;
     
    public AudioMixer audioMixer;
    public MenuManager menuManager;
     

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

        //lives UI and spawn player;
        livesText.text = lives.ToString();
        SpawnPlayer();

        // Get menu ref
        menu = GameObject.FindGameObjectWithTag("Menu");
        menu.SetActive(false);

        // Set volume at game start
        GameManager.instance.audioMixer.SetFloat("Master Volume", menuManager.masterVol_Slider.value);
        GameManager.instance.audioMixer.SetFloat("Music Volume", menuManager.musicVol_Slider.value);
        GameManager.instance.audioMixer.SetFloat("SFX Volume", menuManager.sfxVol_Slider.value);

    }

    // Update is called once per frame
    void Update()
    {
        // Return if paused
        if (Paused)
            return;
    }

    private void SpawnPlayer()
    {        
        player = Instantiate(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation) as PlayerController;
        // Add listener
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
            Pause();
            // Game over
            Debug.Log("GAME OVER!!");
            livesText.text = "0";
            menuText.text = "Continue";
        }        
    }

    public void Pause()
    {
        instance.Paused = true;
        Time.timeScale = 0f;
        menu.SetActive(true);
        menuText.text = "Paused";
        applyQuitText.text = "Apply";
    }

    public void Resume()
    {
        Debug.Log("RESUME!!");
        instance.Paused = false;
        Time.timeScale = 1.0f;
        menu.SetActive(false);
    }
}
