
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResettableGridObject : ResettableObject, IGridObject
{
    [Zenject.Inject] IGridManager m_gridManager;

    [SerializeField] ObjectType m_objectType;

    public ObjectType ObjectType { get { return m_objectType; } }
    public bool IsActive { get { return isActiveAndEnabled; } }

    public Vector2Int PrimaryCoord { get { return m_position; } }
    public virtual Vector2Int[] Coords { get { return m_coords ?? m_autoCoords; } }

    Vector2Int m_position;
    Vector2Int[] m_coords, m_autoCoords;

    protected override void OnEnable () {
        base.OnEnable ();
        if (m_gridManager != null) m_gridManager.AddObject (this);
    }
    protected override void OnDisable () {
        base.OnDisable ();
        if (m_gridManager != null) m_gridManager.RemoveObject (this);
    }


    protected override void OnPrepareForReuses () {
        base.OnPrepareForReuses ();
        
        SetPrimaryCoord (Vector2Int.zero, true);
        SetCoords (null);
    }

    protected virtual void SetPrimaryCoord (Vector2Int position, bool setTransform) {
        
        m_position = position;

        if (m_autoCoords == null || m_autoCoords.Length != 1) 
            m_autoCoords = new Vector2Int[] { position };
        else 
            m_autoCoords[0] = position;
        
        if (setTransform) {
            if (m_gridManager != null && m_gridManager.UnityGrid != null) {
                var grid = m_gridManager.UnityGrid;

                var pos = grid.GetCellCenterWorld (new Vector3Int (m_position.x, m_position.y, 0));
                transform.position = new Vector3(pos.x, pos.y, transform.position.z);
            } else {
                transform.position = new Vector3(m_position.x, m_position.y, transform.position.z);
            }
        }
    }
    protected virtual void SetCoords (Vector2Int[] coords) {
        m_coords = coords;
    }

}
