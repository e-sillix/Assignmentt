using UnityEngine;

public class TransfromCar : MonoBehaviour
{
    [SerializeField] private GameObject Glider;
    private bool isGliding = false;
    private Rigidbody rb;

    public float glideForce;   // forward wind
    public float liftForce;    // upward lift
    public float pitchTorque;  // W/S
    public float yawTorque;    // A/D
    private bool isTurnUP, isTurnDown;
    [SerializeField] private GameObject ButtonUP, ButtonDOWN;
    private PrometeoTouchInput throttlePTI,BackPTI;

    [SerializeField] private float dampingFactor;
    public LayerMask GroundLayer;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        throttlePTI = ButtonUP.GetComponent<PrometeoTouchInput>();
        BackPTI = ButtonDOWN.GetComponent<PrometeoTouchInput>();
    }

    public void TriggerGliding()
    {
        Debug.Log("Triggered Gliding.");
        Glider.SetActive(true);
        isGliding = true;
    }

    public void TriggerCancelGliding()
    {
        Glider.SetActive(false);
        isGliding = false;
    }
    void OnTriggerStay(Collider other)
    {
        Debug.Log("Staying on Ground");
        if (isGliding)
        {

            if ((((1 << other.gameObject.layer) & GroundLayer) != 0))
            {
                Debug.Log("Player has landed .");
                TriggerCancelGliding();

            }
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.G))
        {
            TriggerGliding();
        }
        if (!isGliding)
        {
            return;
        }


        Debug.Log("Gliding Control are being Applied.");
        // Forward force (simulate thrust)
        rb.AddForce(transform.forward * glideForce);

        // Upward lift (simulate air lift)
        // rb.AddForce(transform.up * liftForce * Time.fixedDeltaTime);

        // --- Controls while gliding ---

        // Pitch Up/Down (W/S)
        if (Input.GetKey(KeyCode.W)||throttlePTI.buttonPressed)
        {
            rb.AddTorque(transform.right * pitchTorque);
        }
        else if (Input.GetKey(KeyCode.S)||BackPTI.buttonPressed)
        {
            rb.AddTorque(-transform.right * pitchTorque);
        }


        if (Input.GetKey(KeyCode.A))
            rb.AddTorque(-transform.up * yawTorque * 1.5f);

        if (Input.GetKey(KeyCode.D))
            rb.AddTorque(transform.up * yawTorque * 1.5f);

        rb.AddForce(transform.up * liftForce);

        if (!Input.GetKey(KeyCode.W) &&
        !Input.GetKey(KeyCode.S) &&
        !Input.GetKey(KeyCode.A) &&
        !Input.GetKey(KeyCode.D)&& !throttlePTI.buttonPressed&& !BackPTI.buttonPressed) 
        {
            rb.angularVelocity = Vector3.Lerp(rb.angularVelocity, Vector3.zero,
            dampingFactor * Time.fixedDeltaTime);
        }

    }

    public void TurnOnMobileUsingSteer(float Yawing)
    {
        if (isGliding)
        {
            Debug.Log("Turn with:" + Yawing);
            rb.AddTorque(-transform.up * yawTorque * 1.5f * Yawing);
        }
    }
    public void TurnOnMobileUsingGyro(float Yawing)
    {
        if (isGliding)
        {

            Debug.Log("Turn with:" + Yawing);
            rb.AddTorque(-transform.up * yawTorque * 1.5f * Yawing);
        }
    }
    public void TurnUpMobileUsingButtonON()
    {
        isTurnUP = true;
    }
    public void TurnUpMobileUsingButtonOFF()
    {
        isTurnUP = false;
    }
    public void TurnDownMobileUsingButtonON()
    {
        isTurnDown = true;
    }
   
    public void TurnDownMobileUsingButtonOFF()
    {
        isTurnDown = false;
    }
    
}
