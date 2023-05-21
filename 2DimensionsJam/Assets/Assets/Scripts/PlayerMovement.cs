using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 PlayerMovementInput;
    private Vector2 PlayerMouseInput;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float sensitivity;
    [SerializeField] private float jumpForce;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }
    private void Update() {
        PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        MovePlayer();
    }


    private void MovePlayer(){
        Vector3 moveVector = transform.TransformDirection(PlayerMovementInput) * playerSpeed;
        rb.velocity = new Vector3(moveVector.x, rb.velocity.y, moveVector.z);

        if(Input.GetKeyDown(KeyCode.Space)){
            rb.AddForce(Vector3.up * jumpForce);
        }
    }
}
