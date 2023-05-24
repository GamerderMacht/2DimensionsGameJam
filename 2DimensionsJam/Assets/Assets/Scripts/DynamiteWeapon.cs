using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamiteWeapon : MonoBehaviour
{
    public float collisionRadius;
    private CapsuleCollider capsuleCollider;

    private void Start()
    {
        collisionRadius = capsuleCollider.radius;
    }

    private void Update()
    {
        Explode();
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, collisionRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                /*AI_Health aitarget = hit.transform.GetComponent<AI_Health>();
                if (aitarget != null)
                {
                    aitarget.TakingDamage(damage);
                }*/
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, collisionRadius);
    }
}
