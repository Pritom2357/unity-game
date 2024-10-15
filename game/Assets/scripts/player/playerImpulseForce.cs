using UnityEngine;

public class PlayerVelocityController : MonoBehaviour
{
    private Rigidbody2D playerRb;
    public float velocityMultiplier = 5f; // Adjust this to increase the effect of the velocity change

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    public void ApplyVelocity(Vector2 direction)
    {
        if (playerRb != null)
        {
            Vector2 adjustedVelocity = direction * velocityMultiplier;
            playerRb.velocity = adjustedVelocity;
            Debug.Log($"Applied velocity: {adjustedVelocity}");
        }
        else
        {
            Debug.LogWarning("Rigidbody2D is not found on the player.");
        }
    }
}
