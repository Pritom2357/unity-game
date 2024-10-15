using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosiveObstacle : MonoBehaviour
{
    public float explosionRadius = 5f;
    private Transform player;
    private Animator anim;
    private bool isExploding = false;

    private void Awake(){
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    private void Update(){
        if(!isExploding && player!=null){
            float distance = Vector2.Distance(transform.position, player.position);

            if(distance <= explosionRadius){
                triggerExplosion();
            }
        }
    }

    private void triggerExplosion(){
        isExploding = true;
        anim.SetBool("isExploding", true);
        Destroy(gameObject, 1f);
    }
}
