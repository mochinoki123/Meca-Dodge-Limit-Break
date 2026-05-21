public class LimitBreakTutorialTask : InputTutorialTask
{
    public override string Title => "リミットブレイク";
    public override string Description => "カウンター攻撃";

    public LimitBreakTutorialTask(PlayerInput playerInput) : base(playerInput) { }

    public override void Tick() { /*リミットブレイク判定 */ }
    public override bool IsCompleted() => /* 達成判定 */;
}