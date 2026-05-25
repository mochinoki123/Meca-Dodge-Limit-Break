using UnityEngine.InputSystem;

public class OverClockTutorialTask : InputTutorialTask 
{
    public override string Title => "オーバークロック";
    public override string Description => "オーバークロックを1回発動しよう";

    private bool wasActive;
    private bool hasActivated;

    private OverClock overClock;

    public OverClockTutorialTask(PlayerInput playerInput, OverClock overClock)
        : base(playerInput)
    {
        this.overClock = overClock;
    }

    public override void OnTaskSet()
    {
        base.OnTaskSet();
        wasActive = false;
        hasActivated = false;
    }

    public override void OnTaskEnd() { }

    public override void Tick()
    {
        if (overClock == null) return;

        bool isActive = overClock.isOC;
        if (!wasActive && isActive)
            hasActivated = true;

        wasActive = isActive;
    }

    public override bool IsCompleted() => hasActivated;

    public string GetProgress() => "";
}