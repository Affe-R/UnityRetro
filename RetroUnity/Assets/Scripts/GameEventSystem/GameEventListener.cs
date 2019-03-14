using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public GameEvent Event;
    public UnityEvent OnTriggered;

    void OnEnable()
    {
        if(Event)
        {
            Event.OnTriggered += Trigger;
        }
    }

    void OnDisable()
    {
        if(Event)
            Event.OnTriggered -= Trigger;
    }

    public void Trigger()
    {
        OnTriggered.Invoke();
    }

    

}
