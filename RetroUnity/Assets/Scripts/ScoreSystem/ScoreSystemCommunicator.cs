using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystemCommunicator : MonoBehaviour
{
    public void SetHighScoreHolder(string name)
    {
        ScoreSystem.GetInstance().SetHighscore(name);
    }
}
