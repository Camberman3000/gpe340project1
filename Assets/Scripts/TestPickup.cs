using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPickup : Pickup
{
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
        Debug.LogFormat("I've been picked up by {0}!", playerController);
        base.OnPickUp(playerController);
    }
}
