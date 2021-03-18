using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestHealthText : MonoBehaviour
{
    public Health health;
    private Text text;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
       
    }
    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        player = GameObject.FindWithTag("Player");
        if (player)
        {
            Health health = player.GetComponent<Health>();
            text.text = string.Format("Health: {0}%", Mathf.RoundToInt(health.percentHP * 100f));
        }       
    }
}
