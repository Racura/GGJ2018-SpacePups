using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoggoOutOfBounds : TurnObject
{
    [Zenject.Inject] IBoundsManager m_boundsManager;
    [Zenject.Inject] IGridManager m_gridManager;

    [SerializeField] UnityEngine.Events.UnityEvent m_onOutsideGrid;


    [SerializeField] ObjectType m_doggoType;

    protected override TurnTask DoAction()
    {
        if (!IsGameRunning) return TurnTask.Done;
        
        var state = m_gridManager.GetGridState ();

        for (int i = 0; i < state.Length; ++i) {

            var obj = state[i];
            if (obj.ObjectType != m_doggoType) continue;

            var doggo = obj as Doggo;

            if (doggo == null || doggo.State != DoggoState.Fetching) continue;

            var coord = obj.PrimaryCoord;

            if (!m_boundsManager.IsInside (coord)) {
                var h = m_onOutsideGrid;
                if (h != null) h.Invoke ();
            }
        }

        return TurnTask.Done;
    }
}
