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

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        hits = FindObjectOfType<hitCounter>();
        playerLevel = FindObjectOfType<playerLevelController>();
        shieldDamage = FindObjectOfType<ShieldDamageController>();
        playerImpulse = FindObjectOfType<PlayerVelocityController>();
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
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
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Shield") && !isBouncing)
        {
            obstacleHeath health = GetComponent<obstacleHeath>();

            if (health != null && shieldDamage != null)
            {
                int damage = shieldDamage.GetCurrentDamage();
                health.TakeDamage(damage);
                shieldDamage.RegisterHit();
            }

            Vector2 bounceDirection = collision.contacts[0].normal;
            rb.velocity = bounceDirection * speed;
            // playerRb.velocity = -bounceDirection * speed;

            if (playerImpulse != null)
            {
                // float impulseStrength = 50f;
                // Vector2 impulse = bounceDirection * impulseStrength;
                playerImpulse.ApplyVelocity(bounceDirection);
            }

            hits.IncrementHit();
            playerLevel.RegisterHit();
            StartCoroutine(ResumeChaseAfterDelay());
        }
    }


    private IEnumerator ResumeChaseAfterDelay()
    {
        isBouncing = true;
        yield return new WaitForSeconds(bounceTime);
        isBouncing = false;
    }
}
