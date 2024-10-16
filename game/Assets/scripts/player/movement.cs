using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private knockback kb;

    private void Awake(){
        rb=GetComponent<Rigidbody2D>();
        kb=GetComponent<knockback>();
    }

    private void Update(){
        if(!kb.isBeingKnockBack){
            Move();
        }
            
    }

    private void Move(){
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 moveDirection = new Vector2(moveX, moveY).normalized;
        rb.velocity=moveDirection*speed;
    }
}
