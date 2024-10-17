using UnityEngine;

public class BossController : MonoBehaviour
{
    public float followSpeed = 3.0f; // Speed at which the boss follows the player
    public float viewDistance = 10.0f; // Distance at which the boss can see the player
    public float shootingRange = 5.0f; // Distance at which the boss starts shooting
    public GameObject projectilePrefab; // The projectile that the boss shoots
    public float fireRate = 2.0f; // Time interval between shots
    public Transform bulletPoint; // Reference to the bullet spawn point

    private Transform playerTransform;
    private bool isFighting = false;
    private float nextFireTime = 0f;
    private playerLevelController playerLevel;
    private spawnManager spawner;
    private obstacleHeath health;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerLevel = playerTransform.GetComponent<playerLevelController>();
        spawner = FindObjectOfType<spawnManager>();
        health = GetComponent<obstacleHeath>();

        // Ensure bulletPoint is set and display a warning if it's not
        if (bulletPoint == null)
        {
            Debug.LogWarning("BulletPoint is not assigned. Please assign it in the inspector.");
        }
    }

    private void Update()
    {
        if (playerTransform == null)
            return;
            
        LookAtPlayer();
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        // Start fighting when the player is within view distance
        if (distanceToPlayer <= viewDistance)
        {
            isFighting = true;
            // if(spawner != null){
            //     spawner.StopSpawning();
            // }else{
            //     isFighting = false;
            //     if(spawner!=null){
            //         spawner.ResumeSpawning();
            //     }
            // }
        }

        if (isFighting)
        {
            // Move towards the player if they are outside the shooting range
            if (distanceToPlayer > shootingRange)
            {
                FollowPlayer();
            }
            // Shoot at the player when within shooting range
            else if (Time.time >= nextFireTime)
            {
                ShootProjectile();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    private void ShootProjectile()
    {
        if (bulletPoint == null)
        {
            Debug.LogError("BulletPoint is not set!");
            return;
        }

        // Create a projectile and direct it towards the player
        Vector3 direction = (playerTransform.position - bulletPoint.position).normalized;
        GameObject projectile = Instantiate(projectilePrefab, bulletPoint.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = direction * 5f; // Adjust speed as needed
        Debug.Log("Boss shoots a projectile towards the player");
    }

    private void LookAtPlayer()
    {
        // Get the direction to the player
        Vector2 direction = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;

        // Determine if the obstacle should be flipped
        if ((direction.x > 0 && transform.localScale.x < 0) || (direction.x < 0 && transform.localScale.x > 0))
        {
            // Flip the x scale while maintaining its absolute value
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    private void FollowPlayer()
    {
        // Move towards the player's position
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, followSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerLevel.RegisterHitByObstacle(10);
        }
        else if(collision.gameObject.tag == "Shield")
        {
            if(health != null)
            {
                health.TakeDamage(2);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, viewDistance);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}