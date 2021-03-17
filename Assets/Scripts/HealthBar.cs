using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
    private GameObject player;
    public Text livesText;

    void Start()
    {
       
    }

    private void Update()
    {
        // Update health
        player = GameObject.FindWithTag("Player");
        if (player)
        {
            Health health = player.GetComponent<Health>();             
            SetHealth(health.currentHP);
            // Show remaining lives to UI
            livesText.text = GameManager.instance.lives.ToString();
        }
       
    }
    public void SetHealth(float health)
    {
        slider.value = health;
    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }     
}
