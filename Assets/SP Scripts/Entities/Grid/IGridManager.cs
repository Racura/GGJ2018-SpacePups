using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGridManager {

    int ObjectCount { get; }
    UnityEngine.Grid UnityGrid { get; }
    
    void AddObject (IGridObject gridObject);
    void RemoveObject (IGridObject gridObject);
    bool ContainsObject (IGridObject gridObject);

    IGridObject GetGridObject (Vector2Int position, ref int index);
    
    IGridObject[] GetGridState ();
}
