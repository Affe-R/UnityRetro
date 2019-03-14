using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableComponent : MonoBehaviour
{
    public UnityEvent OnInteracted;
    
    public void Interact(InteractorComponent interactor)
    {
        OnInteracted.Invoke();
    }
}
