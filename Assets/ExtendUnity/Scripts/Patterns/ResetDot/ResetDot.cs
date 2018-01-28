using UnityEngine;
using System.Collections;
using System;

public class ResetDot : MonoBehaviour
{
    ulong m_resetId;
    States? m_currentState;

    float m_alloc;

    bool? m_isLogic, m_isAppearance;

	public ulong ResetId { get { return m_resetId; } }
	protected States CurrentState { get { return m_currentState ?? States.Active; } }

	protected bool IsLogicActive { get { return m_isLogic ?? false; } }
	protected bool IsAppearanceActive { get { return m_isAppearance ?? false; } }
    
	protected virtual float DefaultDeallocateTime { get { return m_resetId; } }
	public bool ShouldDeallocate { 
        get { return m_currentState == States.Retiring && m_alloc < Time.unscaledTime; } 
    }

    public bool IsDeallocated   { get { return m_currentState == States.Dealloc; } }
    public bool IsAllocated     { get { return m_currentState != States.Dealloc; } }
    public bool IsActive        { get { return m_currentState == States.Active; } }
	
    public virtual void Prepare () 
    {
        if (IsDeallocated) ++m_resetId;
        TrySetState (States.Preparing);
    }
    public virtual void Restore () 
    {
        TrySetState (States.Active);
    }
    public virtual void Retire () 
    {
        m_alloc = Time.unscaledTime + DefaultDeallocateTime;
        TrySetState (States.Retiring);
    }
    public virtual void ForceDeallocate () 
    {
        TrySetState (States.Dealloc);
    }
    protected virtual bool TrySetState (States state) 
    {
        if (m_currentState.HasValue && CurrentState == state)
            return false;
    
        m_currentState = state;

        StateChanged (state);
        return true;
    }
    protected virtual void StateChanged (States state) 
    {
        if (m_listener == null) {
            m_listener = this.GetIComponentsInChildren<IDotListener> (true);
        }

        for (int i = 0; i < m_listener.Length; ++i) {
            m_listener[i].DotStateChanged (CurrentState);
        }
    }

    IDotListener[] m_listener;
    public void MarkWatchersDirty () { m_listener = null; }

    public void ExtendDeallocate (float seconds) {
        m_alloc = Mathf.Max (m_alloc, Time.unscaledTime + seconds);
    }

    public enum States {
        Dealloc,
        Preparing,
        Active,
        Retiring,
    }
}   
