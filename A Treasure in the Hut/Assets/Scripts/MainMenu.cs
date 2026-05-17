using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene();
    }
    public void QuitGame()
    {
        Debug.Log("Oyun başarıyla kapatıldı."); 
        Application.Quit(); 
    }
}