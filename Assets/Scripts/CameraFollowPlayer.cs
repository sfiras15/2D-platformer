using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    private Transform player; // Reference to the player's transform
    public Vector3 offset;   // Offset distance between the player and camera
    public float smoothSpeed = 0.125f; // Smoothing speed for the camera movement

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }
    void LateUpdate()
    {
        // Define the target position based on the player's position and the offset
        Vector3 targetPosition = player.position + offset;

        // Maintain the original z position of the camera
        targetPosition.z = transform.position.z;

        // Smoothly move the camera towards the target position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

        // Update the camera's position with the new x and y, but keep the z the same
        transform.position = smoothedPosition;
    }
}

