using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

    public class OSelectableView : OPanelView {

        [SerializeField] Selectable[] selectables;

        protected override void OnPanelFocused () {
            base.OnPanelUnfocused ();
            SetInteractable (true);
        }

        protected override void OnPanelUnfocused () {
            base.OnPanelUnfocused ();
            SetInteractable (false);
        }

        protected virtual void SetInteractable(bool interactable)
        {
            for (int i = 0; i < selectables.Length; ++i) {
                if (selectables[i] != null) {
                    selectables[i].interactable = interactable;
                }
            }
        }
    }