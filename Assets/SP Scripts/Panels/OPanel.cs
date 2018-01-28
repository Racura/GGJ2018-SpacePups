using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

    public class OPanel : ResettableObject 
    {
        [SerializeField] UnityEvent backEvent;
        [SerializeField] OTransitionGroup transitionGroup;
        [SerializeField] UnityEngine.UI.Selectable defaultSelectable;

        bool m_isShowing, m_isFocused;
        OPanel m_parent;
        UnityEngine.GameObject m_previousSelectable;
        

        
        public OPanel Parent { get { return m_parent; } }
        public ONavigationPanel Navigation { 
            get { 
                var parent = Parent;

                while (parent != null) {
                    if (parent is ONavigationPanel) {
                        return parent as ONavigationPanel;
                    }

                    parent = parent.Parent;
                }

                return null; 
            }
        }
        
        public bool IsShowing {
            get { return transitionGroup != null ? transitionGroup.IsOn : m_isShowing; }
        }
        public bool IsVisible {
            get { return IsActive && transitionGroup != null ? transitionGroup.IsOnOrTransitioning : m_isShowing; }
        }

        public bool IsFocused { get { return m_isFocused; } }

        protected bool HasBackEvent {
            get { return backEvent != null && backEvent.GetPersistentEventCount() > 0; }
        }


        protected override void OnPrepareForReuses() {
            base.OnPrepareForReuses ();

            SetParent (null);
            SetFocused (false);
            SetShowing (false);
            
            Snap ();
        }

        public void SetParent (OPanel parent) { 
            m_parent  = parent;
        }

        public void Show () { SetShowing(true); }
        public void Hide () { SetShowing(false); }

        public virtual void Snap () { 
            if (transitionGroup != null) transitionGroup.Snap ();
        }

        protected void SetShowing (bool isShowing)
        {
            if (m_isShowing != isShowing) {
                m_isShowing = isShowing;

                if (m_isShowing) {
                    OnAppearing ();
                } else {
                    OnDisappearing ();
                }
            }
            if (transitionGroup != null) transitionGroup.IsOn = m_isShowing;
        }

        public virtual void SetFocused (bool focused)
        {
            if (m_isFocused != focused) {
                m_isFocused = focused;

                if (m_isFocused) {
                    OnFocus ();
                } else {
                    OnUnfocus ();
                }
            }
        }

        protected virtual void OnAppearing () {
            m_previousSelectable = null;
        }
        protected virtual void OnDisappearing () {
            m_previousSelectable = null;
        }
        protected virtual void OnFocus () {

            var selectable = m_previousSelectable;

            if (m_previousSelectable == null && defaultSelectable != null) {
                m_previousSelectable = defaultSelectable.gameObject;
            }

            var es = UnityEngine.EventSystems.EventSystem.current;
            if (es != null) es.SetSelectedGameObject (m_previousSelectable);
        }
        protected virtual void OnUnfocus () {


            if (IsShowing) {
                var es = UnityEngine.EventSystems.EventSystem.current;
                m_previousSelectable = es.currentSelectedGameObject;
                es.SetSelectedGameObject (null);
            }
        }

        public virtual bool CanHandleBack () { return HasBackEvent; }
        public virtual bool TryHandleBack () { 

            if (HasBackEvent) {
                var h = backEvent;
                h.Invoke ();

                return true;
            }


            return false;
        }
    }
