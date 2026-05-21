using UnityEngine.InputSystem;

public class DashTutorialTask : InputTutorialTask
{
    public override string Title => "ダッシュ";
    public override string Description => "Shiftキーでダッシュしよう";

    private bool dashed;
    private InputAction dashAction;

    public override void OnTaskSet()
    {
        base.OnTaskSet();
        dashed = false;

        dashAction = GetAction("Dash");
        if (dashAction != null)
            dashAction.performed += OnDash;
    }

    private void OnDash(InputAction.CallbackContext ctx) => dashed = true;

    public void OnTaskEnd()
    {
        if (dashAction != null)
            dashAction.performed -= OnDash;
    }

    public override bool IsCompleted() => dashed;
}