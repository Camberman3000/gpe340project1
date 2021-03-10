using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ragdoll : MonoBehaviour
{

    private Rigidbody[] childRigidbodies;
    private Collider[] childColliders;
    private Rigidbody mainRigidbody;
    private Collider mainCollider;
    private Animator animator;
    private NavMeshAgent navAgent;
    private float originalNavSpeed;

    private void Awake()
    {
        childRigidbodies = GetComponentsInChildren<Rigidbody>();
        childColliders = GetComponentsInChildren<Collider>();
        mainRigidbody = GetComponent<Rigidbody>();
        mainCollider = GetComponent<Collider>();
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        TurnOffRagdoll();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnOnRagdoll()
    {
        foreach (var rB in childRigidbodies)
        {
            rB.isKinematic = false;
        }

        foreach (var coll in childColliders)
        {
            coll.enabled = true;
        }

        mainCollider.enabled = false;
        mainRigidbody.isKinematic = true;
        animator.enabled = false;

        // Keeps enemy from following player when dead
        if (navAgent)
        {
            // store orig value
            originalNavSpeed = navAgent.speed;
            // Set speed to 0
            navAgent.speed = 0;
        }
       
    }

    
    private void TurnOffRagdoll()
    {
        foreach (var rB in childRigidbodies)
        {
            rB.isKinematic = true;
        }

        foreach (var coll in childColliders)
        {
            coll.enabled = false;
        }

        mainCollider.enabled = true;
        mainRigidbody.isKinematic = false;
        animator.enabled = true;
        // Reset speed to orig value
        navAgent.speed = originalNavSpeed;
    }
}
