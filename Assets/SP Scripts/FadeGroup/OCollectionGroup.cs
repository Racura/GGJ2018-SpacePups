using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class OCollectionGroup : OTransitionGroup 
    {
        public const float Epsilon = 0.01f;

        [SerializeField] OTransitionGroup[] transitions;

        public override bool IsOnOrTransitioning { 
            get {
                for (int i = transitions.Length - 1; i >= 0; --i) {
                    if (transitions[i].IsOnOrTransitioning) return true;
                }
                
                return base.IsOnOrTransitioning;
            } 
        }

        public override void SetOn (bool on) {
            base.SetOn (on);
            
            for (int i = transitions.Length - 1; i >= 0; --i) {
                transitions[i].SetOn (IsOn);
            }
        }

        public override void Snap () {
            base.Snap ();
            
            for (int i = transitions.Length - 1; i >= 0; --i) {
                transitions[i].Snap ();
            }
        }
    }
