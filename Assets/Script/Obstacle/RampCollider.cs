using UnityEngine;

public class RampCollider : MonoBehaviour
{
    public LayerMask PlayerLayer;
    private CameraSwitchingManager cameraSwitchingManager;

    void Start()
    {
        cameraSwitchingManager = FindAnyObjectByType<CameraSwitchingManager>();        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & PlayerLayer) != 0)
        {
            Debug.Log("Player hit the ramp!");
            cameraSwitchingManager.SwitchToStuntCamera();
        }
    }
}
