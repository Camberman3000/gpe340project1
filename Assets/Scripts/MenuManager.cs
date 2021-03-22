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
    [SerializeField] private Dropdown resolution_Dropdown;
    [SerializeField] private Toggle screenMode_Toggle;
    [SerializeField] private Dropdown videoQuality_Dropdown;
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
    }

    public void QuitGame()
    {
        EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
