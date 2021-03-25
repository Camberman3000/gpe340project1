using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
 

public class MenuManager : MonoBehaviour
{
    // Volume settings
    public Slider masterVol_Slider;
    [SerializeField, Tooltip("The slider value vs decibel volume curve")]
    private AnimationCurve volumeVsDecibels;
    public Slider sfxVol_Slider;
    public Slider musicVol_Slider;  

    // Player pref strings
    private string masterVolEntry = "Master Volume";
    private string sfxVolEntry = "SFX Volume";
    private string musicVolEntry = "Music Volume";

    // Graphics settings
    [SerializeField] private Dropdown resolution_Dropdown;
    [SerializeField] private Toggle screenMode_Toggle;
    [SerializeField] private Dropdown videoQuality_Dropdown;

    // Screen resolution
    private List<string> resolutionList;
    private Resolution[] resolutions;

    private void Awake()
    {
        resolutions = Screen.resolutions; // Get list of screen resolutions and save to array
        resolution_Dropdown.ClearOptions(); // Clear out existing dropdown options
        resolutionList = new List<string>(); // Create a list to hold resolutions
        for (int i = 0; i < Screen.resolutions.Length; i++) // Loop through each resolution
        {
            resolutionList.Add($"{Screen.resolutions[i].width} x {Screen.resolutions[i].height}"); // Add the resolution to the resolution list
            resolution_Dropdown.value = i; // Set the dropdown value to the resolution index
        }
        resolution_Dropdown.AddOptions(resolutionList); // Add the list of resolutions to the dropdown options

        videoQuality_Dropdown.ClearOptions(); // Clear the video quality options
        videoQuality_Dropdown.AddOptions(QualitySettings.names.ToList()); // Populate the video quality dropdown

        //PlayerPrefs.DeleteAll();
        // Get volume
        //float masterVol;
        //float musicVol;
        //float sfxVol;
        //GameManager.instance.audioMixer.GetFloat("Master Volume", out masterVol);
        //GameManager.instance.audioMixer.GetFloat("Music Volume", out musicVol);
        //GameManager.instance.audioMixer.GetFloat("SFX Volume", out sfxVol);

       
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResumeGame()
    {
        Debug.LogFormat("Clicked resume");
        SceneManager.LoadScene("Main");
    }

    public void ApplyQuitPressed()
    {
        if (GameManager.instance.menuText.text == "Paused")
        {
            // Handle Quit Game
            QuitGame();
        }
        else
        {
            // Handle Apply Settings
            ApplySettings();
        }
    }

    public void ApplySettings()
    {
        Debug.Log("Apply settings");
        // Save settings to player prefs
        PlayerPrefs.SetFloat(masterVolEntry, masterVol_Slider.value);
        PlayerPrefs.SetFloat(sfxVolEntry, sfxVol_Slider.value);
        PlayerPrefs.SetFloat(musicVolEntry, musicVol_Slider.value);

        // Set screen resolution
        int i = resolution_Dropdown.value;            
        Screen.SetResolution(Screen.resolutions[i].width, Screen.resolutions[i].height, true);

        // Set volume 
        GameManager.instance.audioMixer.SetFloat("Master Volume", masterVol_Slider.value);
        GameManager.instance.audioMixer.SetFloat("Music Volume", musicVol_Slider.value);
        GameManager.instance.audioMixer.SetFloat("SFX Volume", sfxVol_Slider.value);
    }

    public void QuitGame()
    {
        EditorApplication.isPlaying = false;
        Application.Quit();
    }

    private void OnEnable()
    {
        // Set master volume
        if (PlayerPrefs.HasKey(masterVolEntry))
        {
            masterVol_Slider.value = PlayerPrefs.GetFloat(masterVolEntry);
        }

        if (PlayerPrefs.HasKey(sfxVolEntry))
        {
            sfxVol_Slider.value = PlayerPrefs.GetFloat(sfxVolEntry);
        }

        if (PlayerPrefs.HasKey(musicVolEntry))
        {
            musicVol_Slider.value = PlayerPrefs.GetFloat(musicVolEntry);
        }

        screenMode_Toggle.isOn = Screen.fullScreen;
        videoQuality_Dropdown.value = QualitySettings.GetQualityLevel();
    }
}
 