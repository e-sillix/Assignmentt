using UnityEngine;

public class FinishDetector : MonoBehaviour
{
    public LayerMask PlayerLayer;
    [SerializeField] InGameStateManager inGameStateManager;
    void OnTriggerEnter(Collider collision)
    {
        if (((1 << collision.gameObject.layer) & PlayerLayer) != 0)
        {
            Debug.Log("Player reached Finish Line!");
            inGameStateManager.TriggerGameCleared();
        }
    }
}
