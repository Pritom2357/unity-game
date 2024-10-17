using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossLevelSender : MonoBehaviour
{
    // Start is called before the first frame update
    private playerLevelController playerLevelController;
    void Start()
    {
        playerLevelController = GetComponent<playerLevelController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerLevelController.GetPlayerLevel() >= 25)
        {
            SceneManager.LoadScene("BossFight");
        }
    }
}
