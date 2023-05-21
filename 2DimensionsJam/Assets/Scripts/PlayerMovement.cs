using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    Vector3 moveDirection;
    public Animator animator;

    [Header("Main Camera:")]
    [SerializeField] GameObject mainCamera;
    [SerializeField] Transform cameraHolder;

    [Header("Ground Check:")]
    [SerializeField] float playerHeight;
    [SerializeField] Transform groundPt;
    [SerializeField] bool isGrounded;
    [SerializeField] LayerMask groundLayer;

    [Header("Movement:")]
    [SerializeField] float moveForward;
    [SerializeField] float moveSide;
    [SerializeField] float currentSpeed;
    [SerializeField] float walkSpeed;
    [SerializeField] bool isWalking = false;
    [SerializeField] float sprintSpeed;
    [SerializeField] bool isRunning = false;
    [SerializeField] float backwardRunSpeed;
    [SerializeField] bool isStrafing = false;

    [Header("Slope Handling:")]
    [SerializeField] float maxSlopeAngle;
    private RaycastHit slopeHit;
    [SerializeField] bool exitingSlope;

    [Header("Jumping:")]
    [SerializeField] bool jumpKeyPressed;
    [SerializeField] float idleJumpForce = 2f;
    [SerializeField] float runJumpForce = 4f;
    [SerializeField] float newGravity = -9.81f;
    [SerializeField] float gravityMultiplier = 2f;
    [SerializeField] bool isJumping = false;

    [Header("Idle Jump Cooldown:")]
    [SerializeField] bool idleJumpReady;
    [SerializeField] float idleJumpCoolDown = 1.7f;
    [SerializeField] float idleJumpCoolDownCurrentTime = 0f;

    [Header("Run Jump Cooldown:")]
    [SerializeField] bool runJumpReady;
    [SerializeField] float runJumpCoolDown = 1.5f;
    [SerializeField] float runJumpCoolDownCurrentTime = 0f;

    [Header("Footstep Audio:")]
    [SerializeField] bool useFootsteps = true;
    [SerializeField] float baseStepSpeed = 0.5f;
    [SerializeField] float sprintStepMultiplier = 0.6f;
    [SerializeField] AudioSource footstepAudioSource = default;
    [SerializeField] AudioClip[] grassClips = default;
    [SerializeField] AudioClip[] floorClips = default;
    [SerializeField] AudioClip[] carpetClips = default;
    [SerializeField] float footStepTimer = 0;
    [SerializeField] float audioRaycast = 1;

    private float GetCurrentOffset => isRunning ? baseStepSpeed + sprintStepMultiplier : baseStepSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        currentSpeed = walkSpeed;

        // Gets reference to main camera 
        if (mainCamera == null)
        {
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
    }

    private void Update()
    {
        moveForward = Input.GetAxis("Vertical") * currentSpeed;
        moveSide = Input.GetAxis("Horizontal") * currentSpeed;
        jumpKeyPressed = Input.GetButtonDown("Jump");

        rb.mass = 1f;
        isGrounded = Physics.CheckSphere(groundPt.position, 0.5f, groundLayer);

        // Upright Movement 

        // Walking forward 
        if (Input.GetKey(KeyCode.W))
        {
            isWalking = true;
            animator.SetBool("isIdle", false);
            currentSpeed = walkSpeed;
            animator.SetBool("isWalking", true);
            FootstepsHandler();
            //Debug.Log(grassClips + " " + floorClips + " " + carpetClips);
        }
        else if (!Input.GetKey(KeyCode.W))
        {
            isWalking = false;
            animator.SetBool("isWalking", false);
            animator.SetBool("isIdle", true);
        }

        // Running forward
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
        {
            isWalking = false;
            isRunning = true;
            currentSpeed = sprintSpeed;
            animator.SetBool("isBackward", false);
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", true);
            FootstepsHandler();
            //Debug.Log(grassClips + " " + floorClips + " " + carpetClips);
        }
        else if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = false;
            currentSpeed = walkSpeed;
            animator.SetBool("isRunning", false);
        }
        else if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = false;
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", true);
        }
        // Ensure holding shift after letting go of W key does not continue the running forward animation
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = false;
            animator.SetBool("isRunning", false);
            animator.SetBool("isIdle", true);
        }

        // Walking Backward Animation
        if (Input.GetKey(KeyCode.S))
        {
            isWalking = true;
            animator.SetBool("isIdle", false);
            currentSpeed = walkSpeed;
            animator.SetBool("isNegative", true);
            FootstepsHandler();
            //Debug.Log(grassClips + " " + floorClips + " " + carpetClips);
        }
        else if (!Input.GetKey(KeyCode.S))
        {
            isWalking = false;
            animator.SetBool("isNegative", false);
            animator.SetBool("isIdle", true);
        }

        // Running Backward Animation 
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.LeftShift))
        {
            isWalking = false;
            isRunning = true;
            animator.SetBool("isBackward", true);
            animator.SetBool("isRunning", false);
            animator.SetBool("isNegative", false);
            currentSpeed = backwardRunSpeed;
            FootstepsHandler();
            //Debug.Log(grassClips + " " + floorClips + " " + carpetClips);
        }
        else if (!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = false;
            currentSpeed = walkSpeed;
            animator.SetBool("isBackward", false);
        }
        else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = walkSpeed;
            animator.SetBool("isBackward", false);
            animator.SetBool("isNegative", true);
        }
        // Ensure holding shift after letting go of S key does not continue the running backward animation
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            //isRunning = false;
            animator.SetBool("isBackward", false);
            animator.SetBool("isIdle", true);
        }

        // Right Strafe
        if (Input.GetKey(KeyCode.D))
        {
            isWalking = false;
            isStrafing = true;
            currentSpeed = walkSpeed;
            animator.SetBool("isRight", true);
            FootstepsHandler();
            //Debug.Log(grassClips + " " + floorClips + " " + carpetClips);
        }
        else if (!Input.GetKey(KeyCode.D))
        {
            isStrafing = false;
            animator.SetBool("isRight", false);
        }

        // Left Strafe 
        if (Input.GetKey(KeyCode.A))
        {
            isWalking = false;
            isStrafing = true;
            currentSpeed = walkSpeed;
            animator.SetBool("isLeft", true);
            FootstepsHandler();
            //Debug.Log(grassClips + " " + floorClips + " " + carpetClips);
        }
        else if (!Input.GetKey(KeyCode.A))
        {
            //isStrafing = false;
            animator.SetBool("isLeft", false);
        }

        // On Slope
        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection() * currentSpeed * 20f, ForceMode.Force);
            // Keeps player constantly on the slope
            if (rb.velocity.y > 0)
            {
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }

            if (rb.velocity.magnitude > currentSpeed)
            {
                rb.velocity = rb.velocity.normalized * currentSpeed;
            }
        }
        rb.useGravity = !OnSlope();

        // When idle jump is ready 
        if (idleJumpCoolDownCurrentTime >= idleJumpCoolDown)
        {
            idleJumpReady = true;
        }
        else
        {
            idleJumpReady = false;
            idleJumpCoolDownCurrentTime += Time.deltaTime;
            idleJumpCoolDownCurrentTime = Mathf.Clamp(idleJumpCoolDownCurrentTime, 0f, idleJumpCoolDown);
        }

        //Makes Character Jump in Idle 
        if (isGrounded && jumpKeyPressed && !isWalking && !isRunning && !isStrafing && !Input.GetKey(KeyCode.W) && idleJumpReady)
        {
            isJumping = true;
            exitingSlope = true;
            StartCoroutine(IdleJump());
            animator.SetBool("isJumped", true);
            idleJumpCoolDownCurrentTime = 0f;
            Debug.Log("Player is idle jumping");
        }
        else if (isGrounded || !jumpKeyPressed)
        {
            isJumping = false;
            animator.SetBool("isJumped", false);
        }
        else if (isRunning && jumpKeyPressed)
        {
            isJumping = false;
        }

        IEnumerator IdleJump()
        {
            yield return new WaitForSeconds(.25f);
            rb.AddForce(transform.up * idleJumpForce * rb.mass, ForceMode.Impulse);
        }

        // When idle jump is ready 
        if (runJumpCoolDownCurrentTime >= runJumpCoolDown)
        {
            runJumpReady = true;
        }
        else
        {
            runJumpReady = false;
            runJumpCoolDownCurrentTime += Time.deltaTime;
            runJumpCoolDownCurrentTime = Mathf.Clamp(runJumpCoolDownCurrentTime, 0f, runJumpCoolDown);
        }

        // Make character run jump 
        if (isRunning && isGrounded && jumpKeyPressed && runJumpReady)
        {
            rb.AddForce(transform.up * runJumpForce * rb.mass, ForceMode.Impulse);
            animator.SetBool("isJumping", true);
            runJumpCoolDownCurrentTime = 0f;
            Debug.Log("Player is run jumping");
        }
        else if (isRunning && !jumpKeyPressed)
        {
            animator.SetBool("isJumping", false);
        }
    }

    private void FootstepsHandler()
    {
        if (!isGrounded)
        {
            return;
        }

        footStepTimer -= Time.deltaTime;

        if (footStepTimer <= 0)
        {
            if (Physics.Raycast(groundPt.transform.position, Vector3.down, out RaycastHit hit, audioRaycast))
            {
                switch (hit.collider.tag)
                {
                    case "Organic":
                        footstepAudioSource.PlayOneShot(grassClips[Random.Range(0, grassClips.Length - 1)]);
                        break;
                    case "Hard floor":
                        footstepAudioSource.PlayOneShot(floorClips[Random.Range(0, floorClips.Length - 1)]);
                        break;
                    case "Carpet":
                        footstepAudioSource.PlayOneShot(carpetClips[Random.Range(0, carpetClips.Length - 1)]);
                        break;
                    default:
                        footstepAudioSource.PlayOneShot(grassClips[Random.Range(0, grassClips.Length - 1)]);
                        break;
                }
            }
            footStepTimer = GetCurrentOffset;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, audioRaycast);
    }


    private void FixedUpdate()
    {
        // Adds gravity when player is in the air so the player can come down at an increase rate
        if (rb.velocity.y < -0.05f)
        {
            rb.AddForce(Physics.gravity * gravityMultiplier, ForceMode.Acceleration);
        }
        else
        {
            Physics.gravity = new Vector3(0, newGravity, 0);
            rb.mass = 1;
        }

        rb.velocity = (transform.forward * moveForward) + (transform.right * moveSide) + (transform.up * rb.velocity.y);
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }
        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }
}
