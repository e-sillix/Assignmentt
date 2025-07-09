using UnityEngine;

public class FlameCollisionDetection : MonoBehaviour
{

    public LayerMask PlayerLayer;
    
     private InGameStateManager inGameStateManager;
    void Start()
    {
        inGameStateManager = FindAnyObjectByType<InGameStateManager>();   
    }
    void OnTriggerEnter(Collider collision)
    {
        if (((1 << collision.gameObject.layer) & PlayerLayer) != 0)
        {
            Debug.Log("Player Touched the flame thrower!");
            collision.gameObject.GetComponentInParent<PrometeoCarController>().CarBlast();
            inGameStateManager.TriggerGameOver();          
        }
    }
    
}
