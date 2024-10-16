using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShootingObstacle : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private float maxHealth;
    public float speed = 5f;
    private Transform player;
    public float shootDistance = 10f;
    public GameObject bulletPrefab;
    public GameObject bulletParent;
    public float fireRate = 1f;

    private float nextFireTime;
    public float health = 20f;
    public float healthDepletionRate = 1f;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        maxHealth = health;
        slider = GetComponentInChildren<Slider>();
        updateHealthUI(health, maxHealth);
    }

    private void Update()
    {
        if (player != null)
        {
            float distance = Vector2.Distance(player.position, transform.position);

            if (distance > shootDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }else if(distance <= shootDistance && Time.time >= nextFireTime){
                Instantiate(bulletPrefab, bulletParent.transform.position, Quaternion.identity);
                nextFireTime = Time.time+fireRate;
            }

            health -= healthDepletionRate * Time.deltaTime;
            updateHealthUI(health, maxHealth);

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    

    private void updateHealthUI(float currentHealth, float maxHealth){
            if(slider!=null){
                slider.value = currentHealth/maxHealth;
            }
    }
}

