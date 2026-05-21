using System.Collections.Generic;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private List<TutorialStep> steps;

    private List<ITutorialTask> tasks;
    private int currentIndex = 0;

    private void Start()
    {
        // タスクリストを構築（PlayerInputは外部から渡す）
        var playerInput = FindFirstObjectByType<PlayerInput>();
        tasks = new List<ITutorialTask>
        {
            new MoveTutorialTask(playerInput),
            new DashTutorialTask(playerInput),
            new ParryTutorialTask(playerInput),
            // ...
        };

        tasks[currentIndex].OnTaskSet();
    }

    private void Update()
    {
        if (currentIndex >= tasks.Count) return;

        var current = tasks[currentIndex];
        current.Tick();

        if (current.IsCompleted())
            StartCoroutine(AdvanceTask(current));
    }

    private IEnumerator AdvanceTask(ITutorialTask task)
    {
        task.OnTaskEnd();
        yield return new WaitForSeconds(task.TransitionTime);

        currentIndex++;
        if (currentIndex < tasks.Count)
            tasks[currentIndex].OnTaskSet();
    }
}