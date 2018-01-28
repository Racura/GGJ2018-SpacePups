using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITurnObject {

    int Priority { get; }
    bool IsActive { get; }
    TurnTask DoAction ();
}
