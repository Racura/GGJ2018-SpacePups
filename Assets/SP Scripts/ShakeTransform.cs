using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeTransform : MonoBehaviour {

    [SerializeField] Transform target;
    [SerializeField] bool startOnEnable;

    [Header("")]
    [SerializeField] RangeF m_shakeLength = 5;
    [SerializeField] Vector3 m_shakeStrength = new  Vector3(1, 1, 0);
    [SerializeField] AnimationCurve m_shakeEasing = AnimationCurve.Linear (0, 1, 1, 0);
    [SerializeField] RangeF m_shakeStep = 0.1f;

    Vector3 m_offset;


    protected virtual void OnEnable () {
        Debug.Log (transform.localPosition);
        if (startOnEnable) StartShake ();
    }
    protected virtual void OnDisable () {        
        AddOffset (-m_offset);
    }


    private void AddOffset(Vector3 offset)
    {
        m_offset            += offset;
        transform.position  += offset;
    }
    public void StartShake () {
        StartShake (target, m_shakeLength.Random ());
    }

    public void StartShake (Transform target, float length) {
        StartCoroutine (ShakeCoroutine (target, length));
    }

    IEnumerator ShakeCoroutine (Transform target, float length) {

        if (target == null) yield break;

        Vector3 p1 = Vector3.zero;
        Vector3 p2 = Vector3.zero;

        Vector3 offset = Vector3.zero;;

        float stepLength = -1f;

        float stepTimer = 0f;
        float timer = 0f;

        while (length <= 0 || timer < length)
        {

            if (stepTimer >= stepLength)
            {
                stepTimer -= stepLength;
                stepLength = m_shakeStep.Random();

                p1 = p2;
                p2 = Vector3.Scale(Random.insideUnitSphere, m_shakeStrength);
            }

            var strength = m_shakeEasing.Evaluate(length <= 0 ? 0f : timer / length);

            var pos = Vector3.Lerp(p1, p2, stepLength > 0 ? stepTimer / stepLength : 1f) * strength;
            AddOffset(pos - offset);
            offset = pos;

            yield return null;

            var delta = Time.deltaTime;

            timer += delta;
            stepTimer += delta;
        }

        AddOffset (-offset);
    }
}
