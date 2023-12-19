using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltBird : SocketMessage
{
    private Animator animator;
    public float duration = 0.5f;
    public float currentValue = 0;

    public override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

    public override void TiltRight()
    {
        base.TiltRight();
        StartCoroutine(MoveValue(1f));
    }

    public override void TiltLeft()
    {
        base.TiltLeft();
        StartCoroutine(MoveValue(-1f));
    }

    IEnumerator MoveValue(float endValue)
    {
        float elapsedTime = 0f;
        float startValue = currentValue;

        while (elapsedTime < duration)
        {
            currentValue = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            animator.SetFloat("Blend", currentValue);
 
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        animator.SetFloat("Blend", endValue);
    }
}
