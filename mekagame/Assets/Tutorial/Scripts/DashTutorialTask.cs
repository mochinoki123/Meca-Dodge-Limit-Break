using UnityEngine.InputSystem;

public class DashTutorialTask : InputTutorialTask
{
    public override string Title => "ダッシュ";
    public override string Description => "□でダッシュしよう";

    private bool dashed;
    private InputAction dashAction;

    public DashTutorialTask(PlayerInput playerInput) : base(playerInput) { }

    public override void OnTaskSet()
    {
        base.OnTaskSet();
        dashed = false;
        dashAction = GetAction("Sprint");
        if (dashAction != null)
            dashAction.performed += OnDash;
    }

    public override void Tick() { }

    private void OnDash(InputAction.CallbackContext ctx) => dashed = true;

    public override void OnTaskEnd()
    {
        if (dashAction != null)
            dashAction.performed -= OnDash;
    }

    public override bool IsCompleted() => dashed;

    public string GetProgress() => "";
}