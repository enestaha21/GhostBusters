using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float timeRemaining = 300f;

    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject jumpscareImage;

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
        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }
    }

    private void TriggerLoss()
    {
        if (jumpscareImage != null)
        {
            jumpscareImage.SetActive(true);
        }

        Invoke("ShowLosePanel", 2f);
    }

    private void ShowLosePanel()
    {
        if (jumpscareImage != null)
        {
            jumpscareImage.SetActive(false);
        }

        if (losePanel != null)
        {
            losePanel.SetActive(true);
        }
    }
}