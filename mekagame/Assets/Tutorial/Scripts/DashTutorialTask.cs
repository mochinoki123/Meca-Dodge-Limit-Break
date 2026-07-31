//ダッシュのチュートリアル用
using UnityEngine.InputSystem;

public class DashTutorialTask : InputTutorialTask
{
    public override string Title => "ダッシュ";
    public override string Description => "□でダッシュしよう";

    private bool dashed;
    private InputAction dashAction;

    public DashTutorialTask(PlayerInput playerInput) : base(playerInput) { }

　　　//初期化処理
    public override void OnTaskSet()
    {
        base.OnTaskSet();
        dashed = false;
        dashAction = GetAction("Sprint");//ダッシュ用のインプットアクション取得
        if (dashAction != null)
            dashAction.performed += OnDash;//ダッシュ入力時にOnDashを呼ぶイベント登録
    }

    public override void Tick() { }

    //CallbackContextは入力の詳細情報構造体
    private void OnDash(InputAction.CallbackContext ctx) => dashed = true;

    public override void OnTaskEnd()
    {
        if (dashAction != null)
            dashAction.performed -= OnDash;//イベント解除
    }

    public override bool IsCompleted() => dashed;

    public override string GetProgress() => "";
}