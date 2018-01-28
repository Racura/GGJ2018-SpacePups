using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class OPanelView : MonoBehaviour 
    {
        [SerializeField] bool animated = true;
        [SerializeField] OPanel panel;

        [System.NonSerialized] bool m_focused, m_show;

        protected OPanel Panel { 
            get {
                if (panel == null) panel = GetComponentInParent <OPanel>();
                return panel;
            } 
        }
        protected ONavigationPanel Navigation { 
            get { return Panel != null ? Panel.Navigation : null; }
        }
        public bool IsPanelFocused { get { return Panel != null && Panel.IsFocused; } }
        public bool IsPanelShowing { get { return Panel != null && Panel.IsShowing; } }

        protected virtual void OnEnable () { UpdateState(); }
        protected virtual void OnDisable () { UpdateState(); }
        protected virtual void Update () { UpdateState(); }

        public void SetRootPanel (OPanel panel) { SetRootPanel (panel, animated); }
        public void PushPanel (OPanel panel) { PushPanel (panel, animated); }

        public virtual void SetRootPanel (OPanel panel, bool animated)
        {
            if (panel == null || !Panel.IsFocused) return;

            var panelManager = DependencyResolver.Get<IPanelManager>();
            panelManager.SetRoot (panel, animated);
        }
        public virtual void PushPanel (OPanel panel, bool animated)
        {
            if (panel == null || !Panel.IsFocused) return;

            var panelManager = DependencyResolver.Get<IPanelManager>();
            panelManager.PushPanel (panel, animated);
        }


        private void UpdateState()
        {
            if (m_show != IsPanelShowing)
            {
                m_show = IsPanelShowing;

                if (m_show)
                {
                    OnPanelShow();
                }
                else
                {
                    OnPanelHide();
                }
            }
            if (m_focused != IsPanelFocused)
            {
                m_focused = IsPanelFocused;

                if (m_focused)
                {
                    OnPanelFocused();
                }
                else
                {
                    OnPanelUnfocused();
                }
            }
        }

        protected virtual void OnPanelShow () { }
        protected virtual void OnPanelHide () { }
        protected virtual void OnPanelFocused () { }
        protected virtual void OnPanelUnfocused () { }
    }
