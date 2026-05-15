using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class LimitBreak : MonoBehaviour
{
    // ゲージ消費量
    [SerializeField] private int lBUseGage;
    // 攻撃ダメージ（PlayerParryから参照）
    [field: SerializeField] public int lBDamage { get; private set; }
    // LB時のパリィ受付時間（PlayerParryから参照）
    [field: SerializeField] public float lBTime { get; private set; }
    // LB失敗時の硬直時間（PlayerParryから参照）
    [field: SerializeField] public float lBCoolTime { get; private set; }

    // LB発動中フラグ（外部読み取り専用）
    public bool isLB { get; private set; } = false;

    private PlayerParry parry;

    private void Awake()
    {
        // コンポーネント取得
        parry = GetComponent<PlayerParry>();
    }

    private void OnLimitBreak(InputValue value)
    {
        // 既にLB発動中なら中断
        if (isLB) return;
        // パリィ中・硬直中・parryが未取得なら中断
        if (parry == null || parry.isParry || parry.notMove) return;
        // ゲージ不足なら中断
        if (GameManager.Instance.NowGage < lBUseGage) return;

        StartCoroutine(AttackLimitBreak());
    }

    private IEnumerator AttackLimitBreak()
    {
        // LB発動フラグを立ててゲージを消費
        isLB = true;
        GameManager.Instance.UseGage(lBUseGage);

        // パリィ処理を実行し完了まで待機
        // （成功時はLBAttackが内部で呼ばれ、isLBフラグをもとに挙動が変わる）
        yield return StartCoroutine(parry.ExecuteParry());

        // パリィ処理完了後にフラグを解除
        isLB = false;
    }
}