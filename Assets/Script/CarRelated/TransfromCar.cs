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

    [SerializeField] private float dampingFactor ;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
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

    void FixedUpdate()
    {
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
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddTorque(-transform.right * pitchTorque);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.AddTorque(transform.right * pitchTorque);
        }


        if (Input.GetKey(KeyCode.A))
            rb.AddTorque(transform.forward * yawTorque * 1.5f);

        if (Input.GetKey(KeyCode.D))
            rb.AddTorque(-transform.forward * yawTorque * 1.5f);

        rb.AddForce(transform.up * liftForce);

        if (!Input.GetKey(KeyCode.W) &&
        !Input.GetKey(KeyCode.S) &&
        !Input.GetKey(KeyCode.A) &&
        !Input.GetKey(KeyCode.D))
    {
        rb.angularVelocity = Vector3.Lerp(rb.angularVelocity, Vector3.zero,
        dampingFactor * Time.fixedDeltaTime);
    }

    }
    
}
