using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [Header("Lock Settings")]
    public bool isLocked = false;

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
                Camera.main.GetComponent<PlayerInteraction>().DestroyHeldKey();
            }
            else
            {
                Debug.Log("The door is locked. You need to find a key.");
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
        isOpen = true;
        isLocked = false;
    }

    void CloseDoor()
    {
        if (anim != null)
        {
            anim.SetBool("isOpen", false);
        }
        isOpen = false;
        Debug.Log("Door is closing...");
    }
}