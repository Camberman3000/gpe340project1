using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyData : MonoBehaviour
{

    //[SerializeField] private int health = 8;

    public int health = 8;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (health <= 0)
        {
            Debug.Log("DUMMY HIT!");
            // Die
            Destroy(this.gameObject);
        }
    }
}
