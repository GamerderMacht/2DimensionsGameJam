using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] public int damageAmount;
    private void OnTriggerEnter(Collider other) {
        //Server layer or player object
        if(other.gameObject.layer == 7 || other.gameObject.GetComponent<Movement_Danny>()){
            if(other.gameObject.GetComponent<Health>()) other.gameObject.GetComponent<Health>().TakeDamage(damageAmount);
            Debug.Log("Player or the Server was just hit!");
        }
        Debug.Log(other.gameObject + " was hit");
    }
}
