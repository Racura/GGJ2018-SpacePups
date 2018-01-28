using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBoundsManager {
    Vector2Int Min { get; }
    Vector2Int Max { get; }

    bool IsInside(Vector2Int position);
}
