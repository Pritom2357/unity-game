using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public float thresholdX = 2f; // Adjust these thresholds to control the distance before the camera starts following
    public float thresholdY = 2f;

    private void FixedUpdate()
    {
        if (player != null)
        {
            // Calculate the desired position with offset
            Vector3 targetPosition = player.position + offset;

            // Check if the player is near the edges of the camera view
            float distanceX = Mathf.Abs(transform.position.x - targetPosition.x);
            float distanceY = Mathf.Abs(transform.position.y - targetPosition.y);

            if (distanceX > thresholdX || distanceY > thresholdY)
            {
                // Only move the camera if the player is beyond the threshold
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
                transform.position = smoothedPosition;
            }
        }
    }
}
