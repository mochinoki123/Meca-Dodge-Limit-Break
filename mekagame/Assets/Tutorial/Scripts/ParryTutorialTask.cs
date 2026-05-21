public class ParryTutorialTask : InputTutorialTask
{
    public override string Title => "パリィ";
    public override string Description => "パリィを3回成功させよう";

    private int parryCount;
    private const int RequiredCount = 3;

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
}