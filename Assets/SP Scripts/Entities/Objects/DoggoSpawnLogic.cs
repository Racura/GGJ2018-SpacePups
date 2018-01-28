
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoggoSpawnLogic : TurnObject
{
    [Zenject.Inject] IScoreManager m_scoreManager;

    [SerializeField] RangeI m_scoreRange        = new RangeI (0, 100);
    [SerializeField] DoggoManager m_spawner;

    [SerializeField] Doggo[] m_prefabs;
    [SerializeField] RangeI m_spawnTurnDelay     = 4;
    [SerializeField] RangeI m_maxDoggoCount        = 2;
    

    int m_spawnCounter;
    int m_delay;

    public override void OnResetGame () {
        base.OnResetGame ();

        m_delay = m_spawnCounter = 0;
    }
    
    protected override TurnTask DoAction()
    {
        if (!IsGameRunning) return TurnTask.Done;
        if (!m_scoreRange.Inside (m_scoreManager.Score)) return TurnTask.Done;

        
        ++m_spawnCounter;

        if (m_delay == 0) m_delay = m_spawnTurnDelay.Random ();

        if (m_spawnCounter >= m_delay && m_spawner != null) {

            if (m_spawner.GetDoggoCount() < m_maxDoggoCount.Random()) {
                m_spawner.TrySpawn (m_prefabs[Random.Range (0, m_prefabs.Length)]);
            }

            m_spawnCounter = 0;
            m_delay = m_spawnTurnDelay.Random ();
        }



        return TurnTask.Done;
    }
}
