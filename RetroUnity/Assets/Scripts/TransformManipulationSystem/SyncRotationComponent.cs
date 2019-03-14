using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncRotationComponent : MonoBehaviour
{
    public Transform Target;

    void Update()
    {
        if(Target)
            transform.rotation = Target.rotation;
    }
}
