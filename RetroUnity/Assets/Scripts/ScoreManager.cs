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
    int Score;
    string ScoreDisplay;
    Highscore highestScore = new Highscore(default, default);

    Highscore[] HighscoreArray;

    public InputField NameInput;
    public Text ScoreText;
    public Text RecordText;

    string filePath;

    void Start()
    {
        DynamicGI.UpdateEnvironment();
        filePath = Path.Combine(Application.dataPath, "save.json");

        Score = PlayerPrefs.GetInt("Score");

        highestScore = LoadHighscoreFromJson();

        UpdateScoreText();
        UpdateHighScoreText();

        NameInput.gameObject.active = false;
    }

    public void AddScore(int ScoreToAdd)
    {
        Score += ScoreToAdd;
        PlayerPrefs.SetInt("Score", Score);
        UpdateScoreText();
    }

    public void CheckNewHighscore()
    {
        ResetScore();

        if (Score >= highestScore.Score)
        {
            NameInput.gameObject.active = true;
        }
    }

    public void SetNewHighscore()
    {
        if (NameInput.text != "")
            highestScore = new Highscore(NameInput.text, Score);
        else
            highestScore = new Highscore("Pilot", Score);

        SaveHighscoreToJson(highestScore);
        UpdateHighScoreText();
    }


    void UpdateHighScoreText()
    {
        Highscore TEMP = LoadHighscoreFromJson();
        RecordText.text = TEMP.Name + " : " + TEMP.Score.ToString();
    }

    void UpdateScoreText()
    {
        ScoreText.text = Score.ToString();
    }

    void SaveHighscoreToJson(Highscore inHighscore)
    {
        string HighscoreStr = JsonUtility.ToJson(highestScore);
        File.WriteAllText(filePath, HighscoreStr);
    }

    public Highscore LoadHighscoreFromJson()
    {
        jsonStrSerialized = File.ReadAllText(filePath);
        return JsonUtility.FromJson<Highscore>(File.ReadAllText(filePath));
    }

    public void ResetScore()
    {
        PlayerPrefs.SetInt("Score", 0);
    }
}
