using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class OFadeGroup : OAnimatedTransitionGroup 
    {
        [Header ("Fading")]
        [SerializeField] AnimationCurve alphaFade   = AnimationCurve.Linear (0, 0, 1, 1);
        [SerializeField] CanvasGroup canvasGroup;
        [SerializeField] bool setInteractable;

        [System.NonSerialized] float m_alpha;

        protected override void UpdateFade (float delta)
        {
            base.UpdateFade (delta);

            var length = alphaFade.GetLength ();

            m_alpha = alphaFade.Evaluate (TransitionValue / length);

            if (canvasGroup != null)
            {
                canvasGroup.alpha = m_alpha;
                if (setInteractable)
                {
                    canvasGroup.interactable = canvasGroup.blocksRaycasts
                         = IsOnOrTransitioning;
                }
            }
        }


    }
