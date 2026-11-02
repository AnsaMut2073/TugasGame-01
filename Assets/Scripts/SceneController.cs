using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void PindahMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void PindahMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
