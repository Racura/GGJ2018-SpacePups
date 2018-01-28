using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IGameManager, IGameController {

    bool m_reset;

    List <IGameResponder> m_list;

    bool m_gameRunning;    
    public bool IsGameRunning { get { return m_gameRunning; } }

    protected virtual void Awake () {
        Initialize () ;
    }
    
    protected virtual void Initialize () {
        if (m_list == null) m_list = new List<IGameResponder> ();
    }
    
    public void AddObject (IGameResponder gridObject) {
        Initialize () ;
        m_list.Add (gridObject);
    }
    public void RemoveObject (IGameResponder gridObject){
        Initialize () ;
        m_list.Remove (gridObject);
    }
    public bool ContainsObject (IGameResponder gridObject){
        return m_list != null  && m_list.Contains (gridObject);
    }

    public void ResetGame () {
        Initialize ();

        for (int i = 0; i < m_list.Count; ++i) {
            m_list[i].OnResetGame ();
        }
        m_reset = true;
    }

    public void StartGame () {
        Initialize ();
        if (!m_reset) ResetGame ();

        m_gameRunning = true;
        for (int i = 0; i < m_list.Count; ++i) {
            m_list[i].OnStartGame ();
        }

        m_reset = false;
    }

    public void EndGame () {
        Initialize ();

        m_gameRunning = false;
        for (int i = 0; i < m_list.Count; ++i) {
            m_list[i].OnEndGame ();
        }

        m_reset = false;
    }
}
