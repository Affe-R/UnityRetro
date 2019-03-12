using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateMachine
{
    public string mainMenuSceneName;
    public string playingSceneName;

    static GameStateMachine instance;

    static public GameStateMachine GetInstance()
    {
        if(instance == null)
        {
            instance = new GameStateMachine();
        }

        return instance;
    }

    public void LoadState(GameState state)
    {
        if(state.scene != null && SceneManager.GetActiveScene().name != state.scene.name)
            SceneManager.LoadScene(state.scene.name);
            
        Time.timeScale = state.timeScale;
    }
}
