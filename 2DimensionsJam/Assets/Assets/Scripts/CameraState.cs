using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraState : MonoBehaviour
{   
    [SerializeField] private Camera cam;
    [SerializeField] private float maxDistance;
    private int playerLayer = 7;
    private Vector3 mousePos;

    private void Awake() {
        cam = Camera.main;
    }

    private void Update() {
        //If the player left clicks and the object has the potential
        //to become a player, hacking will ensue
        /*if(Input.GetMouseButtonDown(0)){
            mousePos = Input.mousePosition;
            RaycastHit hit;
            if(Physics.Raycast(, mousePos, out hit, maxDistance, playerLayer)){
                Debug.Log(hit.collider.gameObject.name);
            }   
    } */

    if (Input.GetMouseButtonDown(0))
{
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    RaycastHit hit;
    if (Physics.Raycast(ray, out hit, maxDistance))
    {
        if (hit.transform.tag == "Player")
        {
            Debug.Log("ishitting");
        }
    }
}
}

private void OnDrawGizmos() {
    Gizmos.DrawRay(cam.transform.position, mousePos);
}

}
