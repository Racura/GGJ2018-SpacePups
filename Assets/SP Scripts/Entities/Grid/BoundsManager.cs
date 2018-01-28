using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsManager : MonoBehaviour, IBoundsManager {

    [SerializeField] RectInt m_rect;

    public Vector2Int Min { get { return new Vector2Int (m_rect.x, m_rect.y); } }
    public Vector2Int Max { get { return new Vector2Int (m_rect.x + m_rect.width, m_rect.y + m_rect.height); } }

    public bool IsInside (Vector2Int position) {
        

        return m_rect.x <= position.x
            && m_rect.y <= position.y
            && m_rect.x + m_rect.width >= position.x
            && m_rect.y + m_rect.height >= position.y;
    }








    protected virtual void OnDrawGizmos () {

        var grid = FindObjectOfType<Grid>();

        if (grid != null)  {

            Gizmos.color = Color.red;

            var min = Min;
            var max = Max;

            var minPos = grid.GetCellCenterWorld (new Vector3Int(min.x, min.y, 0)) - grid.cellSize * 0.5f;
            var maxPos = grid.GetCellCenterWorld (new Vector3Int(max.x, max.y, 0)) + grid.cellSize * 0.5f;

            Gizmos.DrawWireCube ((minPos + maxPos) * 0.5f, maxPos - minPos);
            
            Gizmos.color = Color.red.MultiplyAlpha (0.5f);

            for (int x = min.x; x <= max.x; ++x) {
                for (int y = min.y; y <= max.y; ++y) {
                    Gizmos.DrawWireCube (
                        grid.GetCellCenterWorld (new Vector3Int(x, y, 0)), grid.cellSize
                    );
                }
            }
        }
    }
}
