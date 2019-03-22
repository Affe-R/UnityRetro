using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHighScore : MonoBehaviour
{
    public Text text;
    void Start()
    {
        string holder = ScoreSystem.GetInstance().GetHighscoreHolder();
        string score = ScoreSystem.GetInstance().GetHighscore().ToString();
        if(!text)
            text = GetComponent<Text>();

        if(text)
            text.text = holder + " " + score;
    }
}
