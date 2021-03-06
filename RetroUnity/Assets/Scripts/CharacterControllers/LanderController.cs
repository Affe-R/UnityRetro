﻿using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LanderController : MonoBehaviour
{
    public float thrustForceDown;
    public float thrustForceRotate;
    public Text fuelText;
    public int startFuel;
    public Image FuelImage;
    //public GameObject thrustImage;
    //public GameObject muzzleLeft;
    //public GameObject muzzleRight;
    //public GameObject muzzleDown;

    private Rigidbody2D rb2d;
    private float fuel;
    private bool hasLanded;
    //private GameObject muzzle;
    //private bool isThrusting;

    public UnityEvent onRotateRight;
    public UnityEvent onRotateLeft;
    public UnityEvent onForwardThrust;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        fuel = startFuel;
        //if(fuelText)
        //    fuelText.text = "Fuel: " + fuel.ToString();

        //if (muzzleLeft)
        //    muzzleLeft.SetActive(false);
        //if (muzzleRight)
        //    muzzleRight.SetActive(false);
        //if (muzzleDown)
        //    muzzleDown.SetActive(false);
    }

    void FixedUpdate()
    {
        if(FuelImage)
            FuelImage.fillAmount = GetFuelPercentage();
    }

    public float GetFuelPercentage()
    {
        return fuel / startFuel;
    }
    
    void Update()
    {

        //isThrusting = false;

        //if (Input.GetButton("Vertical"))
        //{
        //    ForwardThrust();
        //}

        //if (Input.GetButton("Horizontal"))
        //{
        //    Rotate(Input.GetAxis("Horizontal"));
        //}

        //if (Input.GetKeyDown("left")) muzzleRight.SetActive(true);
        //if (Input.GetKeyDown("right")) muzzleLeft.SetActive(true);
        //if (Input.GetKeyDown("up")) muzzleDown.SetActive(true);

        //if (Input.GetKeyUp("left")) muzzleRight.SetActive(false);
        //if (Input.GetKeyUp("right")) muzzleLeft.SetActive(false);
        //if (Input.GetKeyUp("up")) muzzleDown.SetActive(false);

        //if(fuelText)
        //    fuelText.text = "Fuel: " + fuel.ToString();
    }

    public void Rotate(float thrustHorizontal)
    {
        if (fuel > 0 && !hasLanded)
        {
            //isThrusting = true;
            rb2d.AddTorque(thrustHorizontal * -thrustForceRotate);
            fuel--;

            // aktivera thrust flames
            //if (thrustHorizontal > 0)
            //    muzzleLeft.SetActive(true);
            //if (thrustHorizontal < 0)
            //    muzzleRight.SetActive(true);

            if (thrustHorizontal > 0)
            {
                onRotateRight.Invoke();
            }
            else
            {
                onRotateLeft.Invoke();
            }
        }
    }

    public void ForwardThrust()
    {
        if (fuel > 0 && !hasLanded)
        {
            rb2d.AddRelativeForce(new Vector2(0, thrustForceDown));
            fuel--;
            //muzzleDown.SetActive(true);

            onForwardThrust.Invoke();
        }
    }

    public void Landed()
    {
        hasLanded = true;
    }

    //public void StopThrusting()
    //{
    //    if (muzzle)
    //    {
    //        isThrusting = false;
    //        Destroy(muzzle);
    //    }
    //}

}