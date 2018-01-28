using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour, ITurnManager, IComparer<ITurnObject> {

    [SerializeField] float turnDelay = 0.1f;
    
    List <ITurnObject> m_list;
    bool m_sort;

    Coroutine m_coroutine;

    protected virtual void Awake () {
        if (m_list == null) m_list = new List<ITurnObject> ();
    }

    protected virtual void OnEnable () {
        if (m_coroutine != null) StopCoroutine (m_coroutine);
        m_coroutine = StartCoroutine (TurnCoroutine ());
    }

    public int TurnObjectCount { get { return m_list != null ? m_list.Count : 0; } }

    public void AddObject (ITurnObject turnObject) {

        if (m_list == null) m_list = new List<ITurnObject> ();

        m_sort = true;
        for (int i = 0; i < m_list.Count; ++i) {
            
            if (m_list[i] == null) continue;

            if (Compare (turnObject, m_list[i]) < 0) {
                m_list.Insert (i, turnObject);
                return;
            }
        }

        m_list.Add (turnObject);
    }

    public void RemoveObject (ITurnObject turnObject) {

        m_list.Remove (turnObject);
        m_sort = true;
    }

    public bool ContainsObject (ITurnObject turnObject) {
        return m_list != null && m_list.Contains (turnObject);
    }

    IEnumerator TurnCoroutine () {

        while (true) {
            yield return new WaitForSeconds (turnDelay);

            for (int i = 0; i < m_list.Count; ++i) {
                
                var obj = m_list[i];

                if (!obj.IsActive) continue;
                var task = obj.DoAction ();

                while (task != null && !task.IsDone)
                    yield return null;

                if (m_sort) {
                    m_sort = false;


                    //var str = "";
                    //for (int u = 0; u < m_list.Count; ++u)
                    //    str += (m_list[u] as Component).name;
                    //Debug.Log (str);

                    var index = m_list.IndexOf (obj);

                    if (index >= 0) i = index;
                    else            --i;
                }
            }
        }
    }

    public int Compare(ITurnObject x, ITurnObject y)
    {
        if (x == null) return -1;
        if (y == null) return 1;

        return -x.Priority.CompareTo (y.Priority);
    }
}
