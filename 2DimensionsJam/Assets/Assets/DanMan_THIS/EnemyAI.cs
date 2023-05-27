using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;
    private bool seesPlayer;
    private Vector3 serverPos;
    [SerializeField] private float attackDistance;

    private void Awake() {
        agent = GetComponentInParent<NavMeshAgent>();
        anim = GetComponentInParent<Animator>();
        serverPos = GameObject.Find("Server").transform.position;
        AIStandby();
    }


    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player"){
            agent.SetDestination(other.gameObject.transform.position);
            seesPlayer = true;
        }
    }

    private void Update() {
        
            

        if(!seesPlayer){
            if(GameObject.Find("Server") == null) return;
            if(agent.enabled == true) agent.SetDestination(GameObject.Find("Server").transform.position);
            
        }

        if(Vector3.Distance(transform.position, serverPos) <= attackDistance){
            anim.SetFloat("Blend", 0f);
            agent.isStopped = true;
            anim.SetTrigger("Melee");
        }

        if(FindAnyObjectByType<Movement_Danny>() != null && Vector3.Distance(transform.position, serverPos) <= attackDistance){
            anim.SetFloat("Blend", 0f);
            agent.isStopped = true;
            anim.SetTrigger("Melee");
        }
    }

    public void AIStandby()
    {
        if(!agent.enabled)
        {
            anim.SetFloat("Blend", 0f);
        }
        else
        {
            anim.SetFloat("Blend", 0.35f);
        }
    }
}
