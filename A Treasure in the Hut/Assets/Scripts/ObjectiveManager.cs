using UnityEngine;
using TMPro;

public class ObjectiveManager : MonoBehaviour
{
    public TextMeshProUGUI objectiveText;

    void Start()
    {
        UpdateObjective("Escape from forest");
    }

    public void UpdateObjective(string newObjective)
    {
        if (objectiveText != null)
        {
            objectiveText.text = newObjective;
        }
    }
}