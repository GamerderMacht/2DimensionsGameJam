using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int currentHealth {get; private set;}
    [SerializeField] private int TotalHealth;

    public void TakeDamage(int amount){
        Debug.Log("Object is taking damage");
        currentHealth -= amount;
        if(currentHealth <= 0){
            Die();
        }
    }


    private void Die(){
        //Will need to implement when I get the chance to
    }
}
