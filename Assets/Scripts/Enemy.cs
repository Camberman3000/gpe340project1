using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [Header("Components")]
    [SerializeField, Tooltip("The NavMeshAgent")]
    private NavMeshAgent navMeshAgent;
    [SerializeField, Tooltip("The enemy's target")]    
    private Transform target;

    [Header("Data")]
    // Animator ref
    private Animator animator;
    private Vector3 desiredVelocity;
    

    private void Awake()
    {
        // Cache animator
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        GameObject player = GameObject.FindWithTag("Player");         

        // Sets what the enemy is targeting
        //navMeshAgent.SetDestination(target.position);
        navMeshAgent.SetDestination(player.transform.position);

        // Enemy Movement
        desiredVelocity = Vector3.MoveTowards(desiredVelocity, navMeshAgent.desiredVelocity, navMeshAgent.acceleration * Time.deltaTime);
        Vector3 input = transform.InverseTransformDirection(desiredVelocity);


        animator.SetFloat("Forward", input.x);
        animator.SetFloat("Right", input.z);
    }

    private void OnAnimatorMove()
    {
        // Limits speed of enemy
        navMeshAgent.velocity = animator.velocity;
    }


}
