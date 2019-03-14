using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InputButton 
{
    public string InputName;
    public UnityEvent OnPressed;

    public void Update()
    {
        if(Input.GetButtonDown(InputName))
            OnPressed.Invoke();
    }
}

[System.Serializable]
public class InputKey
{
    public KeyCode Key;
    public UnityEvent OnPressed;

    public void Update()
    {
        if(Input.GetKeyDown(Key))
            OnPressed.Invoke();
    }
}

public class InputComponent : MonoBehaviour
{
    public InputButton[] Buttons;
    public InputKey[] Keys;
    
    void Update()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            if(Buttons[i] != null)
            {
                Buttons[i].Update();
            }
        }

        for (int i = 0; i < Keys.Length; i++)
        {
            if(Keys[i] != null)
                Keys[i].Update();
        }
    }
}
