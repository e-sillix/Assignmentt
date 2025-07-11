using UnityEngine;

public class Physics : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Rigidbody rb;
    public float pitchTorque,yawTorque;
    void Start()
    {
        rb=GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
    rb.AddTorque(-transform.right * pitchTorque);

if (Input.GetKey(KeyCode.S))
    rb.AddTorque(transform.right * pitchTorque);

// Stronger yaw
if (Input.GetKey(KeyCode.A))
    rb.AddTorque(transform.forward * yawTorque * 1.5f);

if (Input.GetKey(KeyCode.D))
    rb.AddTorque(-transform.forward * yawTorque * 1.5f);
    }
}
