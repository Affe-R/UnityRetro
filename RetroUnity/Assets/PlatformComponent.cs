using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatformComponent : MonoBehaviour
{
    public int ScoreValue;
    Text text;
    GameObject platform;
    
    void Awake()
    {
        text = GetComponentInChildren<Text>();
        platform = GetComponentInChildren<SpriteRenderer>().gameObject;
    }

    public void SetValue(int value)
    {
        if(text)
        {
            ScoreValue = value;
            text.text = value + "x";
        }
    }

    public void SetLength(float length)
    {
        if(!platform) return;

        Vector3 scale = platform.transform.localScale;
        scale.x = length;
        platform.transform.localScale = scale;
    }
}
