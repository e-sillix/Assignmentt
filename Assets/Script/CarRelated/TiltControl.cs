using UnityEngine;

public class TiltControl : MonoBehaviour
{
   [SerializeField] private bool useTiltControls = false;
   [SerializeField] private float tiltSensitivity = 2f;

    private void Start()
    {
        Input.gyro.enabled = true;
    }
    public void SetUseTiltControls(bool use)
    {
        useTiltControls = use;
    }
    private void Update()
    {
        if (useTiltControls)
        {
            float tilt = Input.acceleration.x;
            tilt *= tiltSensitivity;
            tilt = Mathf.Clamp(tilt, -1f, 1f);

            if (Mathf.Abs(tilt) < 0.1f)
                tilt = 0f;

            GetComponent<PrometeoCarController>().TurnTheCar(-tilt);
        }
    }
}
