using UnityEngine;
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
        ObjectParry.OnParrySuccesState += OnParrySuccesState;
        base.OnTaskSet();
        parryCount = 0;
    }

    public override void OnTaskEnd() 
    {
        ObjectParry.OnParrySuccesState -= OnParrySuccesState;
    }

    private void OnParrySuccesState(bool parrySuccess)
    {
        if (parrySuccess)
        {
            parryCount++;
        }
    }

    public override bool IsCompleted() => parryCount >= RequiredCount;

    public override string GetProgress() => $"{parryCount}/{RequiredCount}";
}