using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField]
    private float lifespan;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        Destroy(gameObject, lifespan);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        PlayerController playerController = collider.GetComponent<PlayerController>();
        if (playerController)
        {
            OnPickUp(playerController);
        }
    }

    protected virtual void OnPickUp(PlayerController playerController)
    {
        Destroy(gameObject);
    }
}
