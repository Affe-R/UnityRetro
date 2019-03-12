using UnityEngine;
using UnityEngine.Events;

public class LanderCollision : MonoBehaviour
{
    public LayerMask whatLayer;
    public LayerMask Mountains;
    public Transform groundCheck;
    public float groundCheckRadius;
    public float topLandingSpeed;
    public GameObject explosion;
    public int maxLandAngle;

    public float maxLandingVelocityMagnitude;

    public UnityEvent onSuccesfulLanding;
    public UnityEvent onCrash;

    private Rigidbody2D rb2d;
    private bool grounded = false;
    private bool canLand = true;
    private bool hitMountain = false;

    private Vector2 velocity;

    // Start is called before the first frame update
    void Start()
    {
        groundCheck = GetComponent<Transform>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {    
        if(col.contacts[0].normal == Vector2.up && velocity.magnitude < maxLandingVelocityMagnitude && IsShipUpright())
            onSuccesfulLanding.Invoke();
        else
            Explode();
    }

    bool IsShipUpright()
    {
        float angle = Quaternion.Angle(transform.rotation, Quaternion.Euler(Vector3.zero));
        return angle < maxLandAngle;
    }

    void FixedUpdate()
    {
        velocity = rb2d.velocity;
    }

    // Update is called once per frame
    void Update()
    {
        //grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("groundLayer"));
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatLayer);

        if (grounded && canLand)
        {
            canLand = false;

            if (rb2d.velocity.y < -topLandingSpeed)
            {
                Debug.Log("VERTICAL VELOCITY TOO HIGH");
                Explode();
            }

            if (rb2d.velocity.x < -topLandingSpeed)
            {
                Debug.Log("HORIZONTAL VELOCITY TOO HIGH");
                Explode();
            }

            //get current rotation
            Vector3 currentEuler = transform.rotation.eulerAngles;

            if (currentEuler.z > maxLandAngle && currentEuler.z < 360 - maxLandAngle)
            {
                Debug.Log("ANGLE TOO STEEP");
                Explode();
            }

        }

        hitMountain = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, Mountains);

        if (hitMountain)
        {
            Debug.Log("BEEEERG!!!!");
            Explode();
        }

    }

    void Explode()
    {
        onCrash.Invoke();

        Instantiate(explosion, transform.position, transform.rotation);// = Quaternion.identity);   // sets rotation to 0
        Destroy(gameObject);
    }
}