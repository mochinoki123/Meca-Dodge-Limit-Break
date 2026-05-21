public class PulseDiffuserTutorialTask : InputTutorialTask
{
    public override string Title => "パルスディフューザー";
    public override string Description => "無敵";

    public PulseDiffuserTutorialTask(PlayerInput playerInput) : base(playerInput) { }

    public override void Tick() { /*パルスディフューザー判定 */ }
    public override bool IsCompleted() => /* 達成判定 */;
}