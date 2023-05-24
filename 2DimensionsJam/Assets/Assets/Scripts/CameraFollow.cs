using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform orientation;
    [SerializeField] Transform player;
    [SerializeField] Transform playerOjbect;
    [SerializeField] Rigidbody rb;

    [SerializeField] float rotationPower;

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


            playerOjbect.rotation *= Quaternion.AngleAxis(horizonatalInput * rotationPower, Vector3.up);

            playerOjbect.rotation *= Quaternion.AngleAxis(verticalInput * rotationPower, Vector3.right);

            var angles = playerOjbect.localEulerAngles;
            angles.z = 0;

            var angle = playerOjbect.localEulerAngles.x;

            if (angle > 180 && angle < 340)
            {
                angles.x = 340;
            }
            else if (angle < 180 && angle > 40)
            {
                angles.x = 40;
            }

            playerOjbect.localEulerAngles = angles;

            player.rotation = Quaternion.Euler(0, playerOjbect.rotation.eulerAngles.y, 0);

            playerOjbect.localEulerAngles = new Vector3(angles.x, 0, 0);
            //Vector3 inputDirection = orientation.forward * verticalInput + orientation.right * horizonatalInput;

            /*if (inputDirection != Vector3.zero)
            {
                playerOjbect.forward = Vector3.Slerp(playerOjbect.forward, inputDirection.normalized, Time.deltaTime * rotationSpeed);
            }*/
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