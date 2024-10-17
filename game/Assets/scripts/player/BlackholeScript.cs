using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackholeScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed = 2.0f;
    private Animator animator;
    private playerLevelController levelController;
    private obstacleHeath health;
    void Start()
    {
        levelController = FindObjectOfType<playerLevelController>();
        health = FindObjectOfType<obstacleHeath>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
           Debug.Log("Blackhole activated");
        }
        FollowMouse();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == null)
        {
            Debug.LogError("Collider2D other is null");
            return;
        }
        if (other.CompareTag("Enemy"))
        {
            // Increase player's score when the blackhole hits an enemy
            if (levelController != null)
            {
                levelController.RegisterHit(3); // Call the method to register a hit
                Debug.Log("Hit an enemy! Score increased.");
            }

            Destroy(other.gameObject);
        }
        else if(other.CompareTag("Boss")){
            obstacleHeath bossHealth = other.GetComponent<obstacleHeath>();

            if(health != null){
                bossHealth.TakeDamage(10);
                Debug.Log("Hit a boss!");
                levelController.RegisterHit(3);
            }
        }
    }

    void FollowMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0; // Set z to 0 to keep it on the same plane

        // Calculate direction towards the mouse pointer
        Vector3 direction = (mousePosition - transform.position).normalized;

        // Set a low speed for the blackhole
        // float speed = 2.0f;

        // Move the blackhole towards the mouse pointer
        if (Vector3.Distance(transform.position, mousePosition) > 0.05f)
        {
            transform.position += speed * Time.deltaTime * direction;
        }
    }
}
