using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement; 

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private GameObject player;

    [Header("UI")]
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private Text titleText;
    [SerializeField] private Text descriptionText;
    [SerializeField] private Text progressText;

    [Header("Scene")]
    [SerializeField] private string nextSceneName;

    private List<ITutorialTask> tasks;
    private int currentIndex = 0;
    private bool isAdvancing = false;

    private void Start()
    {
        if (player == null)
        {
            Debug.LogError("[TutorialManager] playerÇ™ñ¢ê›íËÇ≈Ç∑");
            return;
        }

        var pd = player.GetComponent<PlayerPulseDiffuser>();
        var oc = player.GetComponent<OverClock>();
        var lb = player.GetComponent<LimitBreak>();
        var graze = player.GetComponentInChildren<PlayerGraze>(); 

        tasks = new List<ITutorialTask>
        {
            new MoveTutorialTask(playerInput),
            new DashTutorialTask(playerInput),
            new GrazeTutorialTask(playerInput, graze),      
            new ParryTutorialTask(playerInput),
            new PulseDiffuserTutorialTask(playerInput, pd),
            new OverClockTutorialTask(playerInput, oc),
            new LimitBreakTutorialTask(playerInput, lb),
        };

        tasks[currentIndex].OnTaskSet();
        ShowTask(tasks[currentIndex]);
    }

    private void Update()
    {
        if (isAdvancing) return;
        if (currentIndex >= tasks.Count) return;

        var current = tasks[currentIndex];
        current.Tick();

        if (progressText != null)
            progressText.text = current.GetProgress();

        if (current.IsCompleted())
            StartCoroutine(AdvanceTask(current));
    }

    private IEnumerator AdvanceTask(ITutorialTask task)
    {
        isAdvancing = true;
        task.OnTaskEnd();

        if (progressText != null) progressText.text = "íBê¨ÅI";
        yield return new WaitForSeconds(task.TransitionTime);

        currentIndex++;

        if (currentIndex < tasks.Count)
        {
            tasks[currentIndex].OnTaskSet();
            ShowTask(tasks[currentIndex]);
            isAdvancing = false;
        }
        else
        {
            HideUI();
            yield return new WaitForSeconds(1.0f);
            SceneManager.LoadScene(nextSceneName);
        }
    }

    private void ShowTask(ITutorialTask task)
    {
        if (tutorialPanel != null) tutorialPanel.SetActive(true);
        if (titleText != null) titleText.text = task.Title;
        if (descriptionText != null) descriptionText.text = task.Description;
        if (progressText != null) progressText.text = task.GetProgress();
    }

    private void HideUI()
    {
        if (tutorialPanel != null) tutorialPanel.SetActive(false);
    }
}