using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfHighscore : MonoBehaviour
{
    public void Check()
    {
        ScoreSystem.GetInstance().CheckIfNewHighscore();
    }
}
