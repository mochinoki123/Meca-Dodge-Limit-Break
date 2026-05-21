public interface ITutorialTask
{
    string Title { get; }
    string Description { get; }
    float TransitionTime { get; }
    void OnTaskSet();
    void OnTaskEnd();  
    bool IsCompleted();
}