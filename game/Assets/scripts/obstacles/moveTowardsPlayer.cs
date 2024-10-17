using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float speed = 2f;
    private Rigidbody2D rb;
    private Transform player;
    private bool isBouncing = false;
    public float bounceTime = 2f;
    private hitCounter hits;
    private ShieldDamageController shieldDamage;
    private playerLevelController playerLevel;
    private PlayerVelocityController playerImpulse;
    private Rigidbody2D playerRb;
    private Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        hits = FindObjectOfType<hitCounter>();
        playerLevel = FindObjectOfType<playerLevelController>();
        shieldDamage = FindObjectOfType<ShieldDamageController>();
        playerImpulse = FindObjectOfType<PlayerVelocityController>();
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (player != null && !isBouncing)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * speed;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Shield") || collision.gameObject.CompareTag("Player"))
        {
            anim.SetBool("attack", true);

            if (collision.gameObject.CompareTag("Shield") && !isBouncing)
            {
                obstacleHeath health = GetComponent<obstacleHeath>();

                if (health != null && shieldDamage != null)
                {
                    int damage = shieldDamage.GetCurrentDamage();
                    health.TakeDamage(damage);
                    shieldDamage.RegisterHit();
                }

                // Check if there are any contacts before accessing the first one
                if (collision.contacts.Length > 0)
                {
                    Vector2 bounceDirection = collision.contacts[0].normal;
                    rb.velocity = bounceDirection * speed;

                    if (playerImpulse != null)
                    {
                        playerImpulse.ApplyVelocity(bounceDirection);
                    }
                }

                hits.IncrementHit();
                playerLevel.RegisterHit();
                StartCoroutine(ResumeChaseAfterDelay());
            }

            if (collision.gameObject.CompareTag("Player"))
            {
                playerLevel.RegisterHitByObstacle(1);
            }
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Shield") || collision.gameObject.CompareTag("Player"))
        {
            anim.SetBool("attack", false);
        }
    }

    private IEnumerator ResumeChaseAfterDelay()
    {
        isBouncing = true;
        yield return new WaitForSeconds(bounceTime);
        isBouncing = false;
    }
}
