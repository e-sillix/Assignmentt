using UnityEngine;

public class TiltControl : MonoBehaviour
{
   [SerializeField] private bool useTiltControls = false;
   [SerializeField] private float tiltSensitivity = 2f;
    private PrometeoCarController prometeoCarController;
    private TransfromCar transformCar;

    private void Start()
    {
        Input.gyro.enabled = true;
        prometeoCarController = GetComponent<PrometeoCarController>();
        transformCar = GetComponent<TransfromCar>();
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

            prometeoCarController.TurnTheCar(-tilt);
            transformCar.TurnOnMobileUsingGyro(-tilt);
            
        }
    }
}
