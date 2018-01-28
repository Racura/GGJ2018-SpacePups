
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : DirectionGridObject
{
    [Zenject.Inject] IBoundsManager m_boundManager;

    protected override void OnPrepareForReuses () {
        base.OnPrepareForReuses ();
    }

    protected override void OnRestore () {
        base.OnRestore ();
    }


    public override TurnTask DoAction()
    {
        if (m_boundManager != null) {
            
            bool retire = false;

            
            if (Direction.x != 0) {
                if (Direction.x > 0 && m_boundManager.Max.x < PrimaryCoord.x)
                    retire = true; 
                if (Direction.x < 0 && m_boundManager.Min.x > PrimaryCoord.x)
                    retire = true; 
            }
            if (Direction.y != 0) {
                if (Direction.y > 0 && m_boundManager.Max.y < PrimaryCoord.y)
                    retire = true; 
                if (Direction.y < 0 && m_boundManager.Min.y > PrimaryCoord.y)
                    retire = true; 
            }


            if (retire) {
                Retire ();
                return TurnTask.Done;
            }
        }


        SetPrimaryCoord (PrimaryCoord + Direction, true);

        return TurnTask.Done;
    }
}
