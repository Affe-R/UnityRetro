using UnityEngine;
using UnityEngine.UI;

public class LanderController : MonoBehaviour
{
    public float thrustForceDown;
    public float thrustForceRotate;
    public Text fuelText;
    public int startFuel;
    public Image FuelImage;
    public GameObject thrustImage;

    private Rigidbody2D rb2d;
    private float fuel;
    private GameObject muzzle;
    private bool isThrusting;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        fuel = startFuel;
        fuelText.text = "Fuel: " + fuel.ToString();
    }

    void FixedUpdate()
    {
        FuelImage.fillAmount = fuel / startFuel;
    }
    
    void Update()
    {

        isThrusting = false;

        if (Input.GetButton("Vertical"))
        {
            ForwardThrust();
        }

        fuelText.text = "Fuel: " + fuel.ToString();

    }

    public void Rotate(float thrustHorizontal)
    {
        if (fuel > 0)
        {
            isThrusting = true;
            rb2d.AddTorque(thrustHorizontal * -thrustForceRotate);
            fuel--;
            if (muzzle == null) // (muzzle)
            {
                muzzle = Instantiate(thrustImage, transform.position, transform.rotation = Quaternion.identity);   // sets rotation to 0
            }
        }
    }

    public void ForwardThrust()
    {
        if (fuel > 0)
        {
            rb2d.AddRelativeForce(new Vector2(0, thrustForceDown));
            fuel--;
        }
    }

    public void StopThrusting()
    {
        if (muzzle)
        {
            isThrusting = false;
            Destroy(muzzle);
        }
    }

}