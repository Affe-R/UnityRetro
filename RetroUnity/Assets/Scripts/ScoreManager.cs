using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Highscore
{
    public string Name;
    public int Score;

    public Highscore(string NameIn, int ScoreIn)
    {

        Name = NameIn;
        Score = ScoreIn;
    }
}


public class ScoreManager : MonoBehaviour
{
    string jsonStrSerialized;
    int Score = 0;
    string ScoreDisplay;
    Highscore highestScore = new Highscore(default, default);

    public Text RecordText;

    string filePath;

    void Start()
    {
        DynamicGI.UpdateEnvironment();
        filePath = Path.Combine(Application.dataPath, "save.json");

        if (LoadHighscoreFromJson() == null)
            SaveHighscoreToJson(highestScore);
        else
            highestScore = LoadHighscoreFromJson();

        UpdateHighScoreText();

        AddScore(1);
    }

    public void AddScore(int ScoreToAdd)
    {
        Score += ScoreToAdd;
        CheckNewHighscore(Score);
    }

    void CheckNewHighscore(int ScoreToCheck)
    {
        if (ScoreToCheck >= highestScore.Score)
        {
            highestScore = new Highscore("Highest", ScoreToCheck);

            SaveHighscoreToJson(highestScore);

            highestScore.Score = ScoreToCheck;

            UpdateHighScoreText();
        }
    }

    void UpdateHighScoreText()
    {
        Highscore TEMP = LoadHighscoreFromJson();
        RecordText.text = TEMP.Name + " : " + TEMP.Score.ToString();
    }

    void SaveHighscoreToJson(Highscore inHighscore)
    {
        string HighscoreStr = JsonUtility.ToJson(highestScore);
        File.WriteAllText(filePath, HighscoreStr);
    }

    Highscore LoadHighscoreFromJson()
    {
        jsonStrSerialized = File.ReadAllText(filePath);
        return JsonUtility.FromJson<Highscore>(File.ReadAllText(filePath));
    }
}
