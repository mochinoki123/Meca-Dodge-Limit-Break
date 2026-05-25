using UnityEngine.InputSystem; 

public class GrazeTutorialTask : InputTutorialTask
{
    public override string Title => "グレイズ";
    public override string Description => "弾をギリギリかすめよう";

    private int grazeCount;
    private const int RequiredCount = 1;

    private PlayerGraze playerGraze;

    public GrazeTutorialTask(PlayerInput playerInput, PlayerGraze playerGraze)
        : base(playerInput)
    {
        this.playerGraze = playerGraze;
    }

    public override void OnTaskSet()
    {
        base.OnTaskSet();
        grazeCount = 0;
        playerGraze?.ResetGrazeCount();
    }

    public override void OnTaskEnd() { }

    public override void Tick()
    {
        if (playerGraze == null) return;

        grazeCount = playerGraze.GrazeCount;
    }

    public override bool IsCompleted() => grazeCount >= RequiredCount;

    public string GetProgress() => "";
}