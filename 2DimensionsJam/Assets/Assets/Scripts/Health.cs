using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int currentHealth {get; private set;}
    [SerializeField] private int TotalHealth;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private Transform explosionLocation;

    public void TakeDamage(int amount){
        Debug.Log("Object is taking damage");
        currentHealth -= amount;
        if(currentHealth <= 0){
            Die();
        }
    }


    //only have implemented server so far
    private void Die(){
        Instantiate(explosionPrefab, explosionLocation.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
