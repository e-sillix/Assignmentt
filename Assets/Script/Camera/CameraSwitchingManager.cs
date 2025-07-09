using UnityEngine;
using System.Collections;
using Unity.Cinemachine;


public class CameraSwitchingManager : MonoBehaviour
{
    public CinemachineCamera mainCam;
    public CinemachineCamera stuntCam;
    [SerializeField] private GroundColliider groundColliider;
    private bool isPlayerOnStunt = false;

    public void SwitchToStuntCamera()
    {
        stuntCam.Priority = 30;
        mainCam.Priority = 10;
        isPlayerOnStunt = true;
        // groundColliider.isPlayerOnStunt = true;
    }

    public void SwitchToMainCamera()
    {
        isPlayerOnStunt = false;
        mainCam.Priority = 30;
        stuntCam.Priority = 10;
        // groundColliider.isPlayerOnStunt = false;
    }
}
