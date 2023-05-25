using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class IsoCamera : MonoBehaviour
{
    [SerializeField] private float maxDistance;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject hackingCanvas;
    private static GameObject hackedRobot;
    private Animator anim;
    private void Start() {
        hackingCanvas = GameObject.Find("Hacking Canvas");
        hackingCanvas.SetActive(false);
        anim = GetComponent<Animator>();
    }

private void Update(){
        
    if (Input.GetMouseButtonDown(0)) // Player presses down
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            // The ray has hit an object
            Debug.Log("Hit object: " + hit.collider.gameObject.name);
            
            // The layer is on the robot layer
                if(hit.collider.gameObject.layer == 8){
                    hackedRobot = hit.collider.gameObject;
                //  begin hacking sequence, then if hack was successful, then set the robot to a player and zoom camera
                // 1. Begin Hacking Sequence
                // 2. If correct switch robot from an enemy to a player
                // 3. Set robot priority camera to the highest in the scene(this makes it so that the cinemachine camera transition occurs)
                hackingCanvas.SetActive(true);
                //hit.collider.gameObject.GetComponentInChildren<CinemachineVirtualCamera>().Priority = 100;
                // We slowly move toward that new Camera position and rotation
                }
        }
    }
}

    public static void SwitchPerspectives(){
        hackedRobot.GetComponentInChildren<CinemachineVirtualCamera>().Priority = 100;
        hackedRobot.GetComponent<Movement_Danny>().enabled = true;
        hackedRobot.GetComponent<Movement>().enabled = false;
    }

    private void OnDrawGizmos()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray.origin, ray.direction * 100f);
    }
}
