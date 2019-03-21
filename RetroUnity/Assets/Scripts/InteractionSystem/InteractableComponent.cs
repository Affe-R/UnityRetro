using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableComponent : MonoBehaviour
{
    public UnityEvent OnInteracted;
    public UnityEvent OnStopInteracting;

    float timer;
    bool timerRunning;
    
    public void Interact(InteractorComponent interactor)
    {
        OnInteracted.Invoke();
        timer = 0.1f;
        timerRunning = true;
    }

    public void StopInteracting()
    {
        OnStopInteracting.Invoke();
        timerRunning = false;
    }

    private void Update()
    {
        if (timerRunning)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                StopInteracting();
            }
        }
    }
}
