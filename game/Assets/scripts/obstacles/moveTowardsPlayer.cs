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

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        hits = FindObjectOfType<hitCounter>();
        playerLevel = FindObjectOfType<playerLevelController>();
        shieldDamage = FindObjectOfType<ShieldDamageController>();
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
            // playerLevelController playerLevel = collision.gameObject.GetComponent<playerLevelController>();
            
            // if (playerLevel != null){
            //     playerLevel.RegisterHit();
            // }

            obstacleHeath health = GetComponent<obstacleHeath>();

            if (health != null && shieldDamage != null)
            {
                int damage = shieldDamage.GetCurrentDamage();
                health.TakeDamage(damage);
                shieldDamage.RegisterHit(); // Register the hit with the ShieldDamageController
            }

            Vector2 bounceDirection = collision.contacts[0].normal;
            rb.velocity = bounceDirection * speed;

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
