using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{

    [Header("Events")]
    [SerializeField, Tooltip("Raised every time the object is healed.")]
    private UnityEvent onHeal;
    [SerializeField, Tooltip("Raised every time the object is damaged.")]
    private UnityEvent onDamage;
    [SerializeField, Tooltip("Raised once when the object's health reaches 0.")]
    private UnityEvent onDie;

    [SerializeField] private float maxHP = 100;
    [SerializeField] private float currentHP;

    // for UI
    public float percentHP { get { return currentHP / maxHP; } }    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// 
    /// Apply the specified amount of damage
    /// 
    /// Damage to apply. Must be positive.
    public void Damage(float damage)
    {
        Debug.LogFormat("I've taken {0} damage!", damage);
        // Get larger value
        damage = Mathf.Max(damage, 0f);
        // Clamp to prevent going over/under
        currentHP = Mathf.Clamp(currentHP - damage, 0f, maxHP);
        SendMessage("OnDamage", SendMessageOptions.DontRequireReceiver);
        // If 0, die
        if (currentHP <= 0f)
        {
            currentHP = 0f;
            SendMessage("OnDie", SendMessageOptions.DontRequireReceiver);
            onDie.Invoke();
            Debug.LogFormat("He's Dead, Jim.");
        }
    }

    public void Heal(float health)
    {
        Debug.LogFormat("I've been healed for {0}!", health);
        // Get larger value
        health = Mathf.Max(health, 0f);
        // Clamp to prevent going over/under
        currentHP = Mathf.Clamp(currentHP + health, 0f, maxHP);
        SendMessage("OnDamage", SendMessageOptions.DontRequireReceiver);             
    }
}
