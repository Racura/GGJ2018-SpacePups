using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandActionBar : MonoBehaviour {

    [Zenject.Inject] ICommandManager m_commander;

    [SerializeField] ObjectType m_targets;


    public void DoCommand (CommandAction command) {
        m_commander.SetCommand (command, m_targets);
    }

    public void DoClockwiseCommand () { DoCommand (CommandAction.Clockwise); }
    public void DoCounterClockwiseCommand () { DoCommand (CommandAction.CounterClockwise); }
    public void DoForwardCommand () { DoCommand (CommandAction.Forward); }


}
