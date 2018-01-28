using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class OFadeOffsetGroup : OFadeGroup 
    {
        [Header ("Offset")]
        [SerializeField] AnimationCurve offsetCurve   = AnimationCurve.Linear (0, 0, 1, 1);
        [SerializeField] RectTransform target;
        [SerializeField] Vector2 offset;


        protected override void UpdateFade (float delta)
        {
            base.UpdateFade (delta);

            var length = offsetCurve.GetLength ();

            var temp = 1 - offsetCurve.Evaluate (TransitionValue / length);

            if (target != null)
            {
                target.anchoredPosition = offset * temp;
            }
        }


    }
