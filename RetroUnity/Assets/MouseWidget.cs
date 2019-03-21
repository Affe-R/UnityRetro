using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWidget : MonoBehaviour
{
    Vector3 position;
    Vector3 lastPosition;
    Vector3 posDelta;

    [SerializeField] Transform ThrusterLeft;
    [SerializeField] Transform ThrusterRight;
    [SerializeField] Transform ThrusterDown;
    SpriteRenderer tL;
    SpriteRenderer tR;
    SpriteRenderer tD;

    private void Start()
    {
        tL = ThrusterLeft.GetComponent<SpriteRenderer>();
        tR = ThrusterRight.GetComponent<SpriteRenderer>();
        tD = ThrusterDown.GetComponent<SpriteRenderer>();
    }

    public void UpdatePosition(Vector3 _position)
    {
        lastPosition = position;
        position = _position;
        posDelta = position - lastPosition;
        transform.position = _position;
        float dirX = Mathf.Clamp(posDelta.x,-1,1);
        float dirY = Mathf.Clamp(posDelta.y,-1,1);
        if (dirY >= 0)
        {
            tD.color = Color.white;
        } else
        {
            tD.color = Color.clear;
        }

        if (dirX < 0)
        {
            tL.color = Color.clear;
            tR.color = Color.white;
        } else if (dirX > 0)
        {
            tL.color = Color.white;
            tR.color = Color.clear;
        }
        else
        {
            tL.color = Color.clear;
            tR.color = Color.clear;
        }
    }
}
