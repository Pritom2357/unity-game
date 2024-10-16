using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackholeScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed = 2.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.B))
        // {
        //     gameObject.SetActive(true);
        // }
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

        // Set a low speed for the blackhole
        // float speed = 2.0f;

        // Move the blackhole towards the mouse pointer
        if (Vector3.Distance(transform.position, mousePosition) > 0.05f)
        {
            transform.position += speed * Time.deltaTime * direction;
        }
    }
}
