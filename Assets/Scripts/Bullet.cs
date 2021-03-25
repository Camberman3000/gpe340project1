using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damageDone;     
    // Lifespan of bullet
    [SerializeField] private float lifespan = 3.0f;
    [SerializeField] private ParticleSystem hitParticle;
    public AudioSource audioSource;
    [SerializeField] private AudioClip hitSound;


    public void Start()
    {         
        Destroy(gameObject, lifespan);
    }

    public void Update()
    {
       
    }

    public void OnCollisionEnter(Collision other)
    {

        // Bullet hit particle
        Instantiate(hitParticle, other.transform.position, other.transform.rotation);

        // Hit sound        
        audioSource.PlayOneShot(hitSound);
        
       

        if (other.gameObject.tag == "Bullet")
        {
            // Do nothing, collision with another bullet
        }         
        else if (other.gameObject.tag == "Dummy")
        {
            // Is target?
            GameObject bulletTarget = GameObject.FindWithTag("Dummy");
            if (other.gameObject == bulletTarget)
            {
                // Get enemy script
                EnemyData eData = bulletTarget.GetComponent<EnemyData>();
                // Apply dmg
                eData.health -= damageDone;
            }
            // Destroy bullet
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Player")
        {
            // Is target?
            GameObject bulletTarget = GameObject.FindWithTag("Player");
            if (other.gameObject == bulletTarget)
            {
                // Get health script
                Health playerHealth = bulletTarget.GetComponent<Health>();
                // Apply dmg
                playerHealth.Damage(damageDone);
            }
            // Destroy bullet
            Destroy(gameObject);
        }
        else
        {
            //Debug.LogFormat("Bullet - other.gameObject = Enemy ");
            // Is target?
            GameObject bulletTarget = GameObject.FindWithTag("Enemy");
            if (other.gameObject == bulletTarget)
            {                
                // Get health script
                Health enemyHealth = bulletTarget.GetComponent<Health>();
                // Apply dmg
                enemyHealth.Damage(damageDone);                
            }
            // Destroy bullet
            Destroy(gameObject);
        }
       
    }
}
