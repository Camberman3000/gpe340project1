using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public enum FiringMode { Safe, Single, Semi, ThreeShotBurst, Full };
[System.Serializable]
public enum WeaponAnimationType { None = 0, Rifle = 1, Handgun = 2 }


public abstract class Weapon : MonoBehaviour
{
    [Header("IK Points")]
    public Transform rightHandPoint;
    public Transform leftHandPoint;

    [Header("Firing Events")]
    public UnityEvent OnAttackStart;
    public UnityEvent OnAttackEnd;
    public UnityEvent OnAltAttackStart;
    public UnityEvent OnAltAttackEnd;

    [Header("Data")]
    [SerializeField] protected int damagePerShotSingle;
    [SerializeField] protected int damagePerShotThreeShot;
    [SerializeField] protected int bulletSpeed;
    

    // Check if conditions allow shooting
    protected bool bCanFire;
    // Check if player is shooting
    protected bool bIsShooting;
    // Firing mode
    public FiringMode firingMode = FiringMode.Semi;
    // Anim type
    public WeaponAnimationType animationType = WeaponAnimationType.None;
    
    public virtual void Awake()
    {

    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
       
    }

    public virtual void AttackStart()
    {         
        OnAttackStart.Invoke();
    }

    public virtual void AttackEnd()
    {         
        OnAttackEnd.Invoke();
    }

    public virtual void AltAttackStart()
    {
        // TODO:
        OnAltAttackStart.Invoke();
    }

    public virtual void AltAttackEnd()
    {
        // TODO:
        OnAltAttackEnd.Invoke();
    }
}
