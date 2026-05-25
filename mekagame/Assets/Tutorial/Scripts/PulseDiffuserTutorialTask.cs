using UnityEngine.InputSystem;

public class PulseDiffuserTutorialTask : InputTutorialTask
{
    public override string Title => "パルスディフューザー";
    public override string Description => "パルスディフューザーを1回発動しよう";

    private bool wasActive;        // 前フレームのisPD
    private bool hasActivated;     // 発動を検知したか

    private PlayerPulseDiffuser pulseDiffuser;

    public PulseDiffuserTutorialTask(PlayerInput playerInput, PlayerPulseDiffuser pulseDiffuser)
        : base(playerInput)
    {
        this.pulseDiffuser = pulseDiffuser;
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
        if (pulseDiffuser == null) return;

        // isPDがfalse→trueに変わった瞬間を発動として検知
        bool isActive = pulseDiffuser.isPD;
        if (!wasActive && isActive)
            hasActivated = true;

        wasActive = isActive;
    }

    public override bool IsCompleted() => hasActivated;

    public string GetProgress() => "";

}