using UnityEngine;
using TMPro;

public class ObjectiveManager : MonoBehaviour
{
    public TextMeshProUGUI objectiveText;

    void Start()
    {
        UpdateObjective("Objective: Search the area for a way out.");
    }

    public void UpdateObjective(string newObjective)
    {
        if (objectiveText != null)
        {
            objectiveText.text = newObjective;
        }
    }
}