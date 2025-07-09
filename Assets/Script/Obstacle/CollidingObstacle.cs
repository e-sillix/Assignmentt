using UnityEngine;

public class CollidingObstacle : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with:"+gameObject.name);
    }
}
