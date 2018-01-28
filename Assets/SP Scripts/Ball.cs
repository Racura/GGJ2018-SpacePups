using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : ResettableGridObject, ITintObject {

    [SerializeField] bool m_isFinal;
    [SerializeField] int m_score;

    protected override void OnPrepareForReuses () {
        base.OnPrepareForReuses ();
        SetTint (-1);

        SetPosition(Vector2Int.zero);
    }

    int m_guid;
    public int Tint { get { return m_guid; } }

    public bool IsFinal { get { return m_isFinal; } }
    public int Score { get { return m_score; } }
    

    public void SetTint (int guid) { m_guid = guid; }

    protected override void OnRestore () {
        base.OnRestore ();
    }

    public void SetPosition(Vector2Int position)
    {
        SetPrimaryCoord (position, true);
    }
}
