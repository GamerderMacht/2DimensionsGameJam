using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public GameObject itemCurrentlyHolding;
    [SerializeField] GameObject playerRightHand;
    public bool isHolding = false;
    [SerializeField] bool canPickup = false;

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
        //itemCurrentlyHolding.transform.localScale = new Vector3(1f, 1f, 1f);
        itemCurrentlyHolding.transform.localPosition = new Vector3(0f, 0f, 0f);
        isHolding = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !isHolding)
        {
            canPickup = true;
            Debug.Log(canPickup);
            PickupWeapon();
        }
    }

    public void DropWeapon()
    {
        itemCurrentlyHolding.transform.parent = null;
        RaycastHit hit;
        Physics.Raycast(transform.position, -Vector3.up, out hit);
        itemCurrentlyHolding.transform.position = hit.point + new Vector3(transform.forward.x, 0, transform.forward.z);
        isHolding = false;
    }
}
