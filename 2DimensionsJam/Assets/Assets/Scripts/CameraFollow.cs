using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform orientation;
    [SerializeField] Transform player;
    [SerializeField] Transform playerOjbect;
    [SerializeField] Rigidbody rb;

    [SerializeField] float rotationSpeed;

    [Header("Camera Style")]
    [SerializeField] GameObject thirdPersonCamera;    
    [SerializeField] GameObject isometricCamera;
    
    public CameraStyle cameraStyle;

    public enum CameraStyle
    {
        Basic,
        Isometric
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Input keys to switch between Camera Styles
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchCameraStyle(CameraStyle.Basic);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchCameraStyle(CameraStyle.Isometric);
        }

        // Rotate around Orientation object 
        Vector3 viewDirection = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDirection.normalized;

        // Rotate around Player Object
        if (cameraStyle == CameraStyle.Basic || cameraStyle == CameraStyle.Isometric)
        {
            float horizonatalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 inputDirection = orientation.forward * verticalInput + orientation.right * horizonatalInput;

            if (inputDirection != Vector3.zero)
            {
                playerOjbect.forward = Vector3.Slerp(playerOjbect.forward, inputDirection.normalized, Time.deltaTime * rotationSpeed);
            }
        }
    }

    void SwitchCameraStyle(CameraStyle updatedStyle)
    {
        thirdPersonCamera.SetActive(false);
        isometricCamera.SetActive(false);

        if (updatedStyle == CameraStyle.Basic)
        {
            thirdPersonCamera.SetActive(true);
        }
        if (updatedStyle == CameraStyle.Isometric)
        {
            isometricCamera.SetActive(true);
        }

        cameraStyle = updatedStyle;
    }
}