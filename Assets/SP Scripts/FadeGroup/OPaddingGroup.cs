using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class OPaddingGroup : OAnimatedTransitionGroup 
    {
        [Header ("Padding")]
        [SerializeField] AnimationCurve curve   = AnimationCurve.Linear (0, 0, 1, 1);
        [SerializeField] RectTransform rectTransform;
        [SerializeField] RectOffset padding;

        [HideInInspector] Vector2 m_offsetmin, m_offsetmax;

        protected override void UpdateFade (float delta)
        {
            base.UpdateFade (delta);

            if (rectTransform == null) {
                rectTransform = GetComponent <RectTransform>();
            }

            var length = curve.GetLength ();
            var lerp = curve.Evaluate (TransitionValue / length);

            var min = new Vector2(-padding.left, -padding.bottom) * lerp;
            var max = new Vector2(padding.right, padding.top) * lerp;

            if (rectTransform != null) {
                rectTransform.offsetMin += (min - m_offsetmin);
                rectTransform.offsetMax += (max - m_offsetmax);
            }

            m_offsetmin = min;
            m_offsetmax = max;
        }


    }
