using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class increaseSize : MonoBehaviour
{
    private playerLevelController level;
    private Transform shieldTransform;
    public float sizeIncrement = 0.1f;

    private void Awake(){
        level = FindObjectOfType<playerLevelController>();
        shieldTransform = GameObject.FindWithTag("Shield").transform;
    }

    private void Start(){
        if(level != null){
            level.onLevelUp += IncreaseShieldSize;
        }
    }

    private void OnDestroy(){
        if(level != null){
            level.onLevelUp -= IncreaseShieldSize;
        }
    }

    private void IncreaseShieldSize(int newLevel){
        if(shieldTransform != null){
            Vector3 newSize = new Vector3(shieldTransform.localScale.x, shieldTransform.localScale.y + sizeIncrement, shieldTransform.localScale.z);

            shieldTransform.localScale = newSize;
            Debug.Log($"Shield size increased to: {shieldTransform.localScale}");
        }
    }
}
