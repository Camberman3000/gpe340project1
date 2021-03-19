using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
    private GameObject player;
    private GameObject enemy;


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

        //enemy = GameObject.FindWithTag("Enemy");
        //if (GameObject.FindWithTag("Enemy"))
        //{
        //    Health health = enemy.GetComponent<Health>();
        //    HealthBar healthBar = enemy.GetComponent<HealthBar>();
        //    SetHealth(health.currentHP);
        //    healthBar.slider.value = health.currentHP;
        //}
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
