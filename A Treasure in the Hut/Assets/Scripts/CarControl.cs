using UnityEngine;

public class CarControl : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Fuel")
        {
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