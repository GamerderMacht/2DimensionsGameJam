using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamiteWeapon : MonoBehaviour
{
    public float collisionRadius;
    private CapsuleCollider capsuleCollider;
    [SerializeField] AudioSource bombAudio;
    [SerializeField] ParticleSystem explosionPS;

    public WeaponPickup weapon;

    private void Start()
    {
        collisionRadius = capsuleCollider.radius;
        weapon = GetComponent<WeaponPickup>();
        bombAudio = GetComponentInChildren<AudioSource>();
        explosionPS = GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && weapon.isHolding)
        {
            // Throw animation 
            Explode();
        }
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, collisionRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                collider.GetComponent<Health>().TakeDamage(100);
                Destroy(this.gameObject, 1f);
            }

            if (collider.CompareTag("Anything"))
            {
                explosionPS.Play();
                bombAudio.Play();
                Destroy(this.gameObject);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, collisionRadius);
    }
}
