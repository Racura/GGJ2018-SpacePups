using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ResettablePool : MonoBehaviour {   

    [Zenject.Inject] Zenject.DiContainer container;

    [SerializeField] bool canAccess;
	
	List<IResettableObject> m_prefabs;
	List<IResettableObject> m_list;
	int createdCounter;

    IResettableObject[] m_calAccess;


    public IResettableObject[] AccessList {
        get {
            ConstructList();
        
            if (m_calAccess != null)
            {
                for (int i = m_calAccess.Length - 1; i >= 0; --i) {
                    if (!m_calAccess[i].IsActive) {
                        m_calAccess = null;
                        break;
                    }
                }
            }

            if (m_calAccess == null)
            {
                var len = canAccess ? m_list.Count : 0;
                var c = 0;

                for (int i = len - 1; i >= 0; --i) {
                    if (m_list[i].IsActive) ++c;
                }

                m_calAccess = new IResettableObject[c];
                var index = 0;

                for (int i = len - 1; i >= 0; --i) {
                    if (m_list[i].IsActive) m_calAccess[index++] = m_list[i];
                }
            }

            return m_calAccess;
        }
    }

	public void CreatePool<T> (T prefab, int poolCount, bool countExisting = true)
		where T : UnityEngine.Component, IResettableObject 
	{
		if (prefab == null || poolCount <= 0) return;

        ConstructList ();

        for (int i = 0; countExisting && i < m_list.Count; ++i) {
            if (m_prefabs[i] == prefab) --poolCount;
        }

		for (int i = 0; i < poolCount; ++i) Create<T>(prefab);
	}

    public bool IsInstance<T> (T instance)
		where T : UnityEngine.Component, IResettableObject 
    {
        ConstructList ();

        for (int i = 0; i < m_list.Count; ++i)
        {
            if (m_list[i] == instance) return true;
        }

        return false;
    }


    public bool GetInstanceOf<T> (T instance, T prefab)
		where T : UnityEngine.Component, IResettableObject 
    {
        ConstructList ();

        for (int i = 0; i < m_list.Count; ++i)
        {
            if (m_list[i] != instance) continue;
            return (m_prefabs[i] == prefab);
        }

        return false;
    }
	
	public T Get<T> (T prefab, bool prepareForReuses = true) 
		where T : UnityEngine.Component, IResettableObject
    {
        ConstructList ();

        T obj = default(T);
        for (int i = 0; i < m_list.Count; ++i)
        {

            if (!m_list[i].IsReusable) continue;

            if (m_prefabs[i] == prefab)
            {
                obj = m_list[i] as T;
                break;
            }
        }

        if (obj == null) obj = Create<T>(prefab);
        if (obj != null && prepareForReuses) obj.PrepareForReuses();

        m_calAccess = null;

        return obj;
    }

    private void ConstructList ()
    {
        if (m_prefabs == null) m_prefabs = new List<IResettableObject>();
        if (m_list == null) m_list = new List<IResettableObject>();
    }

    protected T Create<T> (T prefab) 
		where T : UnityEngine.Component, IResettableObject 
	{
        ConstructList ();

        bool wasActive = prefab.gameObject.activeSelf;
        if (wasActive) prefab.gameObject.SetActive (false);
		
        var prefrabComponent = prefab as Component;
        GameObject inst;

        if (container != null) {
            inst    = container.InstantiatePrefab (prefab.gameObject, this.transform);
        } else {
            inst    = GameObject.Instantiate (prefrabComponent.gameObject, this.transform, false);
        }

        inst.name       = string.Format("{0} ({1})", prefrabComponent.name, createdCounter);
        inst.hideFlags  = HideFlags.DontSave;
        inst.SetActive (false);

        ++createdCounter;

        var obj = inst.GetComponent (typeof(T)) as T;
        obj.SetPool (this);
        
        m_prefabs.Add (prefab);
        m_list.Add (obj);

        if (wasActive) prefab.gameObject.SetActive (wasActive);
		
		return obj;
	}
}
