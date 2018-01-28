using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doggo : CommandableObject, ITintObject {

    [Zenject.Inject] IBoundsManager m_boundsManager;

    [SerializeField] RangeI length = 4;

    Vector2Int[] m_path;

    DoggoState m_state;
    public DoggoState State { get { return m_state; } }

    public override bool IsAcceptingCommands {
        get {
            return base.IsAcceptingCommands
                && State == DoggoState.Fetching;
        }
    }

    protected override void OnPrepareForReuses () {
        base.OnPrepareForReuses ();
        SetCoords (m_path = null);
        SetTint (-1);

        m_state = DoggoState.Spawning;
    }

    int m_guid;
    public int Tint { get { return m_guid; } }
    public void SetTint (int guid) { m_guid = guid; }



    public void CollectedBall(Ball ball)
    {
        if (ball.IsFinal) m_state = DoggoState.Retiring;
    }

    protected override void OnRestore () {
        base.OnRestore ();

        var l = length.Random ();

        if (l > 1) {
            var path = new Vector2Int[l];
            for (int i = 0; i < l; ++i) path[i] = PrimaryCoord - Direction * i;

            SetCoords (m_path = path);
        }
    }

    protected override void SetPrimaryCoord(Vector2Int position, bool setTransform) {
        base.SetPrimaryCoord (position, setTransform);
    
        if (m_path != null && m_path.Length > 0) {
            var p1 = m_path[0];

            if (m_path[0] != PrimaryCoord) {            
                for (int i = m_path.Length - 2; i >= 0; --i)
                    m_path[i + 1] = m_path[i];
                
                m_path[0] = PrimaryCoord;
                SetCoords (m_path);
            }
        }

        switch (State) {
            case DoggoState.Spawning:
                if (m_boundsManager.IsInside (PrimaryCoord)) {
                    m_state = DoggoState.Fetching;
                }
                break;
        }
    }

    public override bool TrySetCommand (CommandAction command) {
        var dir = GetDirectionFromCommand (command, Direction);
        if (dir * -1 == Direction) return false;
        return base.TrySetCommand (command);
    }

    public override TurnTask DoAction () {

        if (State == DoggoState.Retiring && m_boundsManager.IsInside (PrimaryCoord)) {

            var d = int.MaxValue;
            var command = CommandAction.Forward;
            
            var right   = m_boundsManager.Max.x - PrimaryCoord.x;
            var left    = PrimaryCoord.x - m_boundsManager.Min.x;

            var up      = m_boundsManager.Max.y - PrimaryCoord.y;
            var down    = PrimaryCoord.y - m_boundsManager.Min.y;

            if (d > left) {
                d = left;
                command = CommandAction.Left;
            }
            if (d > right) {
                d = right;
                command = CommandAction.Right;
            }
            if (d > down) {
                d = down;
                command = CommandAction.Down;
            }
            if (d > up) {
                d = up;
                command = CommandAction.Up;
            }

            ForceCommand (command);
        }



        return base.DoAction ();
    }
}

public enum DoggoState {
    Spawning,
    Fetching,
    Retiring
}

