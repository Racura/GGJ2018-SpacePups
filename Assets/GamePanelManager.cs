using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GamePanelManager : GameResponder {

    [SerializeField] OPanelManager m_panelManager;
    [SerializeField] OPanel m_gamePanel;
    [SerializeField] OPanel m_gameoverPanel;

    public override void OnStartGame() {
        base.OnStartGame ();

        m_panelManager.SetRoot (m_gamePanel, true);
    }
    public override void OnEndGame() {
        base.OnEndGame ();
        m_panelManager.SetRoot (m_gameoverPanel, true);
    }
}
