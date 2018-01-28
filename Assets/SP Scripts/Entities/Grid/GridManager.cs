using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour, IGridManager {

    [SerializeField] UnityEngine.Grid m_grid;

    List <IGridObject> m_list;
    IGridObject[] m_cal;


    public int ObjectCount { get { return m_list != null ? m_list.Count : 0; } }
    public UnityEngine.Grid UnityGrid { get { return m_grid; } }


    protected virtual void Awake () {
        Initialize () ;
    }
    
    protected virtual void Initialize () {
        if (m_list == null) m_list = new List<IGridObject> ();
    }
    
    public void AddObject (IGridObject gridObject) {
        Initialize () ;
        m_list.Add (gridObject);
        m_cal = null;
    }
    public void RemoveObject (IGridObject gridObject){
        Initialize () ;
        m_list.Remove (gridObject);
        m_cal = null;
    }
    public bool ContainsObject (IGridObject gridObject){
        return m_list != null  && m_list.Contains (gridObject);
    }

    public IGridObject GetGridObject (Vector2Int position, ref int index) {
    
        for (; index < m_list.Count; ++index)
        {
            var obj = m_list[index];

            if (obj == null || !obj.IsActive) continue;

            var contains    = obj.PrimaryCoord == position;

            if (obj.Coords != null) {
                var coords      = obj.Coords;
                for (int j = 0; !contains && j < coords.Length; ++j) {
                    if (coords[j] == position) {
                        contains = true;
                        break;
                    }
                } 
            }


            if (contains) {
                ++index;
                return obj;
            }
        }

        return null;
    }
    
    public IGridObject[] GetGridState () {
        if (m_cal == null) m_cal = m_list.ToArray ();
        return m_cal;
    }
}
