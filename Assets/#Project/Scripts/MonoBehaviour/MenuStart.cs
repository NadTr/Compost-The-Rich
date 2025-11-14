using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{
   public void OnPlayButton ()
    {
        SceneManager.LoadScene("Fight1");
    }
    public void OnQuitButton ()
    {
        Application.Quit();
    }
}
