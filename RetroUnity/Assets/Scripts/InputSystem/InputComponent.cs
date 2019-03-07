using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputComponent : MonoBehaviour
{
    public UnityEventVector2 onMove;
    public UnityEvent onJump;

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float verical = Input.GetAxis("Vertical");

        onMove.Invoke(new Vector2(horizontal, verical));

        if(Input.GetButtonDown("Jump"))
            onJump.Invoke();
    }
}

[System.Serializable]
public class UnityEventVector2 : UnityEvent<Vector2> {}
