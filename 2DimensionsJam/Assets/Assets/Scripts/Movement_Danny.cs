using UnityEngine;
using Cinemachine;

public class Movement_Danny : MonoBehaviour
{
    Animator anim;
    private Rigidbody rb;

    [Header("Player Movement")]
    [SerializeField] float moveSpeed;
    float moveVertical;
    float moveHorizontal;

    [SerializeField] float newGravity;
    //[SerializeField] float gravityMultiplier = 2f;

    [Header("Ground Reference")]
    public LayerMask groundLayer;
    public Transform groundPt;
    public bool isGrounded;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private Transform cam;
    private bool canMove = true;


    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Ground Check 
        isGrounded = Physics.CheckSphere(groundPt.position, 0.5f, groundLayer);
        moveVertical = Input.GetAxis("Vertical"); //* moveSpeed;
        moveHorizontal = Input.GetAxis("Horizontal"); //* moveSpeed;

        if(moveHorizontal != 0 || moveVertical != 0){
            canMove = true;
        }else{
            canMove = false;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            anim.SetTrigger("Melee");
        }

    }

    private void FixedUpdate()
    {

        //Danny's Script=>
        if(canMove){
            anim.SetBool("Run", true);
            Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
            rb.position += movement * moveSpeed * Time.deltaTime;
            //just implement proper rotation and done
        }
        else{
           anim.SetBool("Run", false);
        }
        

     
     //Buggy Script
     /*
       // Gravity controller
        Physics.gravity = new Vector3(0, newGravity, 0);

        // Player movement 
        rb.velocity = (cam.forward * moveVertical) + (cam.right * moveHorizontal);
        if (moveVertical != 0)
        {
            transform.rotation = cam.rotation;
            Debug.Log("I turn");
        }
        else
        {
            transform.rotation = Quaternion.identity;
            Debug.Log("0");
        } */
    }
}