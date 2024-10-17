using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldRotation : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;



    private void Start(){
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Update(){
        // Debug.Log("Shield Rotation");
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z=0;

        Vector3 rotation = mousePos-transform.position;

        float rotz = Mathf.Atan2(rotation.y, rotation.x)*Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotz);
    }
}
