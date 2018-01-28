using UnityEngine;
using System.Collections;
using System;

public class ResettableObject : MonoBehaviour, IResettableObject
{
	public bool IsBeingPrepared { get { return m_prepared; } }
	public bool IsActive { get { return m_isActive; } }
	public bool IsReusable { get { return !(m_isActive || m_prepared); } }
	public ulong ResetId { get { return m_resetId; } }
	public ResettablePool Pool { get { return m_pool; } }
    
	[System.NonSerialized] ulong m_resetId;
	[System.NonSerialized] bool m_prepared;
	[System.NonSerialized] bool m_isActive;
    [System.NonSerialized] ResettablePool m_pool;

	protected virtual void Awake () { }
	protected virtual void OnEnable () { }
	protected virtual void OnDisable () { }

	public void PrepareForReuses () {
		if(m_prepared) Debug.LogError("ResettableObject is already prepared.");
		if(IsActive) Debug.LogError("ResettableObject is already active.");

		unchecked { ++m_resetId; }

		m_prepared = true;
        m_isActive = false;

        OnPrepareForReuses ();
	}

    public void Restore () {
		if(!Application.isPlaying) return;
		if(!m_prepared) Debug.LogError("ResettableObject has not been prepared.");

        gameObject.SetActive(m_isActive = true);
        
        OnRestore ();
    }

    public void Retire () {
		gameObject.SetActive(m_isActive = false);
		m_prepared = false;

        OnRetire ();
    }

	protected virtual void OnPrepareForReuses () { }
	protected virtual void OnRestore () { }
	protected virtual void OnRetire () { }
    

	void IResettableObject.SetPool (ResettablePool pool) {
        m_pool = pool;
    }

}

public interface IResettableObject {
    bool IsActive { get; }
    bool IsReusable { get; }
	ulong ResetId { get; }
	ResettablePool Pool { get; }


	void PrepareForReuses ();
    void Restore ();
    void Retire ();

	void SetPool (ResettablePool pool);
}