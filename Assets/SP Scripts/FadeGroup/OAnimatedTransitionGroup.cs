using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public abstract class OAnimatedTransitionGroup : OTransitionGroup 
    {
        public const float Epsilon = 0.01f;

        [SerializeField] float transitionValue = 1f;

        [SerializeField] float fadeInLength   = 0.5f;
        [SerializeField] float fadeOutLength  = 0.5f;

        bool m_delay;

        public float TransitionValue { get { return transitionValue; } }
        public override bool IsTransitioning { 
            get { return IsOn ? (TransitionValue < 1 - Epsilon) : (TransitionValue > Epsilon); } 
        }

        public override void SetOn (bool on) {
            if (IsOn != on) m_delay = true;
            base.SetOn (on);
        }
        public override void Snap () { 
            base.Snap ();
            UpdateFade (float.MaxValue);
        }
        public virtual void LateUpdate () { 

            if (!m_delay) UpdateFade (Mathf.Min(Time.unscaledDeltaTime, Time.maximumDeltaTime));
            m_delay = false;
        }
        
        protected virtual void UpdateFade (float delta)
        {
            var wantedValue     = IsOn ? 1f : 0f;
            var length          = IsOn ? fadeInLength : fadeOutLength;

            if (delta > length) 
            {
                transitionValue = wantedValue;
            }
            else
            {
                transitionValue = Mathf.MoveTowards (
                    transitionValue, wantedValue,
                    delta / length
                );
            }
        }

    }
