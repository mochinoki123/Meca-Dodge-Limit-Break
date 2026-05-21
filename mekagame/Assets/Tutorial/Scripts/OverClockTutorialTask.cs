public class OveerClockTutorialTask : InputTutorialTask
{
    public override string Title => "オーバークロック";
    public override string Description => "自強化";

    public OveerClockTutorialTask(PlayerInput playerInput) : base(playerInput) { }

    public override void Tick() { /* オーバークロック判定 */ }
    public override bool IsCompleted() => /* 達成判定 */;
}