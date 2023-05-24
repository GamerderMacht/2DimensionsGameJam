using UnityEngine;

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

    [Header("Camera Reference")]
    [SerializeField] Transform cam;


    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Ground Check 
        isGrounded = Physics.CheckSphere(groundPt.position, 0.5f, groundLayer);

        moveVertical = Input.GetAxis("Vertical") * moveSpeed;
        moveHorizontal = Input.GetAxis("Horizontal") * moveSpeed;

        anim.SetFloat("Speed", rb.velocity.magnitude / moveSpeed);
        anim.SetFloat("Horizontal", moveHorizontal);
        anim.SetFloat("Vertical", moveVertical);

        if (Input.GetKeyDown(KeyCode.G))
        {
            anim.SetTrigger("Attack");
        }

    }

    private void FixedUpdate()
    {
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
        }
    }
}