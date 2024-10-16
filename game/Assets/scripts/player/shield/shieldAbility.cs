using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAbilities : MonoBehaviour
{
    public Transform blackhole;
    public float blackholeSpeed = 5f;
    public KeyCode blackholeKey = KeyCode.Q; // Key to activate blackhole

    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 10f;
    public KeyCode shootBulletKey = KeyCode.E; // Key to shoot bullet

    public KeyCode timeFreezeKey = KeyCode.R; // Key to activate time freeze
    public float timeFreezeDuration = 5f;
    private bool isTimeFrozen = false;

    private void Update()
    {
        if (Input.GetKeyDown(blackholeKey))
        {
            StartBlackhole();
        }

        if (Input.GetKeyDown(shootBulletKey))
        {
            ShootBullet();
        }

        if (Input.GetKeyDown(timeFreezeKey) && !isTimeFrozen)
        {
            StartCoroutine(TimeFreeze());
        }
    }

    private void StartBlackhole()
    {
        StartCoroutine(MoveBlackholeToMouse());
    }

    private IEnumerator MoveBlackholeToMouse()
    {
        while (Input.GetKey(blackholeKey))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            blackhole.position = Vector3.MoveTowards(blackhole.position, mousePosition, blackholeSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void ShootBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * bulletSpeed;
    }

    private IEnumerator TimeFreeze()
    {
        isTimeFrozen = true;

        // Find all obstacles and disable their movement.
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        List<Rigidbody2D> obstacleRigidbodies = new List<Rigidbody2D>();
        foreach (var obstacle in obstacles)
        {
            Rigidbody2D rb = obstacle.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                obstacleRigidbodies.Add(rb);
                rb.isKinematic = true; // Stops the obstacle's movement
            }
        }

        yield return new WaitForSeconds(timeFreezeDuration);

        // Re-enable the movement of obstacles.
        foreach (var rb in obstacleRigidbodies)
        {
            rb.isKinematic = false;
        }

        isTimeFrozen = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle") && Input.GetKey(blackholeKey))
        {
            Destroy(collision.gameObject);
        }
    }
}
