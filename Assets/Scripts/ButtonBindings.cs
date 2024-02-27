using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBindings : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}