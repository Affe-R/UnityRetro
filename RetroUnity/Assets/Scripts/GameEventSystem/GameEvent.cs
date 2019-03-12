using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Event")]
public class GameEvent : ScriptableObject
{
    public delegate void DefaultGameEvent();
    public DefaultGameEvent OnTriggered;

    public void Trigger()
    {
        OnTriggered?.Invoke();
    }
}
