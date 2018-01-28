
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoggoCollison : TurnObject
{
    [Zenject.Inject] IGridManager m_gridManager;
    [SerializeField] ObjectType m_doggoType;
    [SerializeField] UnityEngine.Events.UnityEvent m_onCollsion;

    private void CollectBall(Doggo doggo, Ball ball)
    {
        doggo.CollectedBall (ball);
        ball.Retire ();
    }

    protected override TurnTask DoAction()
    {
        if (!IsGameRunning) return TurnTask.Done;
        
        var state = m_gridManager.GetGridState ();

        for (int i = 0; i < state.Length; ++i) {

            var obj = state[i];
            if (obj.ObjectType != m_doggoType) continue;

            var d1 = obj as Doggo;

            var coord = obj.PrimaryCoord;

            int index = 0;

            IGridObject other = null;//m_gridManager.GetGridObject (coord, ref index);

            do {            
                other = m_gridManager.GetGridObject (coord, ref index);
                var skip = false;

                if (other == d1) {
                    skip = true;
                    var path = other.Coords;
                    for (int u = 1; u < path.Length; ++u) {
                        if (path[u] == coord) {
                            skip = false;
                        }
                    }
                }


                if (!skip && other != null) {
                    if (other.ObjectType != m_doggoType) continue;

                    var h = m_onCollsion;
                    if (h != null) h.Invoke ();

                }

            } while (other != null);


        }

        return TurnTask.Done;
    }
}
