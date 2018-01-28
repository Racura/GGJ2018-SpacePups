using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnList : MonoBehaviour {

    [Zenject.Inject] IGridManager m_gridManager;

    [SerializeField] SpawnPoint[] m_spawnPoints;

    public SpawnPoint[] SpawnPoints { get { return m_spawnPoints; } }

    public bool TryFindEmpty (out SpawnPoint spawnPoint) {
    
        var rnd = Random.Range (0, m_spawnPoints.Length);

        for (int i = 0; i < m_spawnPoints.Length; ++i) {
            var ii = i + rnd;
            if (ii >= m_spawnPoints.Length) ii -= m_spawnPoints.Length;

            var point = m_spawnPoints[ii];
            int index = 0;

            var coll = m_gridManager.GetGridObject (point.position, ref index);
            if (coll == null && point.direction.x != 0 && point.direction.y != 0) 
                coll = m_gridManager.GetGridObject (point.position + point.direction, ref index);

            if (coll == null)
            {
                spawnPoint = point;
                return true;
            }
        }

        spawnPoint = new SpawnPoint ();
        return false;    
    }


    protected virtual void OnDrawGizmos () {

        var grid = FindObjectOfType<Grid>();

        if (grid != null)  {
            Gizmos.color = Color.green;

            for (int i = 0; i < m_spawnPoints.Length; ++i) {

                SpawnPoint spawnPoint = m_spawnPoints[i];

                var pos = grid.GetCellCenterWorld (new Vector3Int(spawnPoint.position.x, spawnPoint.position.y, 0));
                var dir = new Vector3(spawnPoint.direction.x, spawnPoint.direction.y, 0);

                Gizmos.DrawSphere (pos, 0.4f);
                Gizmos.DrawRay (pos, dir);
            }
        }
    }
}

[System.Serializable]
public struct SpawnPoint {
    public Vector2Int position;
    public Vector2Int direction;
}
