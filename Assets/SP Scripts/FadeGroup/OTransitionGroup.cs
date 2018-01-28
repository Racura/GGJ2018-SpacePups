using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public abstract class OTransitionGroup : MonoBehaviour 
    {

        [SerializeField] bool isOn = false;

        public bool IsOn { get { return isOn; } set { SetOn (value); } }
        public virtual bool IsTransitioning { get { return false; } }
        public virtual bool IsOnOrTransitioning { get { return IsOn || IsTransitioning; } }

        public virtual void Snap () { }


        [System.NonSerialized] bool m_isOn;


        public virtual void SetOn (bool on) {
            isOn = m_isOn = on;
        }

        protected virtual void Update () {
            if (isOn != m_isOn) SetOn (isOn);
        }
    }
