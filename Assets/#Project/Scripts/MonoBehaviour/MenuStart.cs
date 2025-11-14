using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{
   public void OnNextButton ()
    {
        SceneManager.LoadScene("Menu_Before_Fight1");
    }
       public void OnPlayButton ()
    {
        SceneManager.LoadScene("Fight1");
    }
    public void OnQuitButton ()
    {
        Application.Quit();
    }
}
