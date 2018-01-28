using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGridObject {

    bool IsActive { get; }
    
    Vector2Int PrimaryCoord { get; }
    Vector2Int[] Coords { get; }


    ObjectType ObjectType { get; }
}
