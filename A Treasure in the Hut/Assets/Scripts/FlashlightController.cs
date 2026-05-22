using UnityEngine;
using TMPro;

public class FlashlightController : MonoBehaviour
{
    public GameObject flashlight;
    public TextMeshProUGUI infoText;

    void Start()
    {
        if (flashlight != null)
        {
            flashlight.SetActive(false);
        }

        if (infoText != null)
        {
            infoText.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (flashlight != null)
            {
                flashlight.SetActive(!flashlight.activeSelf);
            }

            if (infoText != null)
            {
                infoText.gameObject.SetActive(false);
            }
        }
    }
}