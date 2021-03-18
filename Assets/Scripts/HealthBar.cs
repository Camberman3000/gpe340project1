using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
    private GameObject player;


    void Start()
    {
       
    }

    private void Update()
    {
        player = GameObject.FindWithTag("Player");
        if (player)
        {
            Health health = player.GetComponent<Health>();
            //text.text = string.Format("Health: {0}%", Mathf.RoundToInt(health.percentHP * 100f));
            SetHealth(health.currentHP);
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
