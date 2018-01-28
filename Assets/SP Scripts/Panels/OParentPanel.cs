using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class OParentPanel : OPanel
    {
        List<OPanel> m_stack;
        OPanel[] m_cacheStack;

        [SerializeField] ResettablePool pool;


        public OPanel[] Stack { 
            get { 
                if (m_stack == null) return null;
                if (m_cacheStack == null) m_cacheStack = m_stack.ToArray ();
                return m_cacheStack; 
            } 
        }
        public OPanel ActivePanel { 
            get {
                if (m_stack == null) return null;

                var c = m_stack.Count;
                for (int i = c - 1; i >= 0; --i) {
                    if (m_stack[i].IsShowing) return m_stack[i];
                }
                return null; 
            } 
        }
        public OPanel RootPanel { 
            get {
                if (m_stack == null) return null;
                var c = m_stack.Count;
                for (int i = 0; i < c; ++i) {
                    if (m_stack[i].IsShowing) return m_stack[i];
                }
                return null; 
            } 
        }

        protected virtual void Update () {
            UpdateStack ();
        }

        protected virtual void UpdateStack () 
        {
            if (m_stack == null) return;

            var activePanel = ActivePanel;

            for (int i = 0; i < m_stack.Count; ++i)
            {
                bool focused = m_stack[i] == activePanel && IsFocused;

                if (m_stack[i].IsFocused != focused) {
                    m_stack[i].SetFocused (focused);
                }

                if (!m_stack[i].IsVisible) 
                {
                    if (m_stack[i].IsActive) m_stack[i].Retire ();

                    m_stack.RemoveAt (i);
                    m_cacheStack = null;
                    --i;

                    continue;
                }
            }
        }

        public override void SetFocused (bool focused)
        {
            base.SetFocused (focused);
            UpdateStack ();
        }

        public OPanel GetInstancePanel (OPanel prefab) 
        {
            if (pool.IsInstance (prefab)) return prefab;

            var instance = pool.Get (prefab, true);
            instance.SetParent (this);

            return instance;
        }

        public void CreatePanelPool (OPanel[] panels, int count) 
        {
            for (int i = 0; i < panels.Length; ++i) {
                pool.CreatePool (panels[i], count);
            }
        }

        protected void ClearPanels (bool animated) {

            if (m_stack == null) m_stack = new List<OPanel> ();
            
            for (int i = m_stack.Count - 1; i >= 0; --i)
            {
                if (m_stack[i].IsShowing) m_stack[i].Hide ();

                if (!animated) m_stack[i].Snap ();
            }
            
            UpdateStack ();
        }

        protected OPanel AddPanel (OPanel panel, bool animated) 
        {
            if (m_stack == null) m_stack = new List<OPanel> ();

            OPanel instance = GetInstancePanel (panel);

            instance.transform.SetAsLastSibling ();
            m_stack.Add (instance);

            m_cacheStack = null;
            
            instance.Restore ();
            instance.Show ();
            if (!animated) instance.Snap ();

            UpdateStack ();

            return instance;
        }

        protected void RemovePanel (OPanel panel, bool animated) {
            
            if (m_stack == null) m_stack = new List<OPanel> ();

            for (int i = m_stack.Count - 1; i >= 0; --i)
            {
                if (m_stack[i] == panel || pool.GetInstanceOf (m_stack[i], panel))
                {
                    m_stack[i].Hide ();
                    if (!animated) m_stack[i].Snap ();

                    UpdateStack ();
                    return;
                }
            }
        }

        public override void Snap () 
        {
            base.Snap ();

            if (m_stack != null) {
                for (int i = 0; i < m_stack.Count; ++i)
                    m_stack[i].Snap ();
            }
            UpdateStack ();
        }

        public override bool CanHandleBack () {

            var active = ActivePanel;

            if (active != null) {
                if (active.CanHandleBack () || active != RootPanel) return true;
            }
            return base.CanHandleBack ();
        }
        public override bool TryHandleBack () { 

            var active = ActivePanel;
            if (active != null) {
                if (active.CanHandleBack ()) {
                    if (active.TryHandleBack ()) return true;
                }
                if (active != RootPanel) {
                    RemovePanel (active, true);
                    return true;
                }
            }

            return base.TryHandleBack ();
        }
    }
