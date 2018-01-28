using UnityEngine;
using System.Collections;
using System;

public abstract class DotListener : MonoBehaviour, IDotListener
{

    [SerializeField] ResetDot dot;
    [SerializeField] float delayDeallocLength = 0.2f;

    protected abstract bool IsListenerActive { get; }
    protected ResetDot Dot { get { return dot; } }


    protected virtual void Update () {
        if (dot == null) return;

        if (dot.IsAllocated && !dot.IsActive) {
            if (IsListenerActive) {
                dot.ExtendDeallocate (delayDeallocLength);
            }
        }

    }

    public virtual void DotStateChanged (ResetDot.States state) { }

}   
