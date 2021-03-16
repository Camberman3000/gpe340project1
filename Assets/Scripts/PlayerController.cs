using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : Controller
{
    [Header("Weapons")]
    private Weapon _weapon;
    private Weapon equippedWeapon;
    public Weapon weapon1;
    public Weapon weapon2;
    public Transform weaponAttachPoint;

    [Header("Components")]
    // Animator
    private Animator anim;

    [Header("Data")]
    [SerializeField, Tooltip("The max normal run speed of the player")]
    private float speed = 1.0f;
    [SerializeField, Tooltip("The max sprint speed of the player")]
    private float sprintSpeed = 66.0f;
    float newSpeed = 0f;
    [SerializeField, Tooltip("The rotation speed of the player when moving the mouse")]
    private float playerRotateSpeed = 90.0f;
    public Vector3 axis = Vector3.up;
    Vector3 MousePosition;

    public Vector3 jump;
    public float jumpForce = 2.0f;
    public bool isGrounded;
    Rigidbody rb;

    // Getter/Setter for health
    public Health health { get; private set; }

    public override void Awake()
    {
        base.Awake();
        // Get components
        anim = GetComponent<Animator>();
        health = GetComponent<Health>();
    }
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        HandlePlayerInput();
        HandlePlayerMovement();
        HandlePlayerRotation();
    }

    private void HandlePlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Debug.LogFormat("LMB DOWN!");
            // If player has weapon, start attack
            if (equippedWeapon)
            {
                equippedWeapon.AttackStart();
            }           
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            // If player has weapon, end attack
            if (equippedWeapon)
            {
                equippedWeapon.AttackEnd();
            }           
        }

        if (Input.GetKeyDown(KeyCode.Mouse4))
        {
            //_weapon.AltAttackStart();
        }

        if (Input.GetKeyUp(KeyCode.Mouse4))
        {
           // _weapon.AltAttackEnd();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {             
            // Equip primary weapon
            //Debug.LogFormat("You pressed 1 to equip weapon");             
            _weapon = weapon1;
            _weapon.animationType = WeaponAnimationType.Handgun;
            anim.SetInteger("Weapon Anim Type", ((int)_weapon.animationType));             
            EquipWeapon(_weapon);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // Equip secondary weapon
            //Debug.LogFormat("You pressed 2 to equip weapon");             
            _weapon = weapon2;
            _weapon.animationType = WeaponAnimationType.Rifle;
            anim.SetInteger("Weapon Anim Type", ((int)_weapon.animationType));
            EquipWeapon(_weapon);
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            // Unequip weapon
            if (equippedWeapon)
            {
                Debug.LogFormat("You pressed 0 to unequip weapon");
                equippedWeapon.animationType = WeaponAnimationType.None;
                _weapon.animationType = WeaponAnimationType.None;

                UnequipWeapon();
            }           
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
            Debug.LogFormat("Jump");
        }
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

    /// 
    /// Unequip the equipped weapon, if there is one
    /// 
    private void UnequipWeapon()
    {
        if (equippedWeapon)
        {             
            Destroy(equippedWeapon.gameObject);
            equippedWeapon = null;
        }
    }

    // Get mouse point on screen
    void GetPoint()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            // Mouse pos
            MousePosition = hit.point;
        }
    }    

    public void OnAnimatorIK()
    {
        // No weapon? Zero out IK and return
        if (equippedWeapon == null)
        {
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0.0f);
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0.0f);
            anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 0.0f);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0.0f);

            // exit
            return;
        }

        // Weapon uses right hand?
        if (equippedWeapon.rightHandPoint != null)
        {
            anim.SetIKPosition(AvatarIKGoal.RightHand, equippedWeapon.rightHandPoint.position);
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
            anim.SetIKRotation(AvatarIKGoal.RightHand, equippedWeapon.rightHandPoint.rotation);
            anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);
        }
        else
        {
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0.0f);
            anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 0.0f);
        }

        // Weapon uses left hand?
        if (equippedWeapon.leftHandPoint != null)
        {
            anim.SetIKPosition(AvatarIKGoal.LeftHand, equippedWeapon.leftHandPoint.position);
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
            anim.SetIKRotation(AvatarIKGoal.LeftHand, equippedWeapon.leftHandPoint.rotation);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);
        }
        else
        {
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0.0f);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0.0f);
        }
    }

    private void HandlePlayerMovement()
    {        
            // Player movement
            Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            input = Vector3.ClampMagnitude(input, 1f);
            input = transform.InverseTransformDirection(input);
            // Sprint 
            if (Input.GetKey(KeyCode.LeftShift))
            {
                newSpeed = speed + sprintSpeed;
                //Debug.LogFormat("speed {0}!", newSpeed);
            }
            else
            {
                newSpeed = speed;
                //Debug.LogFormat("speed {0}!", newSpeed);
            }
            input *= newSpeed;
            //Debug.LogFormat("input.x {0}: input.y {1}: ", input.x, input.y);

        // Keeps the idiot from moving when there's no input
        if (input.x == 0 || input.y == 0)
        {
            rb.isKinematic = true;
        }
        else
        {
            rb.isKinematic = false;
        }
            anim.SetFloat("Forward", input.x * newSpeed);
            anim.SetFloat("Right", input.z * newSpeed);   
    }

    private void HandlePlayerRotation()
    {
        // Get mouse ptr position
        GetPoint();
        // Get target rotation based on current mouse position and player position
        Quaternion targetRotation = Quaternion.LookRotation(MousePosition - transform.position);
        // Turn player to mouse position
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, playerRotateSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        //rb.constraints = ~RigidbodyConstraints.FreezePositionY;
        rb.AddForce(jump * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }
    void OnCollisionStay()
    {
        isGrounded = true;
        //rb.constraints = RigidbodyConstraints.FreezePositionY;         
    }
}
