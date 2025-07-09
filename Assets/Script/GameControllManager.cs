using UnityEngine;

public class GameControllManager : MonoBehaviour
{
    [SerializeField] private ControlType controlType = ControlType.SteerWheel;
    [SerializeField] private GameObject SteerWheelUI, TiltUI;
    private PrometeoCarController prometeoCarController;
    void Start()
    {
        prometeoCarController = FindAnyObjectByType<PrometeoCarController>();
        SetControlType(controlType);
    }

    public void SetControlType(ControlType Type)
    {
        if (ControlType.SteerWheel == Type)
        {
            if (prometeoCarController != null)
            {
                prometeoCarController.GetComponent<TiltControl>().SetUseTiltControls(false);
                prometeoCarController.SetIsTiltControlEnabled(false);
            }
            SteerWheelUI.SetActive(true);
            TiltUI.SetActive(false);
        }
        else if (ControlType.Tilt == Type)
        {
            if (prometeoCarController != null)
            {
                prometeoCarController.GetComponent<TiltControl>().SetUseTiltControls(true);
                prometeoCarController.SetIsTiltControlEnabled(false);
            }
            SteerWheelUI.SetActive(false);
            TiltUI.SetActive(true);
        }

        controlType = Type;
    }
    public ControlType GetControlType()
    {
        return controlType;
    }
    public void RemoveAllControls()
    {
        SteerWheelUI.SetActive(false);
        TiltUI.SetActive(false);
    }
}
public enum ControlType
{
    SteerWheel,
    Tilt
}
