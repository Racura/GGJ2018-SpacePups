using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class OSetPanel : OPanel 
    {
        [SerializeField] OPanel panel;
        [SerializeField] float delay        = 1.0f;
        [SerializeField] bool setAsRoot     = true;
        [SerializeField] bool unscaledTime  = true;

        float m_timer;

        protected override void OnFocus() 
        {
            m_timer = 0;
        }

        protected virtual void Update () 
        {
            if (IsFocused) {
                m_timer += unscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
                if (m_timer > delay) TrySetPanel ();
            }
        }

        public void SetPanel ()
        {
            TrySetPanel ();
        }
        public bool TrySetPanel ()
        {
            if (!IsFocused) return false;
            
            var pm = Navigation;

            if (setAsRoot) {
                pm.SetRoot (panel, true);
            } else {
                pm.PushPanel (panel, true);
            }

            Hide ();
            return true;
        }


        public override bool CanHandleBack () { return true; }
        public override bool TryHandleBack () {
            TrySetPanel ();
            return true; 
        }
    }
