using UnityEngine;

public class Movement_Danny : MonoBehaviour
{
    public float moveSpeed = 5f;     // Player movement speed

    private Rigidbody rb;
    Animator anim;
    private Vector3 moveVec;
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Get input axes
        moveVec.x = Input.GetAxis("Horizontal");
        moveVec.y = Input.GetAxis("Vertical");

        if(moveVec.x != 0 || moveVec.y != 0){
            anim.SetFloat("Speed", 1f);
            MovePlayer();
        }else{
            //anim.SetBool("IsRunning", false);
        }

        anim.SetFloat("Speed", moveVec.sqrMagnitude);
        anim.SetFloat("Horizontal", moveVec.x);
        anim.SetFloat("Vertical", moveVec.y);
        
        if(Input.GetKeyDown(KeyCode.G)){
            anim.SetTrigger("Attack");
        }

    }

    private void MovePlayer(){
        // Calculate movement vector
        Vector3 movement = new Vector3(moveVec.x, 0f, moveVec.y) * moveSpeed * Time.deltaTime;
        
        // Apply movement to the rigidbody
        rb.velocity = movement;
    }
}