using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class IsoCamera : MonoBehaviour
{
    [SerializeField] private float sphereSize;
    [SerializeField] private float maxDistance;
    [SerializeField] private Camera cam;
    private void Update()
{
    if (Input.GetMouseButtonDown(0)) // Player presses down
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // The ray has hit an object
            Debug.Log("Hit object: " + hit.collider.gameObject.name);
            
            // The layer is on the robot layer
            if(hit.collider.gameObject.layer == 8){
                // We grab the robot's holder camera position
                hit.collider.gameObject.GetComponentInChildren<CinemachineVirtualCamera>().Priority = 100;

                // We slowly move toward that new Camera position and rotation
                
                //hit.collider.gameObject.GetComponent<TransitionCam>().PlayerClicksOnRobot(hit.collider.gameObject, );
            }
            // Perform any actions or logic based on the hit object
            // For example, you can call a function on the hit object:
            // hit.collider.gameObject.GetComponent<MyScript>().MyFunction();
        }
        else
        {
            // The raycast did not hit any object
            Debug.Log("No hit");
        }
    }
}

    private void OnDrawGizmos()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray.origin, ray.direction * 100f);
    }
}
