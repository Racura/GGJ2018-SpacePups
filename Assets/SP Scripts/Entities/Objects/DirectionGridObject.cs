
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DirectionGridObject : ResettableGridObject, ITurnObject
{
    [Zenject.Inject] ITurnManager m_turnManager;
    
    [SerializeField] int m_turnPriority;

    Vector2Int m_direction;

    public Vector2Int Direction { get { return m_direction; } }
    int ITurnObject.Priority { get { return m_turnPriority; } }



    protected override void OnEnable () {
        base.OnEnable ();
        if (m_turnManager != null) m_turnManager.AddObject (this);
    }
    protected override void OnDisable () {
        base.OnDisable ();
        if (m_turnManager != null) m_turnManager.RemoveObject (this);
    }


    protected override void OnPrepareForReuses () {
        base.OnPrepareForReuses ();
        
        SetPosition (Vector2Int.zero);
        SetDirection (Vector2Int.right);
    }

    public virtual void SetPosition (Vector2Int position) {
        SetPrimaryCoord (position, true);
    }
    public virtual void SetDirection (Vector2Int direction) {
        m_direction = direction;
    }

    protected override void OnRestore () {
        base.OnRestore ();
    }


    public abstract TurnTask DoAction();
}
