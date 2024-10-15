using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class hitCounter : MonoBehaviour
{
    public TextMeshProUGUI hitCounterText;
    private int hitCount = 0;

    public void IncrementHit(){
        hitCount++;
        UpdateHitCount();
    }

    private void UpdateHitCount(){
        hitCounterText.text = "Hit: "+ hitCount.ToString();
    }
}
