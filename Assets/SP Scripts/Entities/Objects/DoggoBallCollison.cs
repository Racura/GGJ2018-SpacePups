
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoggoBallCollison : TurnObject
{
    [Zenject.Inject] IGridManager m_gridManager;
    [Zenject.Inject] IScoreManager m_scoreMaanger;

    [SerializeField] ObjectType m_doggoType;
    [SerializeField] ObjectType m_ballType;

    public bool IsActive { get { return isActiveAndEnabled; } }


    private void CollectBall(Doggo doggo, Ball ball)
    {
        m_scoreMaanger.AddScore (ball.Score);

        doggo.CollectedBall (ball);
        ball.Retire ();

    }

    protected override TurnTask DoAction()
    {
        if (!IsGameRunning) return TurnTask.Done;
        
        var state = m_gridManager.GetGridState ();

        for (int i = 0; i < state.Length; ++i) {

            var obj = state[i];
            if (obj.ObjectType != m_ballType) continue;

            var ball = obj as Ball;

            var coord = obj.PrimaryCoord;

            int index = 0;

            IGridObject other = null;//m_gridManager.GetGridObject (coord, ref index);

            do {            
                other = m_gridManager.GetGridObject (coord, ref index);

                if (other != null) {
                    if (other.ObjectType != m_doggoType) continue;

                    var doggo = other as Doggo;

                    if (doggo.Tint == ball.Tint) {
                        CollectBall (doggo, ball);
                    } else {
                    }
                }

            } while (other != null);
        }

        return TurnTask.Done;
    }
}
