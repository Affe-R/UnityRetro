using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    //On Win/Loose save Score
    int Score = 1000;
    int HighScore = 0;

    public Text RecordText;

    void Start()
    {
        DynamicGI.UpdateEnvironment();
        HighScore = PlayerPrefs.GetInt("Highscore");
        RecordText.text = HighScore.ToString() + "   S e c     H i g h s c o r e";
    }

    public void ScoreUpdate(int NewScore)
    {
        Score = NewScore;
        CheckNewHighscore(Score);
    }

    void CheckNewHighscore(int ScoreToCheck)
    {
        if (ScoreToCheck > HighScore)
        {
            PlayerPrefs.SetInt("Highscore", ScoreToCheck);
            HighScore = ScoreToCheck;
            RecordText.text = HighScore.ToString() + "   S e c     H i g h s c o r e";

        }
    }
}
