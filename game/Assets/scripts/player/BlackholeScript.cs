using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackholeScript : MonoBehaviour
{
    [SerializeField] private float speed = 2.0f;

    // Reference to the playerLevelController
    private playerLevelController levelController;

    private void Start()
    {
        // Find the playerLevelController in the scene
        levelController = FindObjectOfType<playerLevelController>();
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
        }
    }

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
