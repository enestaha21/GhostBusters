using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [Header("Lock Settings")]
    public bool isLocked = false;

    [Header("Audio Settings")] // <-- YENİ EKLENEN SES KISMI
    public AudioSource doorAudioSource;
    public AudioClip lockedSound; // Kapı kilitliyken zorlama sesi
    public AudioClip openSound;   // Kapı açılma/kapanma gıcırtısı

    private Animator anim;
    private bool isOpen = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            anim = GetComponentInParent<Animator>();
        }
        if (anim == null)
        {
            anim = GetComponentInChildren<Animator>();
        }
    }

    public void InteractWithDoor(GameObject itemInHand)
    {
        if (isOpen)
        {
            CloseDoor();
            return;
        }

        if (isLocked)
        {
            if (itemInHand != null && itemInHand.name == "Key")
            {
                Debug.Log("Door is opening with key...");
                OpenDoor();

                ObjectiveManager objectiveManager = FindObjectOfType<ObjectiveManager>();
                if (objectiveManager != null)
                {
                    objectiveManager.UpdateObjective("Find the fuel inside the cabin");
                }

                Camera.main.GetComponent<PlayerInteraction>().DestroyHeldKey();
            }
            else
            {
                Debug.Log("The door is locked. You need to find a key");

                // <-- KAPI KİLİTLİYKEN ZORLAMA SESİ ÇAL -->
                if (doorAudioSource != null && lockedSound != null)
                {
                    doorAudioSource.PlayOneShot(lockedSound);
                }

                ObjectiveManager objectiveManager = FindObjectOfType<ObjectiveManager>();
                if (objectiveManager != null)
                {
                    objectiveManager.UpdateObjective("Find the cabin key");
                }
            }
        }
        else
        {
            Debug.Log("Door is unlocked. Door is opening...");
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        if (anim != null)
        {
            anim.SetBool("isOpen", true);
        }

        // <-- KAPI AÇILIRKEN GICIRTI SESİ ÇAL -->
        if (doorAudioSource != null && openSound != null)
        {
            doorAudioSource.PlayOneShot(openSound);
        }

        isOpen = true;
        isLocked = false;
    }

    void CloseDoor()
    {
        if (anim != null)
        {
            anim.SetBool("isOpen", false);
        }

        // <-- KAPI KAPANIRKEN DE GICIRTI SESİ ÇAL -->
        if (doorAudioSource != null && openSound != null)
        {
            doorAudioSource.PlayOneShot(openSound);
        }

        isOpen = false;
        Debug.Log("Door is closing...");
    }
}