using UnityEngine;
using Cinemachine;
public class CameraFollow : MonoBehaviour
{
    public Transform target;          // Reference to the player's transform
    public float smoothSpeed = 0.125f; // Camera movement smoothness

    private Vector3 offset = new Vector3(0, 4, -2.2f); 
    [SerializeField] private float rotation = 50;          // Offset between the camera and the player
    [SerializeField] float rotationSpeed;

    [Header("Camera Style")]
    [SerializeField] CinemachineVirtualCamera thirdPersonCamera;    
    [SerializeField] GameObject isometricCamera;
    
    public CameraStyle cameraStyle;

    public enum CameraStyle
    {
        Basic,
        Isometric,
        Hacking
    }

    private void Start()
    {
        // Calculate initial offset between the camera and the player
        //offset = transform.position - target.position;
        transform.position = offset;
        transform.rotation = Quaternion.Euler(rotation, 0, 0);
    }

    private void FixedUpdate()
    {
        if(target == null) return;
        // Calculate the desired position of the camera
        Vector3 desiredPosition = target.position + offset;

        // Smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

    public void CameraAssigned(GameObject _target){
        target = _target.transform;
    }
}