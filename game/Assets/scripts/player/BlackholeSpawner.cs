using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackholeSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject blackholePrefab;
    [SerializeField] private GameObject shield;
    [SerializeField] private float blackholeLifetime = 5.0f;
    [SerializeField] private float blackHoleCoolDowntime = 5.0f;
    [SerializeField] private Animator animator;
    private float timeCounter = 0.0f;

    private Vector3 initialScale;
    void Awake()
    {
        
        blackholePrefab.SetActive(false);

    }
    void Start()
    {
        initialScale = blackholePrefab.transform.localScale;
        
    }

    // Update is called once per frame
    void Update()
    {
        blackholeTracker();
    }

    private void blackholeTracker(){
        if(blackholePrefab.activeSelf){
            if(blackholePrefab.transform.localScale.x == 0.0f && animator.GetBool("timeout")){
                animator.SetBool("timeout", false);
                blackholePrefab.SetActive(false);
                blackholePrefab.transform.localScale = initialScale;
                shield.SetActive(true);
                timeCounter = 0.0f;
            }
            if(blackholeLifetime < timeCounter){
                Debug.Log("timeout");
                animator.SetBool("timeout", true);
            }
            
               
        }
        timeCounter += Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.B) && !blackholePrefab.activeSelf && blackHoleCoolDowntime < timeCounter){
            
            blackholePrefab.SetActive(true);
            animator.SetBool("timeout", false);
            blackholePrefab.transform.position = shield.transform.position;
            shieldDeactiavtor();
            timeCounter = 0.0f;
        }
    }
    private void shieldDeactiavtor(){
        if(blackholePrefab.activeSelf){
            shield.SetActive(false);
        }
    }
}
