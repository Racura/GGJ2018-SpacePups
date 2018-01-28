
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : TurnObject, ICommandManager
{

    TurnTask m_turnTask;
    List<ICommandable> m_list;

    public int CommandObjectCount { get { return m_list != null ? m_list.Count : 0; } }
    


    protected virtual void Awake () {
        Initialize () ;
    }
    
    protected virtual void Initialize () {
        if (m_list == null) m_list = new List<ICommandable> ();
    }


    
    public void AddObject (ICommandable commandable) {
        Initialize () ;
        m_list.Add (commandable);
    }
    public void RemoveObject (ICommandable commandable){
        Initialize () ;
        m_list.Remove (commandable);
    }
    public bool ContainsObject (ICommandable commandable){
        return m_list != null  && m_list.Contains (commandable);
    }

    public void SetCommand(CommandAction command)
    {
        for (int i = 0; i < m_list.Count; ++i) {
            if (m_list[i] == null || !m_list[i].IsActive) 
                continue;

            m_list[i].TrySetCommand (command);
        }
    }

    public void SetCommand(CommandAction command, object obj)
    {
        for (int i = 0; i < m_list.Count; ++i) {
            if (m_list[i] == null || !m_list[i].IsActive) 
                continue;
            if (!m_list[i].Is (obj)) 
                continue;

            m_list[i].TrySetCommand (command);
        }
    }

    public bool HasCommand()
    {
        if (m_list == null) return false;

        for (int i = 0; i < m_list.Count; ++i) {
            if (m_list[i] == null || !m_list[i].IsActive) 
                continue;

            if (m_list[i].HasCommand) return true;
        }

        return false;
    }

    protected virtual void Update () {

        if (m_turnTask != null) {
            if (HasCommand ()) {
                m_turnTask.Resolve ();
                m_turnTask = null;
            }
        }
    }

    public override void OnResetGame () {
        base.OnResetGame ();




        if (m_turnTask != null) {
            m_turnTask.TryResolve ();
            m_turnTask = null;
        }
    }


    protected override TurnTask DoAction()
    {
        if (!IsGameRunning) return TurnTask.Done;

        int counter = 0;

        for (int i = 0; i < m_list.Count; ++i) {
            if (m_list[i] == null || !m_list[i].IsActive) 
                continue;
            if (!m_list[i].IsAcceptingCommands) 
                continue;

            m_list[i].UnsetCommand ();
            counter ++;
        }

        if (counter <= 0) return TurnTask.Done;
        return m_turnTask = new TurnTask ();
    }
}
