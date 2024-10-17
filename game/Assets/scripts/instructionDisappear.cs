using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instructionDisappear : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        gameObject.SetActive(true);
        StartCoroutine(DisableAfterTime(5f));
    }

    IEnumerator DisableAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
