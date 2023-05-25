using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public GameObject itemCurrentlyHolding;
    public GameObject playerRightHand;
    public bool isHolding = false;
    [SerializeField] bool canPickup = false;

    private void Start() {
        this.playerRightHand = FindClosestArm();
        
    }
    public GameObject FindClosestArm()
    {
        GameObject[] arms;
        arms = GameObject.FindGameObjectsWithTag("ArmSlot");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject arm in arms)
        {
            Vector3 diff = arm.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = arm;
                distance = curDistance;
            }
        }
        return closest;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canPickup)
        {
            PickupWeapon();
        }
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DropWeapon();
        }
    }
   
   

    public void PickupWeapon()
    {
        
        itemCurrentlyHolding.transform.SetParent(playerRightHand.transform);
        itemCurrentlyHolding.transform.localPosition = new Vector3(0,0,0);
        if(itemCurrentlyHolding.tag == "Weapon")itemCurrentlyHolding.transform.localRotation = Quaternion.Euler(-260,0,-90);
        if(itemCurrentlyHolding.tag == "Pistol")itemCurrentlyHolding.transform.localRotation = Quaternion.Euler(-160,120,0);
        

        isHolding = !isHolding;
        canPickup = !canPickup;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collided");
        if (other.gameObject.CompareTag("Weapon") && !isHolding)
        {
            Debug.Log("compared");
            canPickup = true;
            
            itemCurrentlyHolding = other.gameObject;
            
        }
        if (other.gameObject.CompareTag("Pistol") && !isHolding)
        {
            Debug.Log("compared");
            canPickup = true;
            
            itemCurrentlyHolding = other.gameObject;
            
        }
    }
    private void OnTriggerExit(Collider other) {
        
    }

    public void DropWeapon()
    {
        //Drops the weapon in front of the player
        
            RaycastHit hit;
            Physics.Raycast(transform.localPosition, -Vector3.up, out hit);
            itemCurrentlyHolding.transform.position = hit.point + new Vector3(transform.forward.x, 0, transform.forward.z);
            isHolding = !isHolding;
            itemCurrentlyHolding.transform.parent = null;
            itemCurrentlyHolding = null;
        
        
        
    }
}
