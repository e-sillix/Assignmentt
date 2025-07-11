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
        rb.AddForce(transform.forward * glideForce );

        // Upward lift (simulate air lift)
        // rb.AddForce(transform.up * liftForce * Time.fixedDeltaTime);
        rb.AddForce(Vector3.up * liftForce);
        // --- Controls while gliding ---

        // Pitch Up/Down (W/S)
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddTorque(-transform.right * pitchTorque );
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.AddTorque(transform.right * pitchTorque );
        }

        // Yaw Left/Right (A/D)
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddTorque(-transform.up * yawTorque);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddTorque(transform.up * yawTorque );
        }
    }
}
