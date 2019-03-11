using UnityEngine;
using UnityEngine.UI;

public class LanderController : MonoBehaviour
{
    public float thrustForceDown;
    public float thrustForceRotate;
    public Text fuelText;

    private Rigidbody2D rb2d;
    private int fuel;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        fuel = 150;
        fuelText.text = "Fuel: " + fuel.ToString();
    }

    void FixedUpdate()
    {

        if (Input.GetButton("Horizontal") && fuel > 0)
        {
            rb2d.AddTorque(Input.GetAxis("Horizontal") * -thrustForceRotate);
            fuel--;
            fuelText.text = "Fuel: " + fuel.ToString();
        }

    }
    
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetButton("Vertical") && fuel > 0)
        {
            rb2d.AddRelativeForce(new Vector2(0, thrustForceDown));
            fuel--;
            fuelText.text = "Fuel: " + fuel.ToString();
        }

    }
}