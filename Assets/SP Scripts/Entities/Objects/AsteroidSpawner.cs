
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : TurnObject
{
    [Zenject.Inject] IBoundsManager m_boundManager;

    [SerializeField] ResettablePool m_pool;

    [SerializeField] int m_spawnTurnDelay       = 4;
    [SerializeField] int m_spawnDistance        = 2;
    [SerializeField] DirectionGridObject m_defaultAstroid;

    int m_spawnCounter;

    private void SpawnRandomAstroid (DirectionGridObject prefab = null)
    {
        var min = m_boundManager.Min;
        var max = m_boundManager.Max;

        var dir = Mathf.FloorToInt (Random.value * 2) * 2 - 1;
        var dirPos = dir * m_spawnDistance;


        if (Random.value > 0.5f) {

            var x = Random.Range (min.x, max.x + 1);
            var y = (dir > 0 ? min.y : max.y) - dirPos;

            SpawnAstroid (prefab, new Vector2Int (x, y), new Vector2Int (0, dir));
        } else {

            var y = Random.Range (min.y, max.y + 1);
            var x = (dir > 0 ? min.x : max.x) - dirPos;

            SpawnAstroid (prefab, new Vector2Int (x, y), new Vector2Int (dir, 0));
        
        }
    }
    private void SpawnAstroid (DirectionGridObject prefab, Vector2Int position, Vector2Int direction)
    {
        if (prefab == null) prefab = m_defaultAstroid;

        var inst = m_pool.Get (prefab);

        inst.SetPosition (position);
        inst.SetDirection (direction);

        inst.Restore ();
    }

    protected override TurnTask DoAction()
    {
        if (!IsGameRunning) return TurnTask.Done;
        
        ++m_spawnCounter;

        if (m_spawnTurnDelay > 0 && m_spawnCounter >= m_spawnTurnDelay) {

            SpawnRandomAstroid ();


            m_spawnCounter = 0;
        }



        return TurnTask.Done;
    }
}
