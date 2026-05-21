using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Environment_Test");
    }

    public void QuitGame()
    {
        Debug.Log("Oyundan çıkıldı");
        Application.Quit();
    }
}