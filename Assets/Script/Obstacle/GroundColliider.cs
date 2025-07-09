using UnityEngine;

public class GroundColliider : MonoBehaviour
{
    public LayerMask PlayerLayer;
    private CameraSwitchingManager cameraSwitchingManager;
    public bool isPlayerOnStunt=false;

    void Start()
    {
        cameraSwitchingManager = FindAnyObjectByType<CameraSwitchingManager>();        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (isPlayerOnStunt)
        {
            if (((1 << other.gameObject.layer) & PlayerLayer) != 0)
            {
                Debug.Log("Player hit the ramp!");
                cameraSwitchingManager.SwitchToMainCamera();
            }
        }
    }
}
