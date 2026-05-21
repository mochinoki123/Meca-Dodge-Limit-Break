using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class TutorialManager : MonoBehaviour
{
    [System.Serializable]
    public class TutorialStep
    {
        public string id;
        public GameObject ui;
        public TextMeshProUGUI text;
        public int requiredCount = 1;
    }

    [SerializeField] private List<TutorialStep> steps;

    private Dictionary<string, int> achieveMap = new();

    public void UpdateText(string stepId, int achieve)
    {
        var step = steps.Find(s => s.id == stepId);
        if (step?.text == null) return;

        achieveMap[stepId] = achieve;
        step.text.text = $"{achieve}/{step.requiredCount}";
    }

    public void RefreshAllUI()
    {
        foreach (var step in steps)
        {
            if (step.text == null) continue;

            achieveMap.TryGetValue(step.id, out int count);
            step.text.text = $"{count}/{step.requiredCount}";
        }
    }
}