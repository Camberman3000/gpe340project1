using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    // Camera move speed
    public float speed = 10.0f;
    // Player
    [SerializeField] private Transform player;
    // Camera
    [SerializeField]
    public Transform cam;
    // Cam distance from player
    [SerializeField]
    private float distance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player)
        {
            // Player pos
            Vector3 newPosition = player.position;
            // Distance from player
            newPosition.y += distance;
            // calculate distance to move
            float step = speed * Time.deltaTime;
            // Move cam to player
            cam.position = Vector3.MoveTowards(cam.position, newPosition, step);
        }             
    }
}
