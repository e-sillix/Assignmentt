using UnityEngine;
using UnityEngine.UI;

public class CheckBoxUI : MonoBehaviour
{
    public Toggle steerToggle;
    public Toggle tiltToggle;

    private GameControllManager gameControlManager;

    void Start()
    {
        gameControlManager = GetComponent<GameControllManager>();
        // Add listeners for when toggles are clicked
        steerToggle.onValueChanged.AddListener(OnSteerToggleChanged);
        tiltToggle.onValueChanged.AddListener(OnTiltToggleChanged);

        // Set default based on current manager state
        ControlType current = gameControlManager.GetControlType();
        steerToggle.isOn = current == ControlType.SteerWheel;
        tiltToggle.isOn = current == ControlType.Tilt;
    }
    public void RefreshUI()
{
    ControlType current = gameControlManager.GetControlType();
    steerToggle.isOn = (current == ControlType.SteerWheel);
    tiltToggle.isOn = (current == ControlType.Tilt);
}

    void OnSteerToggleChanged(bool isOn)
    {
        if (isOn)
            gameControlManager.SetControlType(ControlType.SteerWheel);
    }

    void OnTiltToggleChanged(bool isOn)
    {
        if (isOn)
            gameControlManager.SetControlType(ControlType.Tilt);
    }
}
