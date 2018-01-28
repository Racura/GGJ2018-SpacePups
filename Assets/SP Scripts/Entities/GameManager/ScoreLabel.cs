


using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreLabel : GameResponder
{
    [Zenject.Inject] IScoreManager m_scoreManager;
    int m_lastScore;

    [SerializeField] Text text;

    protected virtual void OnEnable () {
        UpdateScore ();
    }
    protected virtual void Update () {

        if (m_lastScore != m_scoreManager.Score) UpdateScore ();
    }

    private void UpdateScore()
    {
        m_lastScore = m_scoreManager.Score;
        text.text = m_lastScore.ToString ();
    }
}