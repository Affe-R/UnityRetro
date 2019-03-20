using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public Transform bgSprite;
    public Vector3 bgStartPos;
    public Vector3 bgEndPos;


    void Update()
    {
        if(bgSprite != null)bgSprite.transform.position = Vector3.LerpUnclamped(bgSprite.transform.position, bgEndPos, Time.deltaTime); 
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Playing",LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
