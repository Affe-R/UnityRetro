using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawThrust : MonoBehaviour
{

    public GameObject landaren; // = GameObject("Lander");// landaren;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.RotateAroundLocal(0,0,0, 45f);
        //transform.RotateAround(transform.position, transform.up, Time.deltaTime * 90f);
        transform.Rotate(0, 0, 150 * Time.deltaTime);
        //transform.rotation = (0, 0, landaren.transform.rotation.z);
        //transform.rotation = Quaternion.Euler(0,0,landaren.rotation.z);

        // A rotation 30 degrees around the y-axis
        //Quaternion rotation = landaren.Quaternion.Euler(0, 30, 0);
    }
}
