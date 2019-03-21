using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreSystem
{
    // public UnityEvent OnNewHighscore;
    // public UnityEventInt OnNewScore;

    public delegate void ScoreEvent(int score);
    public ScoreEvent NewScore;
    public ScoreEvent NewHighscore;

    int currentScore = 0;

    static ScoreSystem scoreSystem;

    public static ScoreSystem GetInstance()
    {
        if(scoreSystem == null)
            scoreSystem = new ScoreSystem();

        return scoreSystem;
    }

    public int GetHighscore()
    {
        return PlayerPrefs.GetInt("score", 0);
    }

    public string GetHighscoreHolder()
    {
        return PlayerPrefs.GetString("highScoreHolder", "NULL");
    }

    public void SetHighscore(string name)
    {
        PlayerPrefs.SetString("highScoreHolder", name);
        PlayerPrefs.SetInt("score", currentScore);
    }

    public void AddToScore(int value)
    {
        currentScore += value;
        // OnNewScore.Invoke(currentScore);
    }

    public bool CheckIfNewHighscore()
    {
        return GetHighscore() < currentScore;
    }

    public void ResetScore()
    {
        currentScore = 0;
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }
}

// [System.Serializable]
// public class UnityEventInt : UnityEvent<int> {}
