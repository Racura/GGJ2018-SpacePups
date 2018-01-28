using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScoreManager {

    void AddScore (int score);

    int Score { get; }
}
