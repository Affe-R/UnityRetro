using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "GameState")]
public class GameState : ScriptableObject
{
    public Object scene;
    public float timeScale = 1;
    public MonoBehaviour[] behaviors;

    GameStateMachine stateMachine;

    public void Load()
    {
        this.stateMachine = GameStateMachine.GetInstance();
        stateMachine.LoadState(this);
    }
}
