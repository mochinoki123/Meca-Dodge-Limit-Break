public class GrazeTutorialTask : InputTutorialTask
{
    public override string Title => "グレイズ";
    public override string Description => "弾をギリギリかすめよう";

    public GrazeTutorialTask(PlayerInput playerInput) : base(playerInput) { }

    public override void Tick() { /* グレイズ判定 */ }
    public override bool IsCompleted() => totalMoveDistance >= RequiredDistance;
}