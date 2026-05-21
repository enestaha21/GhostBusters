using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    
    public GameObject flashlight;

    void Start()
    {
        
        if (flashlight != null)
            flashlight.SetActive(false);
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (flashlight != null)
            {
           
                flashlight.SetActive(!flashlight.activeSelf);
            }
        }
    }
}