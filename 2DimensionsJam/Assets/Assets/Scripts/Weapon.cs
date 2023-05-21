using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Collider col;
    [SerializeField] private int whatIsEnemy;
    [SerializeField] private int damage;

    private void Start() {
        col = GetComponent<CapsuleCollider>();
        Debug.Log(col);
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("The bat has collided with: " + other.gameObject);
        if(other.gameObject.layer == whatIsEnemy){
            other.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
