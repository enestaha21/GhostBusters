using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Animator animator; 
    public Transform player;  
    public float distanceToOpen = 3f; 

    void Update()
    {
       
        float dist = Vector3.Distance(player.position, transform.position);

        
        if (dist < distanceToOpen && Input.GetKeyDown(KeyCode.E))
        {
            bool currentState = animator.GetBool("isOpen");
            animator.SetBool("isOpen", !currentState);
        }
    }
}