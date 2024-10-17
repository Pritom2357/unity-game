using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    public Transform player;
    private BoxCollider2D topWall, bottomWall, leftWall, rightWall;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;

        // Create the colliders
        topWall = new GameObject("TopWall").AddComponent<BoxCollider2D>();
        bottomWall = new GameObject("BottomWall").AddComponent<BoxCollider2D>();
        leftWall = new GameObject("LeftWall").AddComponent<BoxCollider2D>();
        rightWall = new GameObject("RightWall").AddComponent<BoxCollider2D>();

        // Set the layer to PlayerBounds to control what collides with it
        topWall.gameObject.layer = LayerMask.NameToLayer("PlayerBounds");
        bottomWall.gameObject.layer = LayerMask.NameToLayer("PlayerBounds");
        leftWall.gameObject.layer = LayerMask.NameToLayer("PlayerBounds");
        rightWall.gameObject.layer = LayerMask.NameToLayer("PlayerBounds");

        // Make the colliders children of this gameObject to keep the hierarchy clean
        topWall.transform.parent = transform;
        bottomWall.transform.parent = transform;
        leftWall.transform.parent = transform;
        rightWall.transform.parent = transform;

        // Adjust their positions and sizes
        UpdateWalls();
    }

    private void UpdateWalls()
    {
        float screenAspect = (float)Screen.width / Screen.height;
        float camHeight = cam.orthographicSize * 2;

        Vector2 camSize = new Vector2(camHeight * screenAspect, camHeight);

        // Set the size of the walls based on camera size
        topWall.size = new Vector2(camSize.x, 1f);
        bottomWall.size = new Vector2(camSize.x, 1f);
        leftWall.size = new Vector2(1f, camSize.y);
        rightWall.size = new Vector2(1f, camSize.y);

        // Position the walls at the edges of the camera view
        topWall.transform.position = new Vector2(cam.transform.position.x, cam.transform.position.y + camSize.y / 2 + 0.5f);
        bottomWall.transform.position = new Vector2(cam.transform.position.x, cam.transform.position.y - camSize.y / 2 - 0.5f);
        leftWall.transform.position = new Vector2(cam.transform.position.x - camSize.x / 2 - 0.5f, cam.transform.position.y);
        rightWall.transform.position = new Vector2(cam.transform.position.x + camSize.x / 2 + 0.5f, cam.transform.position.y);
    }

    private void Update()
    {
        // Update the walls if the camera or screen size changes (optional)
        UpdateWalls();
    }
}
