using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.AI;

public class IsoCamera : MonoBehaviour
{
    [SerializeField] private float maxDistance;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject hackingCanvas;
    [SerializeField] private GameObject hackingManager;
    private static GameObject hackedRobot;
    private Animator anim;
    private void Start() {
        //hackingCanvas = GameObject.Find("Hacking Canvas");
        //hackingCanvas.SetActive(false);
        anim = GetComponent<Animator>();
    }

private void Update(){
        
    if (Input.GetMouseButtonDown(0)) // Player presses down
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {   
            
            Debug.Log(Input.mousePosition);
            // The layer is on the robot layer
                if(hit.collider.gameObject.layer == 8){
                    Debug.Log(hit.collider.gameObject.name);
                    Debug.Log("The robot has been hit!");
                    hackedRobot = hit.collider.gameObject;
                    if(hit.collider.GetComponent<NavMeshAgent>())
                    {
                        NavMeshAgent enemyAI = hit.collider.GetComponent<NavMeshAgent>();
                        enemyAI.enabled = false;
                        //set the robot on standby
                        hit.collider.GetComponentInChildren<EnemyAI>().AIStandby();
                        
                        //Begin Hacking
                        //Enable Hacking canvas and interactable
                        hackingCanvas.GetComponent<CanvasGroup>().alpha = 1;
                        hackingCanvas.GetComponent<CanvasGroup>().interactable = true;
                        hackingCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
                        hackingManager.GetComponent<Hacking_Manager>().HackingMiniGame();

                    }
                //  begin hacking sequence, then if hack was successful, then set the robot to a player and zoom camera

                // 1. Begin Hacking Sequence

                // 2. If correct switch robot from an enemy to a player
                // 3. Set robot priority camera to the highest in the scene(this makes it so that the cinemachine camera transition occurs)
                
                //hackingCanvas.SetActive(true);
                //SwitchPerspectives();
                }
        }
    }
}

    public void SwitchPerspectives(){
        hackedRobot.GetComponentInChildren<CinemachineVirtualCamera>().Priority = 100;
        hackedRobot.GetComponent<Movement_Danny>().enabled = true;
        //hackedRobot.GetComponent<Movement>().enabled = false;
        hackedRobot.GetComponentInChildren<EnemyAI>().enabled = false;
        FindObjectOfType<Hacking_Manager>().gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray.origin, ray.direction * 100f);
    }
}
