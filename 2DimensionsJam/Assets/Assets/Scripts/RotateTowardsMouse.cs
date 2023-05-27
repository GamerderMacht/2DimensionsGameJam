using UnityEngine;

public class RotateTowardsMouse : MonoBehaviour
{
    private void FixedUpdate()
    {
        // Cast a ray from the camera towards the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Check if the ray hits an object
        if (Physics.Raycast(ray, out hit))
        {
            // Calculate the direction from the player to the hit point
            Vector3 direction = hit.point - transform.position;
            direction.y = 0f; // Ensure the player stays upright

            // Rotate the player towards the hit point
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}