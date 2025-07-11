using UnityEngine;

public class inGameTransform : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    if (player != null)
    {
        Debug.Log("Found Player: " + player.name);
    }
    }
    public void GlideIsClicked()
    {
        player.GetComponent<TransfromCar>().TriggerGliding();
    }
}
