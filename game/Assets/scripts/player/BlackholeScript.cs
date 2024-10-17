using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackholeScript : MonoBehaviour
{
    [SerializeField] private float speed = 2.0f;

    // Reference to the playerLevelController
    private playerLevelController levelController;
    private obstacleHeath health;

    private void Start()
    {
        // Find the playerLevelController in the scene
        levelController = FindObjectOfType<playerLevelController>();
        health = FindObjectOfType<obstacleHeath>();
    }

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

            // Destroy the enemy object
            Destroy(other.gameObject);
        }else if(other.CompareTag("Boss")){
            obstacleHeath bossHealth = other.GetComponent<obstacleHeath>();

            if(health != null){
                bossHealth.TakeDamage(10);
                Debug.Log("Hit a boss!");
                levelController.RegisterHit(3);
            }
        }
    }

    // private void OnTriggerStay2D(Collider2D other)
    // {
    //     if (other.CompareTag("Boss"))
    //     {
    //         obstacleHeath bossHealth = other.GetComponent<obstacleHeath>();

    //         if (bossHealth != null)
    //         {
    //             bossHealth.TakeDamage(1); // Adjust damage rate as necessary
    //             Debug.Log("Continuously damaging the boss.");

    //             if (levelController != null)
    //             {
    //                 levelController.RegisterHit(1); // Increase player level/score incrementally
    //             }
    //         }
    //     }
    // }

    void FollowMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0; // Set z to 0 to keep it on the same plane

        // Calculate direction towards the mouse pointer
        Vector3 direction = (mousePosition - transform.position).normalized;

        // Move the blackhole towards the mouse pointer
        if (Vector3.Distance(transform.position, mousePosition) > 0.05f)
        {
            transform.position += speed * Time.deltaTime * direction;
        }
    }
}
