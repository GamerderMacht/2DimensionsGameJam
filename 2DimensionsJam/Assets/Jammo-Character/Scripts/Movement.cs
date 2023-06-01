using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    //[SerializeField]private float moveSpeed;
    private Rigidbody rb;
    private Animator anim;
    [SerializeField] private Transform serverPos;
    [SerializeField] private float maxDistanceToPlayer;
    //private NavMeshAgent agent;
    [SerializeField] private float enemySpeed;

    private void Awake() {
        GetRefrences();
    }
    private void FixedUpdate() {
        if(Vector3.Distance(transform.position, FindObjectOfType<Movement_Danny>().transform.position) > maxDistanceToPlayer){
            Transform playerPos = FindObjectOfType<Movement_Danny>().transform;
            MoveTowardTarget(playerPos);
            Debug.Log("Moving toward the player");
        }else{
            MoveTowardTarget(serverPos);
            Debug.Log("Moving toward the server");
        }
    }



    private void GetRefrences(){
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
       // agent = GetComponent<NavMeshAgent>();
    }

    private void MoveTowardTarget(Transform targetPos){
        // I am going to apply root motion to the object so no need for coded movement
        anim.SetFloat("Blend", 0.5f);
       // agent.SetDestination(targetPos.position);
        //agent.speed = enemySpeed;
    }



    
}
