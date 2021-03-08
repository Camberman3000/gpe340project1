using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestHealthText : MonoBehaviour
{
    public Health health;
    private Text text;

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
        text.text = string.Format("Health: {0}%", Mathf.RoundToInt(health.percentHP * 100f));
    }
}
