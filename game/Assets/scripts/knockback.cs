using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class knockback : MonoBehaviour
{
    public float knockbackTime = 0.2f;
    public float hitDirectionForce = 10f;
    public float constForce = 5f;
    public float inputForce = 7.5f;
    private Rigidbody2D rb;
    private Coroutine knockbackCoroutine;
    public AnimationCurve knockbackCurve;

    public bool isBeingKnockBack  {get; private set;}

    private void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    public IEnumerator knockbackAction(Vector2 hitDirection, Vector2 constantForceDirection, float inputDirection){
        isBeingKnockBack = true;

        Vector2 _hitForce;
        Vector2 _constantForce;
        Vector2 _knockbackForce;
        Vector2 _combinedForce;
        float _time = 0f;

        _hitForce = hitDirection*hitDirectionForce;
        _constantForce = constantForceDirection*constForce;

        float _elapsedTime = 0f;
        while(_elapsedTime < knockbackTime){
            _elapsedTime += Time.fixedDeltaTime;
            
            _time = Time.fixedDeltaTime;
            _hitForce = hitDirection*hitDirectionForce*knockbackCurve.Evaluate(_time);
            // combine hitforce and constantforce
            _knockbackForce = _hitForce + _constantForce;
            

            if(inputDirection != 0){
                _combinedForce = _knockbackForce + new Vector2(inputDirection, 0f);
            }else{
                _combinedForce = _knockbackForce;
            }

            rb.velocity = _combinedForce;

            yield return new WaitForFixedUpdate();
        }
        isBeingKnockBack = false;
    }

    public void callKnockback(Vector2 hitDirection, Vector2 constantForceDirection, float inputDirection){
        knockbackCoroutine = StartCoroutine(knockbackAction(hitDirection, constantForceDirection, inputDirection));
    }
}
