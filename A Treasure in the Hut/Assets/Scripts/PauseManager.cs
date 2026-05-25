using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [Header("Scene Settings")]
    public string mainMenuSceneName = "MainMenuScene";

    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToMainMenu();
        }
    }

    public void ReturnToMainMenu()
    {
       
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("ESC'ye basıldı, Ana Menüye dönülüyor...");
        SceneManager.LoadScene(mainMenuSceneName);
    }
}