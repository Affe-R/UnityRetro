using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFinalScore : MonoBehaviour
{
    public Text text;

    void OnEnable()
    {
        if(!text)  
            text = GetComponent<Text>();

        if(text)
            text.text = "Final Score: " + ScoreSystem.GetInstance().GetCurrentScore().ToString();
    }
}
