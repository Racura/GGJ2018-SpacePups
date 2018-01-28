using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameManager {

    bool IsGameRunning { get; }

    void AddObject (IGameResponder obj);
    void RemoveObject (IGameResponder obj);
    bool ContainsObject (IGameResponder obj);
}

public interface IGameController {
    void StartGame ();
    void EndGame ();
}