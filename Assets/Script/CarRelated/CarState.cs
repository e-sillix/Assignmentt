using UnityEngine;
using TMPro;

public class CarState : MonoBehaviour
{
    [SerializeField] private float minimumHeightForFall =-100f;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private int speedingAnimationThreshold;
    private bool isSpeedingAnimationTriggered=false;
    private bool isFallen = false;
    // private float speedOfTheCar=0f;
    private Rigidbody carRigidbody;
    private SpeedingAnimiation speedingAnimation;

    void Start(){
        carRigidbody=GetComponent<Rigidbody>();
        speedingAnimation=GetComponent<SpeedingAnimiation>();
    }
    void Update()
    {
        if (!isFallen)
        {
            if (transform.position.y < minimumHeightForFall)
            {
                Debug.Log("Car fallen trigger Game Over");
                FindAnyObjectByType<InGameStateManager>().TriggerGameOver();
                isFallen = true;
            }
        }
      
        float speed = carRigidbody.linearVelocity.magnitude; // Speed in meters per second
        int speedKmph = Mathf.RoundToInt(speed * 3.6f); // ✅ rounded to whole number
        
            // FindAnyObjectByType<SpeedingAnimiation>().TriggerSpeedingAnimation();

        speedText.text = speedKmph + " Km/H"; // ✅ display as int
        // if(!speedingAnimation){
        //     return;
        // }
        if(speedKmph > speedingAnimationThreshold&& !isSpeedingAnimationTriggered){
            speedingAnimation.TriggerSpeedingAnimation();
            isSpeedingAnimationTriggered=true;
        }
        if(speedKmph < speedingAnimationThreshold&& isSpeedingAnimationTriggered){
            speedingAnimation.StopSpeedingAnimation();
            isSpeedingAnimationTriggered=false;
        }
    
    }
}
