using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipController : MonoBehaviour
{
    [SerializeField]ButtonTrigger thrusterButton, rotateLeftButton, rotateRightButton;
    public Transform lander;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lander)
        {
           //transform.rotation = lander.rotation;
        }
    }
}
