


using UnityEngine;

public class ScoreManager : GameResponder, IScoreManager
{
    int m_score;
    public int Score { get { return m_score;} }


    public void AddScore (int score) {
        m_score += score;
    }

    public override void OnResetGame() {
        m_score = 0;
    }
}