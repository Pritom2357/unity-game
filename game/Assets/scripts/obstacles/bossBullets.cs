using UnityEngine;

public class bossBullet : MonoBehaviour
{
     GameObject target;
    public float speed = 5f;
    private Rigidbody2D bulletRb;
    private CircleCollider2D circleCollider;
    private playerLevelController playerLevel;

    private void Awake(){
        circleCollider = GetComponent<CircleCollider2D>();
        playerLevel = FindObjectOfType<playerLevelController>();
    }

    private void Start(){
        bulletRb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
        bulletRb.velocity = new Vector2(moveDir.x, moveDir.y);
        Destroy(gameObject, 3f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Shield"))
        {
            Debug.Log("Bullet hit the shield.");
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Bullet hit the player.");
            playerLevel.RegisterHitByObstacle(1);
            Destroy(gameObject);
        }
    }
}
