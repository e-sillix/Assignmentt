using UnityEngine;
using System.Collections;

public class CarStuntDetector : MonoBehaviour
{
    public LayerMask GroundLayer, RampLayer;
    private CameraSwitchingManager cameraSwitchingManager;
    private bool isPlayerOnStunt = false;
    private bool coroutineRunning = false;

    private void Start()
    {
        cameraSwitchingManager = FindAnyObjectByType<CameraSwitchingManager>();
    }

    void OnCollisionEnter(Collision other)
    {
        // When player hits the ramp
        if (((1 << other.gameObject.layer) & RampLayer) != 0)
        {
            if (!isPlayerOnStunt)
            {
                Debug.Log("Player Has Hit The Ramp.");
                cameraSwitchingManager.SwitchToStuntCamera();
                isPlayerOnStunt = true;

                // Start coroutine to monitor landing
                // if (!coroutineRunning)
                //     StartCoroutine(CheckForGroundLanding());
            }
        }
    //      if(isPlayerOnStunt){
    //     if (((1 << other.gameObject.layer) & GroundLayer) != 0)
    //     {
    //         isPlayerOnStunt=false;
    //         Debug.Log("Player has landed on the ground.1");
    //         cameraSwitchingManager.SwitchToMainCamera();
    //     }

    // }
    // Debug.Log("Still colliding with: " + other.gameObject.name);
    }
 
    void OnTriggerStay(Collider other){
        if((((1 << other.gameObject.layer) & GroundLayer) != 0)&&isPlayerOnStunt){
            Debug.Log("Player has landed .");
            cameraSwitchingManager.SwitchToMainCamera();
            isPlayerOnStunt=false;
        }
    }

    // IEnumerator CheckForGroundLanding()
    // {
    //     coroutineRunning = true;

    //     while (isPlayerOnStunt)
    //     {
    //         yield return new WaitForSeconds(0.5f);

    //         // Check if touching ground layer
    //         if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1.5f))
    //         {
    //             if (((1 << hit.collider.gameObject.layer) & GroundLayer) != 0)
    //             {
    //                 Debug.Log("Player has landed on the ground.");
    //                 cameraSwitchingManager.SwitchToMainCamera();
    //                 isPlayerOnStunt = false;
    //                 break;
    //             }
    //         }
    //     }

    //     coroutineRunning = false;
    // }
}
