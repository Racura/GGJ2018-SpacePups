using UnityEngine;
using System.Collections;
using System;

public struct ResettableObjectRef<T> where T : IResettableObject
{
    ulong m_resetId;
    T m_value;

    public bool HasValue {
        get { Check (); return m_value != null; }
    }
    public T Value {
        get { Check (); return m_value; }
    }

    public ResettableObjectRef (T value) {

        m_resetId = value != null ? value.ResetId : 0;
        m_value = value;
    }


    private void Check()
    {
        if (m_value != null) {
            if (m_value.IsReusable || m_value.ResetId != m_resetId) 
                m_value = default (T);
        }
    }
}