using System.Collections;
using UnityEngine;

public class ShieldDamageController : MonoBehaviour
{
    public int baseDamage = 60;
    public int minDamage = 10;
    public float recoveryRate = 1f;
    public float hitTimeThreshold = 3f;
    private int currentDamage;
    private bool isRecovering = true;
    private float lastHitTime = -1f;

    private void Awake()
    {
        currentDamage = baseDamage;
    }

    private void Update()
    {
        if (isRecovering)
        {
            RecoverDamage();
        }
        Debug.Log(currentDamage);
    }

    public int GetCurrentDamage()
    {
        return currentDamage;
    }

    public void RegisterHit()
    {
        float currentTime = Time.time;
        if (lastHitTime > 0 && currentTime - lastHitTime <= hitTimeThreshold)
        {
            DecreaseDamage();
        }

        lastHitTime = currentTime;
        isRecovering = false;
        StartCoroutine(ResumeDamageRecovery());
    }

    private void DecreaseDamage()
    {
        currentDamage = Mathf.Max(minDamage, currentDamage - 1);
    }

    private void RecoverDamage()
    {
        if (currentDamage < baseDamage)
        {
            currentDamage += Mathf.CeilToInt(recoveryRate * Time.deltaTime);
            currentDamage = Mathf.Clamp(currentDamage, minDamage, baseDamage);
        }
    }

    private IEnumerator ResumeDamageRecovery()
    {
        yield return new WaitForSeconds(3f);
        isRecovering = true;
    }
}
