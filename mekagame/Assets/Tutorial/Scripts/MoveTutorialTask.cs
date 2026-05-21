// 移動タスク：WASDを一定量入力したら達成
using UnityEngine;

public class MoveTutorialTask : InputTutorialTask
{
    public override string Title => "移動";
    public override string Description => "WASDキーで移動しよう";

    private float totalMoveDistance;
    private const float RequiredDistance = 5f;

    public override void OnTaskSet()
    {
        base.OnTaskSet();
        totalMoveDistance = 0f;
    }

    public override void Tick()
    {
        var move = GetAction("Move");
        if (move == null) return;
        totalMoveDistance += move.ReadValue<Vector2>().magnitude * Time.deltaTime;
    }

    public override bool IsCompleted() => totalMoveDistance >= RequiredDistance;
}