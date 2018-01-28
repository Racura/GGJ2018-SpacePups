using System;
using UnityEngine;

public class SimpleTouchInput {

    static Touch[] m_touches;
    public static Touch[] Touches { 
        get { 
            if (m_touches == null) m_touches = new Touch[0];
            return m_touches;
        } 
    }


    public static void SetTouches(Touch[] touches) {
        m_touches = touches;
    }

    public struct Touch {
        public int id;
        public State state;

        public Vector2 downPosition;
        public Vector2 currentPosition;
        public Vector2 deltaPosition;


        public bool IsPressed { get { return state == State.Pressed; } }
        public bool IsReleased { get { return state == State.Released; } }

        public bool IsDown {
            get { return state == State.Down || state == State.Pressed; }
        }

        public void UpdateState(State state) { this.state = state; }
    }

    public enum State {
        Cancelled,
        Pressed, 
        Down, 
        Released,
    }

    public static Touch CreateTouch(int id, Vector2 position) {
        
        return new Touch () {
            id              = id,
            downPosition    = position,
            currentPosition = position,
            state           = State.Pressed,
        };
    }
}