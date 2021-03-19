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
    //private Transform target;

    [Header("Data")]
    // Animator ref
    private Animator animator;
    private Vector3 desiredVelocity;

    private Weapon equippedWeapon;
    public Transform weaponAttachPoint;
    public List<Weapon> weapons;
    private Weapon _weapon;

    [SerializeField, Tooltip("The enemy's allowed attack angle")]
    [Range(1.0f, 100.0f)]
    private float attackAngle = 10.5f;

    [SerializeField, Tooltip("The enemy's max shooting range")]
    private float maxAttackRange = 100.0f;

    private void Awake()
    {
        // Cache animator
        animator = GetComponent<Animator>();

       
    }

    // Start is called before the first frame update
    void Start()
    {
        // Enemy equips random weapon
        _weapon = weapons[Random.Range(0, weapons.Count)];
        EquipWeapon(_weapon);
    }

    // Update is called once per frame
    void Update()
    {

        GameObject player = GameObject.FindWithTag("Player");

        // Sets what the enemy is targeting
        //navMeshAgent.SetDestination(target.position);
        if (player)
        {
            navMeshAgent.SetDestination(player.transform.position); // Enemy destination is player current location
        }       

        // Enemy Movement - uses root motion and navmesh agent to move
        desiredVelocity = Vector3.MoveTowards(desiredVelocity, navMeshAgent.desiredVelocity, navMeshAgent.acceleration * Time.deltaTime);
        Vector3 input = transform.InverseTransformDirection(desiredVelocity);
        animator.SetFloat("Forward", input.x);
        animator.SetFloat("Right", input.z);

        if (player)
        {
            // Distance between player and enemy
            float dist = Vector3.Distance(this.transform.position, player.transform.position);
            // Get direction to target
            Vector3 targetDir = player.transform.position - this.transform.position;
            // Get angle to target
            float angle = Vector3.Angle(targetDir, this.transform.forward);
            // Debug.LogFormat("Angle {0} and Attack angle {1}", angle, attackAngle);
            if (dist < maxAttackRange && angle <= attackAngle)
            {
                // Target is within range and attack angle
                if (equippedWeapon)
                {
                    equippedWeapon.AttackStart();
                }
            }
            else
            {
                // Target is either out of range or not within the attack angle
                equippedWeapon.AttackEnd();
            }
        }       
    }

    private void OnAnimatorMove()
    {
        // Limits speed of enemy
        navMeshAgent.velocity = animator.velocity;
    }
    private void EquipWeapon(Weapon weaponToEquip)
    {
        // Is there already a weapon equipped? If so, unequip first
        if (equippedWeapon != null)
        {
            UnequipWeapon();
        }
        equippedWeapon = weaponToEquip;
        // Equip the desired weapon
        equippedWeapon = Instantiate(weaponToEquip) as Weapon;
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

    public void DestroyCharacter()
    {
        Destroy(this.gameObject);
    }
}
