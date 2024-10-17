using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackholeSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject blackholePrefab;
    [SerializeField] private GameObject shield;
    [SerializeField] private float blackholeLifetime = 5.0f;
    private float timeCounter = 0.0f;
    void Awake()
    {
        blackholePrefab.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        blackholeTracker();
    }

    private void blackholeTracker(){
        if(blackholePrefab.activeSelf){
            if(blackholeLifetime < timeCounter){
                blackholePrefab.SetActive(false);
                shield.SetActive(true);
                timeCounter = 0.0f;
            }else{
                timeCounter += Time.deltaTime;
            }
        }
        if(Input.GetKeyDown(KeyCode.B)){
            
            blackholePrefab.SetActive(true);
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
