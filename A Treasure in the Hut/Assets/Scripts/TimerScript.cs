using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float timeRemaining = 120f;

    [Header("Panels (Just for Texts)")]
    public GameObject winPanel;   
    public GameObject losePanel;  
    public GameObject jumpscareImage;

    [Header("Scene Names to Load")]
    public string winSceneName = "WinMenuScene";   
    public string loseSceneName = "LoseMenuScene"; 

    [Header("Cinematic Settings")]
    public GameObject playerObj;
    public GameObject cinematicGroup;
    public Animator cinematicAnimator;
    public string animationTriggerName = "PlayWin";
    public float winUiDelay = 5f;       
    public float timeBeforeSceneLoad = 5f; 

    public float jumpscareDuration = 2f;

    private bool isTimerRunning = true;

    void Update()
    {
        if (timerText == null || !isTimerRunning) return;

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;

            int minutes = Mathf.FloorToInt(timeRemaining / 60f);
            int seconds = Mathf.FloorToInt(timeRemaining % 60f);

            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            timeRemaining = 0f;
            timerText.text = "00:00";
            isTimerRunning = false;
            TriggerLoss();
        }
    }

    public void WinGame()
    {
        if (!isTimerRunning) return;
        isTimerRunning = false;

        if (playerObj != null) playerObj.SetActive(false);

        if (cinematicGroup != null)
        {
            cinematicGroup.SetActive(true);
            Camera childCam = cinematicGroup.GetComponentInChildren<Camera>(true);
            if (childCam != null) childCam.gameObject.SetActive(true);
        }

        StartCoroutine(PlayCinematicRoutine());
        Invoke("ShowWinPanel", winUiDelay);
    }

    private IEnumerator PlayCinematicRoutine()
    {
        if (cinematicAnimator != null)
        {
            Rigidbody carRb = cinematicAnimator.GetComponent<Rigidbody>();
            if (carRb != null)
            {
                carRb.isKinematic = true;
                carRb.useGravity = false;
                carRb.linearVelocity = Vector3.zero;
                carRb.angularVelocity = Vector3.zero;
            }

            Collider[] allColliders = cinematicAnimator.GetComponentsInChildren<Collider>();
            foreach (Collider col in allColliders) col.enabled = false;

            cinematicAnimator.enabled = false;
            yield return new WaitForSeconds(0.1f);
            cinematicAnimator.enabled = true;
            cinematicAnimator.SetTrigger(animationTriggerName);
        }
    }

    private void ShowWinPanel()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);
            
            Invoke("LoadWinScene", timeBeforeSceneLoad);
        }
    }

    private void TriggerLoss()
    {
        if (jumpscareImage != null) jumpscareImage.SetActive(true);
        Invoke("ShowLosePanel", jumpscareDuration);
    }

    private void ShowLosePanel()
    {
        if (jumpscareImage != null) jumpscareImage.SetActive(false);

        if (losePanel != null)
        {
            losePanel.SetActive(true);
            
            Invoke("LoadLoseScene", timeBeforeSceneLoad);
        }
    }

    private void LoadWinScene()
    {
        SceneManager.LoadScene(winSceneName);
    }

    private void LoadLoseScene()
    {
        SceneManager.LoadScene(loseSceneName);
    }
}