using UnityEngine;

public class SpeedingAnimiation : MonoBehaviour
{
    [SerializeField]private Animator SpeedingAnimator;

    public void TriggerSpeedingAnimation(){
        SpeedingAnimator.SetBool("IsSpeeding", true);
    }
    public void StopSpeedingAnimation(){
        SpeedingAnimator.SetBool("IsSpeeding", false);
    }
    public void EndAnimation(){
        Debug.Log("Animation Triggered for End");
        SpeedingAnimator.SetBool("IsGameEnded", true);
    }
}
