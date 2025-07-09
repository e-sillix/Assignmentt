using UnityEngine;

public class SteeringWheelController : MonoBehaviour
{
    [SerializeField] private RectTransform wheelRect;

    private Vector2 wheelCenter;
    private int activeFingerId = -1;
    private float previousAngle;
    private float currentRotation = 0f;
    private bool isTouching = false;

    public float maxVisualRotation = 180f; // UI visual limit left/right
    public float steeringAxis = 0f; // -1 to +1

    private PrometeoCarController prometeoCarController;


    void Start()
    {
        prometeoCarController = GetComponent<PrometeoCarController>();
    }
   void Update()
    {
        // 1. Detect new touch starting on the wheel
        if (!isTouching && Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch t = Input.GetTouch(i);
                if (t.phase == TouchPhase.Began)
                {
                    wheelCenter = RectTransformUtility.WorldToScreenPoint(null, wheelRect.position);
                    if (RectTransformUtility.RectangleContainsScreenPoint(wheelRect, t.position))
                    {
                        activeFingerId = t.fingerId;
                        previousAngle = GetAngle(t.position);
                        isTouching = true;
                        prometeoCarController.SetIsSteeringTouching(true);
                        break; // only track first touch on wheel
                    }
                }
            }
        }

        // 2. Update only the active finger
        if (isTouching)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch t = Input.GetTouch(i);
                if (t.fingerId != activeFingerId) continue;

                if (t.phase == TouchPhase.Moved)
                {
                    float currentAngle = GetAngle(t.position);
                    float delta = Mathf.DeltaAngle(previousAngle, currentAngle);

                    currentRotation += delta;
                    currentRotation = Mathf.Clamp(currentRotation, -maxVisualRotation, maxVisualRotation);

                    wheelRect.rotation = Quaternion.Euler(0, 0, currentRotation);
                    previousAngle = currentAngle;

                    prometeoCarController.TurnTheCar(steeringAxis);
                }
                else if (t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled)
                {
                    isTouching = false;
                    activeFingerId = -1;
                    prometeoCarController.SetIsSteeringTouching(false);
                }
                break;
            }
        }

        // 3. Reset visual if no touch
        if (!isTouching && Mathf.Abs(currentRotation) > 0.1f)
        {
            ResetWheelVisual();
        }

        // 4. Normalize rotation to -1 to 1
        steeringAxis = Mathf.Clamp(currentRotation / maxVisualRotation, -1f, 1f);
    }



    float GetAngle(Vector2 pos)
    {
        Vector2 dir = pos - wheelCenter;
        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }
  public void ResetWheelVisual()
{
    // Smoothly move currentRotation toward 0
    currentRotation = Mathf.Lerp(currentRotation, 0f, Time.deltaTime * 10f);

    // Apply the rotation to the wheel UI
    wheelRect.rotation = Quaternion.Euler(0, 0, currentRotation);

    // Update axis value
    steeringAxis = Mathf.Clamp(currentRotation / maxVisualRotation, -1f, 1f);
}
}
