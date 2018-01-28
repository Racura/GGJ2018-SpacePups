using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITurnManager {

    int TurnObjectCount { get; }
    
    void AddObject (ITurnObject turnObject);
    void RemoveObject (ITurnObject turnObject);
    bool ContainsObject (ITurnObject turnObject);
}
