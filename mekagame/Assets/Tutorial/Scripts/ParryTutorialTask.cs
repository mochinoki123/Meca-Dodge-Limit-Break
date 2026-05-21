public class ParryTutorialTask : InputTutorialTask
{
    public override string Title => "パリィ";
    public override string Description => "パリィを3回成功させよう";
    public override float TransitionTime => 2.0f;

    private int parryCount;
    private const int RequiredCount = 3;

    public override void OnTaskSet()
    {
        base.OnTaskSet();
        parryCount = 0;
    }

    public override void OnTaskEnd() { /* 購読なし */ }

    public override bool IsCompleted()
    {
        if (ParrySystem.Instance == null) return false;

        // フラグが立っていたら消費してカウント
        if (ParrySystem.Instance.ParrySuccess)
        {
            ParrySystem.Instance.ConsumeParrySuccess();
            parryCount++;
        }

        return parryCount >= RequiredCount;
    }
}