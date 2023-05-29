using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon_new : MonoBehaviour
{

        [SerializeField] private SphereCollider col;
    private void Awake() {
        col.enabled = false;
                this.enabled = false;
    }
        private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Enemy"){
            other.GetComponent<Health>().TakeDamage(100);
        }
    }
}
