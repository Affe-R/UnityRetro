using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSFX : MonoBehaviour
{

    public AudioClip SFX;
    public AudioSource SFXSource;

    // Start is called before the first frame update
    void Start()
    {
        SFXSource.clip = SFX;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") || (Input.GetButtonDown("Jump P2")))
        {
            SFXSource.Play();
        }
    }
}
