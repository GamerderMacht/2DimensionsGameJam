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
    [SerializeField] bool canMove = true;

    [SerializeField] float newGravity;

    [Header("Ground Reference")]
    public LayerMask groundLayer;
    public GameObject groundPt;
    public bool isGrounded;
    [SerializeField] private float rotationSpeed;

    [Header("Camera References")]
    public GameObject cam;
    public GameObject thirdCam;
    public GameObject isoCam;
    [SerializeField] private GameObject explosionPrefab;
    float timetoDie = 10;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponentInParent<Rigidbody>();

        groundPt = GameObject.Find("GroundPt");

        cam = Camera.main.gameObject;
        thirdCam = GameObject.Find("Camera_Types");
        isoCam = GameObject.Find("IsoMetric");
        Debug.Log(isoCam + " HERERERERERE");
    }

    private void Update()
    {
        // Ground Check 
        isGrounded = Physics.CheckSphere(groundPt.transform.position, 0.5f, groundLayer);
        moveVertical = Input.GetAxis("Vertical") * moveSpeed;
        moveHorizontal = Input.GetAxis("Horizontal") * moveSpeed;
        canMove = true;
        /*if(moveHorizontal == 0 || moveVertical == 0)
        {
            canMove = true;
        }
        else
        {
            canMove = false;
        }*/

        if (Input.GetKeyDown(KeyCode.G))
        {
            anim.SetTrigger("Melee");
        }

        timetoDie -= Time.deltaTime;
        Debug.Log(timetoDie);

        if (Input.GetKey(KeyCode.X) || timetoDie <= 0)
        {
            timetoDie = 10;
            Debug.Log("X was pressed");
            Destroy(gameObject);
            thirdCam.SetActive(false);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            transform.GetChild(1).GetComponent<SphereCollider>().enabled = true;
            transform.GetChild(1).GetComponent<weapon_new>().enabled = true;
            FindObjectOfType<Hacking_Manager>().TurnIsoCameraOn();

            //isoCam.SetActive(true);
            // Particle System
        }
        

    }

    private void FixedUpdate()
    {

        //Danny's Script=>
        if(canMove)
        {
            anim.SetFloat("Blend", 0.5f);
            //Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
            rb.velocity = (cam.transform.forward * moveVertical) + (cam.transform.right * moveHorizontal);
            if (moveVertical != 0)
            {
                transform.rotation = cam.transform.rotation;
                Debug.Log("I turn");
            }
            else
            {
                transform.rotation = Quaternion.identity;
                Debug.Log("0");
            }
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