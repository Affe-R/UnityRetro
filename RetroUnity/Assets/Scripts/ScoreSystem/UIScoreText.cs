using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScoreText : MonoBehaviour
{
    public Text text;
    
    void OnEnable()
    {
        ScoreSystem.GetInstance().NewScore += UpdateText;
        UpdateText(ScoreSystem.GetInstance().GetCurrentScore());
    }

    void UpdateText(int value)
    {
        text.text = value.ToString();
    }
}
