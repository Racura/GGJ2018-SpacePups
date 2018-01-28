using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class OPanelManager : MonoBehaviour, IPanelManager
    {
        [SerializeField] ONavigationPanel navigationPanel;
        [SerializeField] OPanel[] rootPanels;
        [SerializeField] OPanel[] pooledPanels;

        public virtual void Awake () 
        {
            if (navigationPanel != null) {
                navigationPanel.PrepareForReuses ();
                navigationPanel.SetFocused (true);
                navigationPanel.Restore ();
            }

            navigationPanel.CreatePanelPool (pooledPanels, 1);

            for (int i = 0; i < rootPanels.Length; ++i) {
                if (i == 0)
                {
                    SetRoot (rootPanels[i], false);
                }
                else
                {
                    PushPanel (rootPanels[i], false);
                }
            }
        }

        protected virtual void Update () 
        {
            if (Input.GetKeyDown (KeyCode.Escape) || Input.GetButtonDown ("Cancel")) HandleBack ();
        }

        public virtual void SetRoot (OPanel panel, bool animated) 
        {
            if (navigationPanel != null) {
                navigationPanel.SetRoot (panel, animated);
            }
        }

        public virtual void PushPanel (OPanel panel, bool animated) 
        {
            if (navigationPanel != null) {
                navigationPanel.PushPanel (panel, animated);
            }
        }

        public virtual void PopPanel (bool animated) {
            
            if (navigationPanel != null) {
                navigationPanel.PopPanel (animated);
            }
        }

        public virtual void Snap () {
            if (navigationPanel != null) {
                navigationPanel.Snap ();
            }
        }


        public virtual void HandleBack () {

            if (navigationPanel != null 
                && navigationPanel.CanHandleBack () 
                && navigationPanel.TryHandleBack ()
            ) {
                return;
            }
            
            Application.Quit ();
        }
    }
