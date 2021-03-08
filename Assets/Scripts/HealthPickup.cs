using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup
{
    [SerializeField, Tooltip("The amount of damage to apply to the player")]
    private float amtToHeal;

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
        // Apply healing
        playerController.health.Heal(amtToHeal);
        // Call base to destroy pickup
        base.OnPickUp(playerController);
    }
}
