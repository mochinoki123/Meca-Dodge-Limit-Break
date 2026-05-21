using UnityEngine.InputSystem;

public class LimitBreakTutorialTask : InputTutorialTask
{
    public override string Title => "リミットブレイク";
    public override string Description => "リミットブレイクを3回成功させよう";

    private int limitBreakCount;
    private const int RequiredCount = 3;

    private bool wasActive;
    private LimitBreak limitBreak;

    public LimitBreakTutorialTask(PlayerInput playerInput, LimitBreak limitBreak)
        : base(playerInput)
    {
        this.limitBreak = limitBreak;
    }

    public override void OnTaskSet()
    {
        base.OnTaskSet();
        wasActive = false;
        limitBreakCount = 0; 
    }

    public override void OnTaskEnd() { }

    public override void Tick()
    {
        if (limitBreak == null) return;

        bool isActive = limitBreak.isLB;

        if (wasActive && !isActive)
            limitBreakCount++;

        wasActive = isActive;
    }

    public override bool IsCompleted() => limitBreakCount >= RequiredCount;

    public string GetProgress() => $"{limitBreakCount}/{RequiredCount}";
}