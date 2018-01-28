using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ResetDotPool : MonoBehaviour
{
    List<ResetDot> m_resetList;
    List<ResetDot> m_prefabsList;



    protected virtual void LateUpdate () {
    
        if (m_resetList == null) return;

        for (int i = 0; i < m_resetList.Count; ++i) {
            if (m_resetList[i].ShouldDeallocate) m_resetList[i].ForceDeallocate ();
        }
    
    }


    public T Get<T>(T prefab, bool prepare = true) 
        where T : ResetDot 
    {
        var inst = GetDeallocated (prefab);

        if (prepare) inst.Prepare ();

        return inst;
    }

    private T GetDeallocated<T>(T prefab) 
        where T : ResetDot 
    {
        if (m_prefabsList != null) {
            for (int i = 0; i < m_prefabsList.Count; ++i) {
                if (m_prefabsList[i] != prefab) continue;

                if (m_resetList[i].ShouldDeallocate) m_resetList[i].Retire ();
                if (m_resetList[i].IsDeallocated) return m_resetList[i] as T;
            }
        }

        return Add (prefab);
    }


    private T Add<T>(T prefab) 
        where T : ResetDot 
    {
        if (m_resetList == null)    m_resetList = new List<ResetDot>();
        if (m_prefabsList == null)  m_prefabsList = new List<ResetDot>();

        var inst = GameObject.Instantiate (prefab, this.transform, false);
        inst.gameObject.hideFlags = HideFlags.DontSave;

        inst.ForceDeallocate ();

        m_resetList.Add (inst);
        m_prefabsList.Add (prefab);

        return inst;
    }


    static ResetDotPool m_staticPool;
    public static ResetDotPool StaticPool {
        get {
            if (m_staticPool == null) m_staticPool = CreateStaticPool ();
            return m_staticPool;
        }
    }

    private static ResetDotPool CreateStaticPool ()
    {
        var go = new GameObject ("Static Dot Pool", typeof (ResetDotPool));
        go.hideFlags = HideFlags.DontSave;

        return go.GetComponent<ResetDotPool>();
    }
}