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

    private GameObject equippedWeapon;
    public Transform weaponAttachPoint;
    public List<GameObject> weapons;
    private GameObject _weapon;


    private void Awake()
    {
        // Cache animator
        animator = GetComponent<Animator>();

        _weapon = weapons[Random.Range(0, weapons.Count)];
        EquipWeapon(_weapon);
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
    private void EquipWeapon(GameObject weaponToEquip)
    {
        // Is there already a weapon equipped? If so, unequip first
        if (equippedWeapon != null)
        {
            UnequipWeapon();
        }
        equippedWeapon = weaponToEquip;
        // Equip the desired weapon
        equippedWeapon = Instantiate(weaponToEquip) as GameObject;
        // Attach to specific point on player
        equippedWeapon.transform.SetParent(weaponAttachPoint);
        // Set weapon loc and rot
        equippedWeapon.transform.localPosition = _weapon.transform.localPosition;
        equippedWeapon.transform.localRotation = _weapon.transform.localRotation;
    }
    private void UnequipWeapon()
    {
        if (equippedWeapon)
        {
            Destroy(equippedWeapon.gameObject);
            equippedWeapon = null;
        }
    }

}
