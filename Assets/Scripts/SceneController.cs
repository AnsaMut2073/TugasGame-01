using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void PindahMainScene()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
        SceneManager.LoadScene("MainScene");
        if (Cursor.visible == true)
        {
            Cursor.visible = false;
        }
    }

    public void PindahMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        if (Cursor.visible == false)
        {
            Cursor.visible = true;
        }
    }
}
