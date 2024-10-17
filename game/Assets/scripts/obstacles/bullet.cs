using UnityEngine;

public class Bullet : MonoBehaviour
{
     GameObject target;
    public float speed = 5f;
    private Rigidbody2D bulletRb;
    private BoxCollider2D boxCollider;
    private playerLevelController playerLevel;

    private void Awake(){
        boxCollider = GetComponent<BoxCollider2D>();
        playerLevel = FindObjectOfType<playerLevelController>();
    }

    private void Start(){
        bulletRb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
        bulletRb.velocity = new Vector2(moveDir.x, moveDir.y);
        Destroy(gameObject, 3f);
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Shield")){
            Destroy(gameObject);
        }

        if(collision.gameObject.CompareTag("Player")){
            playerLevel.RegisterHitByObstacle(1);
            Destroy(gameObject);
        }
    }
}
