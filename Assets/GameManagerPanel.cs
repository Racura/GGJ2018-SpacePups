using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManagerPanel : OPanelView {

    [Zenject.Inject] IGameController m_gameController; 


 
    public void StartGame () { m_gameController.StartGame (); }
    public void EndGame () { m_gameController.EndGame (); }
}
