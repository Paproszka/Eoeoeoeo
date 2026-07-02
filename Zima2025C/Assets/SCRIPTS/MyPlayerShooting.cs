using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayerShooting : MonoBehaviour
{
    public MyPlayerHealth playerHealth;

    public Transform bulletSpawnPoint;
    public ParticleSystem flamethrower;
    public Animator animator;

    public Transform socketSpine;
    public Transform socketRightHand;
    public GameObject weapon;

    private bool usingWeapon = false;

    private void Start()
    {
        playerHealth.onDeath += DetachWeapon;

        AttachWeapon(usingWeapon ? socketRightHand : socketSpine);
        
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localEulerAngles = Vector3.zero;
        flamethrower.Stop();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            usingWeapon = !usingWeapon;

            AttachWeapon(usingWeapon ? socketRightHand : socketSpine);
            animator.SetBool("UsingWeapon", usingWeapon);
        }

        if (usingWeapon == false)
            return;

        if (Input.GetButtonDown("Fire1"))
        {
            //animator.SetLayerWeight(1, 1);
            //animator.SetFloat("Firing", 1);
            flamethrower.Play();
        }

        if (Input.GetButtonUp("Fire1"))
        {
            //animator.SetLayerWeight(1, 0);
            //animator.SetFloat("Firing", 0);
            flamethrower.Stop();
        }
    }

    private void FixedUpdate()
    {
        if (flamethrower.isPlaying)
        {
            if (Physics.SphereCast(bulletSpawnPoint.position, 0.25f, bulletSpawnPoint.forward, out var hitInfo, 4.0f))
            {
                if (hitInfo.collider.CompareTag("Enemy"))
                {
                    var enemyHealth = hitInfo.collider.GetComponent<EnemyHealth>();
                    enemyHealth.TakeDamage(3);
                }
            }
        }
    }

    private void DetachWeapon()
    {
        AttachWeapon(null);
        weapon.AddComponent<MeshCollider>().convex = true;
        weapon.AddComponent<Rigidbody>();
    }

    private void AttachWeapon(Transform parent)
    {
        weapon.transform.parent = parent;
        if (parent != null)
        {
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localEulerAngles = Vector3.zero;
        }
    }
}