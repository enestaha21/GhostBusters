using UnityEngine;

public class CarControl : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioSource carAudioSource;
    public AudioClip carStartAndAwaySound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Fuel" || other.gameObject.name.Contains("Fuel"))
        {
            // <-- TAM ANİMASYON GİRDİĞİ AN ARABA SESİNİ PATLAT -->
            if (carAudioSource != null && carStartAndAwaySound != null)
            {
                carAudioSource.PlayOneShot(carStartAndAwaySound);
            }

            TimerScript timer = FindObjectOfType<TimerScript>();
            if (timer != null)
            {
                timer.WinGame();
            }

            ObjectiveManager objectiveManager = FindObjectOfType<ObjectiveManager>();
            if (objectiveManager != null)
            {
                objectiveManager.UpdateObjective("");
            }

            Destroy(other.gameObject);
        }
    }
}