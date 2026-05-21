using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    private Light myLight;

    [Header("Flicker Ayarlari")]
    public float minIntensity = 0.2f;
    public float maxIntensity = 1.8f;

    void Start()
    {
        myLight = GetComponent<Light>();
    }

    void Update()
    {
        // Her karede ışığın şiddetini rastgele değiştirerek titreme efekti yaratır
        myLight.intensity = Random.Range(minIntensity, maxIntensity);
    }
}
