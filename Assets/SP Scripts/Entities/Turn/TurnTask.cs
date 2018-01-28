


public class TurnTask {

    bool m_isDone;
    public bool IsDone { get { return m_isDone; } }

    public TurnTask () {
        m_isDone = false;
    }
    public TurnTask (bool isDone) {
        m_isDone = isDone;
    }


    public bool TryResolve () {
        if (IsDone) return false;
        Resolve ();
        return true;
    }

    public void Resolve () {
        if (IsDone) throw new System.Exception ("Task has already been resolved!");

        m_isDone = true;
    }

    public static readonly TurnTask Done = new TurnTask (true);
}