using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("Third Person viewpoint references")]
    public CinemachineFreeLook freeLook; 

    public GameObject orientation;
    public GameObject player;
    public GameObject playerObject;

    public float rotationSpeed;

    [Header("Combat viewpoint references")]
    public GameObject combatLookAt;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        freeLook = GetComponentInChildren<CinemachineFreeLook>();

        orientation = GameObject.FindGameObjectWithTag("Orientation");
        player = GameObject.FindGameObjectWithTag("Player");
        playerObject = GameObject.FindGameObjectWithTag("PlayerObject");

        combatLookAt = GameObject.FindGameObjectWithTag("Player");
        freeLook.Follow = player.transform;
        freeLook.LookAt = player.transform;
        freeLook.m_LookAt = player.transform;
    }

    private void Update()
    {
        // Camera Orientation
        Vector3 viewDirection = player.transform.position - new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
        orientation.transform.forward = viewDirection.normalized;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputDirection = orientation.transform.forward * verticalInput + orientation.transform.right * horizontalInput;

        if (inputDirection != Vector3.zero)
        {
            playerObject.transform.forward = Vector3.Slerp(playerObject.transform.forward, inputDirection.normalized, Time.deltaTime * rotationSpeed);
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            Vector3 viewCombatDirection = combatLookAt.transform.position - new Vector3(transform.position.x, combatLookAt.transform.position.z);
            orientation.transform.forward = viewCombatDirection.normalized;

            playerObject.transform.forward = viewCombatDirection.normalized;
        }
    }
}
