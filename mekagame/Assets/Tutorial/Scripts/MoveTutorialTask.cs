using UnityEngine;
using UnityEngine.InputSystem;

public class MoveTutorialTask : InputTutorialTask
{
    public override string Title => "移動";
    public override string Description => "左スティックで移動しよう";

    private float totalMoveDistance;
    private const float RequiredDistance = 5f;

    public MoveTutorialTask(PlayerInput playerInput) : base(playerInput) { }

    public override void OnTaskSet()
    {
        base.OnTaskSet();
        totalMoveDistance = 0f;
    }

    public override void OnTaskEnd() { }

    public override void Tick()
    {
        var move = GetAction("Move");
        if (move == null) return;
        totalMoveDistance += move.ReadValue<Vector2>().magnitude * Time.deltaTime;
    }

    public override bool IsCompleted() => totalMoveDistance >= RequiredDistance;
}