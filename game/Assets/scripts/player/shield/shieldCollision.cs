using System.Collections;
using UnityEngine;

public class ShieldCollision : MonoBehaviour
{
    public float pushBackForce = 5f; // Total force to be applied
    public float pushBackDuration = 0.2f; // Time over which the force is applied

    private Transform player;
    private Rigidbody2D playerRb;

    private void Start()
    {
        player = transform.parent.parent;
        playerRb = player.GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Vector2 direction = (player.position - collision.transform.position).normalized;
            StartCoroutine(ApplySmoothPush(direction));
        }
    }

    private IEnumerator ApplySmoothPush(Vector2 direction)
    {
        float elapsed = 0f;

        while (elapsed < pushBackDuration)
        {
            // Apply a smaller force incrementally over time for smooth movement
            playerRb.AddForce(direction * (pushBackForce / pushBackDuration) * Time.deltaTime, ForceMode2D.Impulse);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}
