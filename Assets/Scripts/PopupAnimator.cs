using System;
using System.Collections;
using UnityEngine;

public class PopupAnimator : MonoBehaviour
{
    [SerializeField] private float delay = 2f;
    [SerializeField] private float duration = 0.25f;
    
    private readonly AnimationCurve _curve = new(
        new Keyframe(0f, 0f, 0, 4f),
        new Keyframe(1f, 1f, -1f, 0f)
        );

    private bool _animating;
    
    private void OnEnable()
    {
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        _animating = true;

        StartCoroutine(ListenForReset());
        
        transform.localScale = Vector3.zero;
        
        yield return new WaitForSeconds(delay);

        var journey = 0f;
        while (journey < duration)
        {
            journey += Time.deltaTime;
            var percent = Mathf.Clamp01(journey / duration);
            var curveValue = _curve.Evaluate(percent);
            var scale = Mathf.LerpUnclamped(0.75f, 1f, curveValue);
            transform.localScale = Vector3.one * scale;
            yield return null;
        }
        
        _animating = false;
    }

    private IEnumerator ListenForReset()
    {
        while (_animating)
        {
            if (Input.anyKeyDown) Reset();
            yield return null;
        }
    }

    public void Reset()
    {
        if (!_animating) return;
        transform.localScale = Vector3.one;
        StopAllCoroutines();
    }
}
