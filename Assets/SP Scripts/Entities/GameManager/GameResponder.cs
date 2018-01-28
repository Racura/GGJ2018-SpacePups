


using UnityEngine;

public class GameResponder : MonoBehaviour, IGameResponder
{
    [Zenject.Inject] IGameManager m_gameManager;

    bool m_gameRunning;
    public bool IsGameRunning { get { return m_gameRunning; } }

    protected virtual void OnEnable () {
        if (m_gameManager != null) m_gameManager.AddObject (this);
        m_gameRunning = m_gameManager.IsGameRunning;
    }
    protected virtual void OnDisable () {
        if (m_gameManager != null) m_gameManager.RemoveObject (this);
    }

    public virtual void OnStartGame() { m_gameRunning = true; }
    public virtual void OnResetGame() { }
    public virtual void OnEndGame() { m_gameRunning = false; }
}