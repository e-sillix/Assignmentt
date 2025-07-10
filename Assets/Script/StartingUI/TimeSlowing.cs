using UnityEngine;
using System.Collections;

public class TimeSlowing : MonoBehaviour
{
    [SerializeField] private float slowDelay = 4f;
    [SerializeField] private float slowDecrementFactor = 0.1f;
    private bool isSlowing = false;
    private Coroutine slowCoroutine;

    public void CancelSlow()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f; // ← Default fixedDeltaTime
        if (slowCoroutine != null)
        {
            StopCoroutine(slowCoroutine);
            slowCoroutine = null;
        }
        isSlowing = false;
    }

    public void StartSlow()
    {
        isSlowing = true;
        if (slowCoroutine != null)
            StopCoroutine(slowCoroutine); // Avoid duplicates

        slowCoroutine = StartCoroutine(SlowingCoroutine());
    }

    IEnumerator SlowingCoroutine()
    {
        while (isSlowing)
        {
            yield return new WaitForSecondsRealtime(0.1f); // Use unscaled time
            Time.timeScale *= slowDecrementFactor;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;

            if (Time.timeScale <= 0.05f) // Avoid going to 0
            {
                Time.timeScale = 0.05f;
                Time.fixedDeltaTime = 0.02f * 0.05f;
                yield break;
            }
        }
    }
}
