
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandableObject : DirectionGridObject, ICommandable
{
    [Zenject.Inject] ICommandManager m_commander;

    [SerializeField] CommandAction m_defaultAction = CommandAction.Forward;

    bool m_commandSet;
    CommandAction m_command;

    public bool HasCommand { get { return m_commandSet; } }
    public CommandAction CommandAction { get { return m_commandSet ? m_command : m_defaultAction; } }

    public virtual bool IsAcceptingCommands { get { return true; } }

    protected override void OnEnable () {
        base.OnEnable ();

        if (m_commander != null) m_commander.AddObject (this);
    }
    protected override void OnDisable () {
        base.OnDisable ();

        if (m_commander != null) m_commander.RemoveObject (this);
    }


    protected override void OnPrepareForReuses () {
        base.OnPrepareForReuses ();
    }

    protected override void OnRestore () {
        base.OnRestore ();
    }

    public static Vector2Int GetDirectionFromCommand (CommandAction command, Vector2Int currentDirection) {
    
    
        switch (command) {
            case CommandAction.Clockwise: 
            return new Vector2Int(currentDirection.y, -currentDirection.x); 
                
            case CommandAction.CounterClockwise: 
                return new Vector2Int(-currentDirection.y, currentDirection.x); 
            case CommandAction.Forward: 
                return currentDirection;
                
            case CommandAction.Up: 
                return  (new Vector2Int(0, 1)); 
                
            case CommandAction.Right: 
                return (new Vector2Int(1, 0)); 
                
            case CommandAction.Down: 
                return (new Vector2Int(0, -1)); 
                
            case CommandAction.Left: 
                return (new Vector2Int(-1, 0)); 

        }

        return new Vector2Int ();    
    }

    public override TurnTask DoAction()
    {
        var action = CommandAction;

        switch (CommandAction) {
            case CommandAction.Stay: 
                UnsetCommand ();
                return TurnTask.Done;
        }

        SetDirection (GetDirectionFromCommand (CommandAction, Direction)); 

        SetPrimaryCoord (PrimaryCoord + Direction, true);
        UnsetCommand ();

        return TurnTask.Done;
    }

    public bool Is(object obj)
    {
        if (obj is ObjectType) {
            var e = (ObjectType)obj;

            return e == ObjectType || e == ObjectType.All;
        }

        if (obj is CommandableObject) {
            return this == (obj as CommandableObject);
        }


        return false;
    }

    public void UnsetCommand()
    {
        m_commandSet = false;
    }

    public virtual bool TrySetCommand(CommandAction command)
    {
        if (!IsAcceptingCommands) return false;
        ForceCommand (command);
        return true;
    }

    protected virtual void ForceCommand(CommandAction command)
    {
        m_commandSet    = command != CommandAction.None;
        m_command       = command;
    }
}
