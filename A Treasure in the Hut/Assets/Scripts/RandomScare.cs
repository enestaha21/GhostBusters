using UnityEngine;
using System.Collections;

public class RandomScare : MonoBehaviour
{
    private AudioSource screamAudio;

    [Header("Rastgele Süre Ayarları (Saniye)")]
    public float minWait = 45f;
    public float maxWait = 90f;

    void Start()
    {
        screamAudio = GetComponent<AudioSource>();
        StartCoroutine(ScreamRoutine());
    }

    IEnumerator ScreamRoutine()
    {
        
        while (true)
        {
            
            float randomTime = Random.Range(minWait, maxWait);

            
            yield return new WaitForSeconds(randomTime);

            
            if (screamAudio != null && !screamAudio.isPlaying)
            {
                screamAudio.Play();
            }
        }
    }
}