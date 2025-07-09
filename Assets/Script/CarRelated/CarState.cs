using UnityEngine;

public class CarState : MonoBehaviour
{
    [SerializeField] private float minimumHeightForFall =-100f;
    private bool isFallen = false;
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
    }
}
