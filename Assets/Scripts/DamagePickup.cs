using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePickup : Pickup
{

    [SerializeField, Tooltip("The amount of damage to apply to the player")]
    private float damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnPickUp(PlayerController playerController)
    {
        // Apply damage
        playerController.health.Damage(damage);
        // Call base to destroy the pickup
        base.OnPickUp(playerController);
    }
}
