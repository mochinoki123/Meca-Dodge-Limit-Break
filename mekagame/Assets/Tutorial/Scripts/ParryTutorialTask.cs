using UnityEngine.InputSystem;

public class ParryTutorialTask : InputTutorialTask
{
    public override string Title => "パリィ";
    public override string Description => "パリィを1回成功させよう";

    private int parryCount;
    private const int RequiredCount = 1;

    public ParryTutorialTask(PlayerInput playerInput) : base(playerInput) { }

    public override void OnTaskSet()
    {
        base.OnTaskSet();
        parryCount = 0;
    }

    public override void OnTaskEnd() { }

    public override void Tick()
    {
        if (!ObjectParry.ParrySuccess) return;
        ObjectParry.ResetParry();
        parryCount++;
    }

    public override bool IsCompleted() => parryCount >= RequiredCount;

    public string GetProgress() => $"{parryCount}/{RequiredCount}";
}