using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    public float interactRange = 3f;
    public Transform holdPoint;

    private GameObject heldObj;

    void Update()
    {
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
        Debug.Log("Key picked up!");
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
        Debug.Log("Key dropped!");
    }
}