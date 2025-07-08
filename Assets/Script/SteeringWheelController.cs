using UnityEngine;

public class SteeringWheelController : MonoBehaviour
{
    [SerializeField] private RectTransform wheelRect;

    private Vector2 wheelCenter;
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
    if (Input.touchCount > 0)
    {
        Touch touch = Input.GetTouch(0);
        Vector2 touchPos = touch.position;

        switch (touch.phase)
        {
            case TouchPhase.Began:
                wheelCenter = RectTransformUtility.WorldToScreenPoint(null, wheelRect.position);
                if (RectTransformUtility.RectangleContainsScreenPoint(wheelRect, touchPos))
                {
                    previousAngle = GetAngle(touchPos);
                    isTouching = true;
                    prometeoCarController.SetIsSteeringTouching(true);
                }
                break;

            case TouchPhase.Moved:
                if (isTouching)
                {
                    float currentAngle = GetAngle(touchPos);
                    float delta = Mathf.DeltaAngle(previousAngle, currentAngle);

                    currentRotation += delta;
                    currentRotation = Mathf.Clamp(currentRotation, -maxVisualRotation, maxVisualRotation);

                    wheelRect.rotation = Quaternion.Euler(0, 0, currentRotation);
                    previousAngle = currentAngle;
                    prometeoCarController.TurnTheCar(this.steeringAxis);
                }
                break;

            case TouchPhase.Ended:
            case TouchPhase.Canceled:
                isTouching = false;
                prometeoCarController.SetIsSteeringTouching(false);
                break;
        }
    }
    else if (!isTouching && Mathf.Abs(currentRotation) > 0.1f)
{
    ResetWheelVisual();
}

    // Normalize rotation value to -1 to 1
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
