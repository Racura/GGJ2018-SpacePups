using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurnObject : GameResponder,  ITurnObject {

    [Zenject.Inject] ITurnManager m_turnManager;
    [SerializeField] int m_turnPriority         = 100;



    int ITurnObject.Priority { get { return m_turnPriority; } }
    bool ITurnObject.IsActive { get { return isActiveAndEnabled; } }

    protected override void OnEnable () {
        base.OnEnable ();
        if (m_turnManager != null) m_turnManager.AddObject (this);
    }
    protected override void OnDisable () {
        base.OnDisable ();
        if (m_turnManager != null) m_turnManager.RemoveObject (this);
    }
    
    TurnTask ITurnObject.DoAction () {
        return DoAction ();
    }
    protected abstract TurnTask DoAction ();
}
