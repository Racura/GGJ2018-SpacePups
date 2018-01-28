using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationShakeTransform : MonoBehaviour {

    [SerializeField] Transform target;
    [SerializeField] bool startOnEnable;

    [Header("")]
    [SerializeField] RangeF m_shakeLength = 5;
    [SerializeField] RangeF m_shakeStrength = new RangeF(-30, 30);
    [SerializeField] AnimationCurve m_shakeEasing = AnimationCurve.Linear (0, 1, 1, 0);
    [SerializeField] RangeF m_shakeStep = 0.1f;

    float m_offset;


    protected virtual void OnEnable () {
        if (startOnEnable) StartShake ();
    }
    protected virtual void OnDisable () {
        AddOffset (-m_offset);
    }


    private void AddOffset(float offset)
    {
        m_offset            += offset;
        transform.localRotation = Quaternion.AngleAxis (m_offset, Vector3.forward );
    }
    public void StartShake () {
        StartShake (target, m_shakeLength.Random ());
    }

    public void StartShake (Transform target, float length) {
        StartCoroutine (ShakeCoroutine (target, length));
    }

    IEnumerator ShakeCoroutine (Transform target, float length) {

        if (target == null) yield break;

        float p1 = 0f;
        float p2 = 0f;

        float offset = 0f;

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
                p2 = m_shakeStrength.Random ();
            }

            var strength = m_shakeEasing.Evaluate(length <= 0 ? 0f : timer / length);

            var pos = Mathf.Lerp(p1, p2, stepLength > 0 ? stepTimer / stepLength : 1f) * strength;
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
