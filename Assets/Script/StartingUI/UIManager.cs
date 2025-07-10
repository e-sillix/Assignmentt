using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
   public void OnClickPlay(){
        SceneManager.LoadScene("Level");
   }
   public void OnClickExit(){
        Application.Quit();
   }
}
