using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    public float interactRange = 3f;
    public Transform holdPoint;

    [Header("UI Settings")]
    public TextMeshProUGUI interactionText;

    [Header("Audio Settings")] // <-- YENİ EKLENEN SES KISMI
    public AudioSource interactionAudioSource;
    public AudioClip pickupSound;

    private GameObject heldObj;

    void Start()
    {
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        CheckUiHover();

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObj == null)
            {
                LookForInteraction();
            }
            else
            {
                if (!LookForInteraction())
                {
                    DropObject();
                }
            }
        }
    }

    void CheckUiHover()
    {
        if (interactionText == null) return;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactRange))
        {
            if (hit.transform.CompareTag("CanPickUp") && heldObj == null)
            {
                interactionText.gameObject.SetActive(true);
                return;
            }

            DoorScript door = hit.transform.GetComponentInParent<DoorScript>();
            if (door != null)
            {
                interactionText.gameObject.SetActive(true);
                return;
            }
        }
        interactionText.gameObject.SetActive(false);
    }

    bool LookForInteraction()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactRange))
        {
            if (hit.transform.CompareTag("CanPickUp") && heldObj == null)
            {
                PickUpObject(hit.transform.gameObject);
                return true;
            }

            DoorScript door = hit.transform.GetComponentInParent<DoorScript>();
            if (door != null)
            {
                door.InteractWithDoor(heldObj);
                return true;
            }
        }
        return false;
    }

    void PickUpObject(GameObject obj)
    {
        heldObj = obj;
        heldObj.GetComponent<Rigidbody>().isKinematic = true;
        heldObj.transform.position = holdPoint.position;
        heldObj.transform.parent = holdPoint;
        heldObj.transform.localRotation = Quaternion.identity;

        // <-- EŞYAYI YERDEN ALINCA SESİ ÇAL -->
        if (interactionAudioSource != null && pickupSound != null)
        {
            interactionAudioSource.PlayOneShot(pickupSound);
        }

        ObjectiveManager objectiveManager = FindObjectOfType<ObjectiveManager>();

        if (heldObj.name == "Fuel")
        {
            if (objectiveManager != null)
            {
                objectiveManager.UpdateObjective("Take the fuel to the car and escape!");
            }
        }
        else if (heldObj.name == "Key" || heldObj.name.Contains("Key"))
        {
            if (objectiveManager != null)
            {
                objectiveManager.UpdateObjective("Use the key to open the locked door");
            }
        }

        Debug.Log("Object picked up!");
    }

    public void DestroyHeldKey()
    {
        if (heldObj != null)
        {
            Destroy(heldObj);
            heldObj = null;
        }
    }

    void DropObject()
    {
        heldObj.GetComponent<Rigidbody>().isKinematic = false;
        heldObj.transform.parent = null;
        heldObj = null;
        Debug.Log("Object dropped!");
    }
}