using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBoundsCollider {
    void OnOutsideBounds (Vector2Int point, IBoundsManager boundsManager);
}
