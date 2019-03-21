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

    Highscore[] HighscoreArray;

    public InputField NameInput;
    public Text ScoreText;
    public Text RecordText;

    string filePath;

    void Start()
    {
        DynamicGI.UpdateEnvironment();
        filePath = Path.Combine(Application.dataPath, "save.json");

        HighscoreArray = new Highscore[3];

        highestScore = LoadHighscoreFromJson(0);
        UpdateScoreText();
        UpdateHighScoreText();

        AddScore(1 + highestScore.Score);

        NameInput.gameObject.active = false;
    }

    public void AddScore(int ScoreToAdd)
    {
        Score += ScoreToAdd;
        UpdateScoreText();
    }

    public void CheckNewHighscore()
    {
        if (Score > highestScore.Score)
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
        Highscore TEMP = LoadHighscoreFromJson(0);
        RecordText.text = TEMP.Name + " : " + TEMP.Score.ToString();
    }

    void UpdateScoreText()
    {
        ScoreText.text = "Score : " + Score.ToString();
    }

    void SaveHighscoreToJson(Highscore inHighscore)
    {
        HighscoreArray[0] = highestScore;
        HighscoreArray[1] = new Highscore("Pilot", 0);
        HighscoreArray[2] = new Highscore("Pilot", 0);
        string HighscoreStr = JsonUtility.ToJson(HighscoreArray);
        File.WriteAllText(filePath, HighscoreStr);
    }

    Highscore LoadHighscoreFromJson(int Index)
    {
        jsonStrSerialized = File.ReadAllText(filePath);
        Highscore[] TempArr = JsonUtility.FromJson<Highscore[]>(File.ReadAllText(filePath));
        return TempArr[Index];
    }
}
