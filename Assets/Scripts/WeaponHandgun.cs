using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandgun :Weapon
{
     
    [SerializeField] private GameObject prefabBullet;   
    [SerializeField] private Transform muzzle;    
    public bool triggerPulled;
    private int shotCounter = 0;
    private float timeNextShotIsReady;
    [SerializeField] private float shotsPerMinute;
    [SerializeField] private int spread = 2;

    public override void Awake()
    {
        timeNextShotIsReady = Time.time;
    }

    // Start is called before the first frame update
    public override void Start()
    {
        // Call parent class function so it runs too
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        if (triggerPulled)
        {
            while (Time.time > timeNextShotIsReady)
            {
                // Shoot
                ShootBullet();
                timeNextShotIsReady += 60f / shotsPerMinute;
            }
        }
        else if (Time.time > timeNextShotIsReady)
        {
            /**makes sure to keep timeNextShotIsReady to ?now? if the trigger isn?t 
             * pulled but there?s been a long enough delay. If we didn?t do this we?d end up storing shots.
             * When we pulled the trigger again all of these shots would be fired at once, 
             * since timeNextShotIsReady would be way behind Time.time.*/
            timeNextShotIsReady = Time.time;
        }
    }

    public override void AttackStart()
    {       
        triggerPulled = true;
    }

    public override void AttackEnd()
    {
        triggerPulled = false;
        shotCounter = 0;
    }

    public void MuzzleFlash()
    {
        if (muzzleFlashParticle)
        {            
            muzzleFlashParticle.Emit(1);
        }
    }

    public void ShootBullet()
    {
        // Particles
        MuzzleFlash();
        // Fire sound def
        AudioSource audioData;
        audioData = GetComponent<AudioSource>();

        if (firingMode == FiringMode.Semi && shotCounter < 1)
        {
            //Debug.LogFormat("PEW PEW!");
            // Instantiate
            GameObject projectile = Instantiate(prefabBullet, muzzle.position, muzzle.rotation * Quaternion.Euler(Random.onUnitSphere * spread)) as GameObject;  
            // Get bullet class ref
            Bullet bullet = projectile.gameObject.GetComponent<Bullet>();
            // Define damage  
            bullet.damageDone = damagePerShotSingle;
            // Get bullet rigidbody and propel forward
            Rigidbody rBody = projectile.GetComponent<Rigidbody>();
            rBody.AddRelativeForce(Vector3.forward * bulletSpeed, ForceMode.VelocityChange);
            // Increment shot counter
            shotCounter++;
           // Play shooting sound assigned in audiosource component
            audioData.Play(0);

        }
        else if (firingMode == FiringMode.ThreeShotBurst && shotCounter < 3)
        {
            //Debug.LogFormat("PEW PEW!");
            // Instantiate
            GameObject projectile = Instantiate(prefabBullet, muzzle.position, muzzle.rotation * Quaternion.Euler(Random.onUnitSphere * spread)) as GameObject;
            // Get bullet class ref
            Bullet bullet = projectile.gameObject.GetComponent<Bullet>();
            // Define damage
            bullet.damageDone = damagePerShotThreeShot;
            // Get bullet rigidbody and propel forward  
            Rigidbody rBody = projectile.GetComponent<Rigidbody>();
            rBody.AddRelativeForce(Vector3.forward * bulletSpeed, ForceMode.VelocityChange);
            // Increment shot counter
            shotCounter++;
            // Play shooting sound
            audioData.Play(0);
        }        
    }
     
}
