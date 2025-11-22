using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

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
        TouchSimulation.Enable();
        EnhancedTouchSupport.Enable();

        Touch.onFingerDown += Reset;
        
        StartCoroutine(Animate());
    }

    private void OnDisable()
    {
        Touch.onFingerDown -= Reset;
        
        TouchSimulation.Disable();
        EnhancedTouchSupport.Disable();
    }

    private IEnumerator Animate()
    {
        _animating = true;

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

    private void Reset(Finger finger)
    {
        if (!_animating) return;
        transform.localScale = Vector3.one;
        StopAllCoroutines();
    }
}
