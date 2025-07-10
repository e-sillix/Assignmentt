using UnityEngine;

public class CarControl : MonoBehaviour
{
    private PrometeoCarController PrometeoCarController;
    public LayerMask Trigger;
    private TimeSlowing timeSlowing;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PrometeoCarController=GetComponent<PrometeoCarController>();
        PrometeoCarController.SetCinematicOn();
        timeSlowing=FindObjectOfType<TimeSlowing>();
    }

    void OnTriggerEnter(Collider other){
        if (((1 << other.gameObject.layer) & Trigger) != 0)
        {
            timeSlowing.StartSlow();
            Debug.Log("Trigger Entered");
        }
    }
   
}
