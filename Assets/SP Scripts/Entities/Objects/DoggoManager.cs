using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoggoManager : TurnObject
{
    [SerializeField] ResettablePool m_pool;

    [SerializeField] int m_tintCount;

    [Header("Spawn List")]
    [SerializeField] SpawnList m_doggoSpawnList;
    [SerializeField] SpawnList m_ballSpawnList;
    [SerializeField] SpawnList m_finalBallSpawnList;

    [Header("Ball List")]
    [SerializeField] Ball m_normalBallPrefab;
    [SerializeField] Ball m_finalBallPrefab;
    
    List<ResettableObjectRef<Doggo>> m_doggoList;
    List<ResettableObjectRef<Ball>> m_ballList;

    public int GetDoggoCount () {
        if (m_doggoList == null) return 0;
        return m_doggoList.Count;
    }

    public bool TrySpawn (Doggo doggoPrefab)
    {
        if (m_doggoList == null) m_doggoList = new List<ResettableObjectRef<Doggo>>();
        if (m_ballList == null)  m_ballList = new List<ResettableObjectRef<Ball>>();

        if (doggoPrefab == null) return false;

        SpawnPoint doggoSpawnPoint;
        SpawnPoint ballSpawnPoint;

        if (!m_doggoSpawnList.TryFindEmpty (out doggoSpawnPoint)
            || !m_ballSpawnList.TryFindEmpty (out ballSpawnPoint))
            return false;

        int tint;

        if (!TryGetTint (out tint)) return false;

        var doggoInst = m_pool.Get (doggoPrefab);

        doggoInst.SetPosition (doggoSpawnPoint.position);
        doggoInst.SetDirection (doggoSpawnPoint.direction);
        doggoInst.SetTint (tint);

        doggoInst.Restore ();

        var ballInst = m_pool.Get (m_normalBallPrefab);

        ballInst.SetPosition (ballSpawnPoint.position);
        ballInst.SetTint (tint);

        ballInst.Restore ();

        m_doggoList.Add (new ResettableObjectRef<Doggo>(doggoInst));
        m_ballList.Add (new ResettableObjectRef<Ball>(ballInst));

        return true;
    }

    private bool TryGetTint(out int tint)
    {
        var rnd = Random.Range (0, m_tintCount);

        for (int i = 0; i < m_tintCount; ++i) {

            var t = (i + rnd) % m_tintCount;
            bool exists = false;

            for (int j = 0; j < m_doggoList.Count; ++j) {
                if (m_doggoList[j].HasValue && m_doggoList[j].Value.Tint == t)
                    exists = true;;

            }

            if (!exists) {
                tint = t;
                return true;
            }
        }
        

        tint = -1;
        return false;
    }

    bool HasBall(Doggo doggo)
    {   
        if (doggo == null || !doggo.IsActive) return true;

        if (m_ballList == null) return false;

        for (int i = m_ballList.Count - 1; i >= 0; --i) {

            var b = m_ballList[i].Value;

            if (b.Tint == doggo.Tint) return true;
        }

        return false;
    }

    bool TrySpawnBall (Doggo doggo)
    {   
        if (doggo.State == DoggoState.Retiring) return false;

        var ballPrefab = m_normalBallPrefab;
        var spawnList = m_ballSpawnList;

        if (Random.value > 0.8f) {
            ballPrefab = m_finalBallPrefab;
            spawnList = m_finalBallSpawnList;
        }

        if (ballPrefab == null) return false;

        var spawnPoint = new SpawnPoint ();

        if (!spawnList.TryFindEmpty (out spawnPoint)) return false;

        var ballInst = m_pool.Get (ballPrefab);

        ballInst.SetPosition (spawnPoint.position);
        ballInst.SetTint (doggo.Tint);

        ballInst.Restore ();

        m_ballList.Add (new ResettableObjectRef<Ball>(ballInst));

        return true;
    }



    public override void OnResetGame () {
        base.OnResetGame ();



        if (m_doggoList == null) m_doggoList = new List<ResettableObjectRef<Doggo>>();
        if (m_ballList == null)  m_ballList = new List<ResettableObjectRef<Ball>>();


        for (int i = m_doggoList.Count - 1; i >= 0; --i) {
            if (m_doggoList[i].HasValue) m_doggoList[i].Value.Retire ();            
            m_doggoList.RemoveAt (i);
        }

        for (int i = m_ballList.Count - 1; i >= 0; --i) {
            if (m_ballList[i].HasValue) m_ballList[i].Value.Retire ();            
            m_ballList.RemoveAt (i);
        }


    }

    protected override TurnTask DoAction()
    {
        if (!IsGameRunning) return TurnTask.Done;
        
        if (m_doggoList == null) m_doggoList = new List<ResettableObjectRef<Doggo>>();
        if (m_ballList == null)  m_ballList = new List<ResettableObjectRef<Ball>>();

        for (int i = m_doggoList.Count - 1; i >= 0; --i) {
            if (!m_doggoList[i].HasValue || m_doggoList[i].Value.State == DoggoState.Retiring) {
                m_doggoList.RemoveAt (i);
            }
        }

        for (int i = m_ballList.Count - 1; i >= 0; --i) {
            if (!m_ballList[i].HasValue) m_ballList.RemoveAt (i);
        }

        for (int i = m_doggoList.Count - 1; i >= 0; --i) {
            if (!HasBall (m_doggoList[i].Value)) TrySpawnBall (m_doggoList[i].Value);
        }

        return TurnTask.Done;
    }
}
