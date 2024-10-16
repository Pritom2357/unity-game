using System.Collections;
using UnityEngine;

public class explosiveObstacle : MonoBehaviour
{
    public float explosionRadius = 5f;
    private Transform player;
    private Animator anim;
    private bool isExploding = false;
    public float explosionDelay = 1f;
    public float explosionForce = 1000f; // Adjust the impulse force
    public int damage = 20; // Amount of damage to apply to the player's health

    public float damageRadius=20f;

    private Rigidbody2D playerRb;
    private playerLevelController playerLevel;
    private Rigidbody2D rb;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        playerRb = player.GetComponent<Rigidbody2D>();
        playerLevel = player.GetComponent<playerLevelController>();
         rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!isExploding && player != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);

            if (distance <= explosionRadius && !isExploding)
            {
                isExploding=true;
                StartCoroutine(TriggerExplosionWithDelay());
            }
            LookAtPlayer();
        }
    }

    private void LookAtPlayer()
    {
        // Get the direction to the player
        Vector2 direction = player.position - transform.position;

        // Determine if the obstacle should be flipped
        if ((direction.x > 0 && transform.localScale.x < 0) || (direction.x < 0 && transform.localScale.x > 0))
        {
            // Flip the x scale while maintaining its absolute value
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    private IEnumerator TriggerExplosionWithDelay()
    {
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(explosionDelay);
        isExploding = true;
        anim.SetBool("isExploding", true);
        Destroy(gameObject, 0.5f); // Destroys the obstacle after playing the explosion animation
        
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);
            if (distance <= damageRadius)
            {
                // Apply force to the player
                Vector2 explosionDirection = (player.position - transform.position).normalized;
                playerRb.AddForce(explosionDirection * explosionForce, ForceMode2D.Impulse);

                // Apply damage to the player
                playerLevel.RegisterHitByObstacle(damage);
            }
        }

    }
}
