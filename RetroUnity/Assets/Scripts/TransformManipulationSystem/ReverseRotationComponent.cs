using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseRotationComponent : MonoBehaviour
{
    public Transform Target;

    void Update()
    {
        if (Target)
            //transform.rotation = (Quaternion)Target.transform.rotation;
            transform.localRotation = new Quaternion(Target.localRotation.x * -1.0f,
                                            Target.localRotation.y,
                                            Target.localRotation.z,
                                            Target.localRotation.w * -1.0f);
    }
}
