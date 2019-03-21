using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINewHighScore : MonoBehaviour
{
    public GameObject go;
    
    void OnEnable()
    {
        ScoreSystem.GetInstance().NewHighscore += EnableGameObject;
    }

    void OnDisable()
    {
        ScoreSystem.GetInstance().NewHighscore -= EnableGameObject;
    }

    void EnableGameObject(int value)
    {
        go.SetActive(true);
    }
}
