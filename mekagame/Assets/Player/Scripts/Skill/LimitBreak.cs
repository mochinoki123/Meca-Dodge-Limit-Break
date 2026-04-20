using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;


public class LimitBreak : MonoBehaviour
{
    // 消費ゲージ量
    [SerializeField] private int lBUseGage;
    // 攻撃ダメージ
    [SerializeField] public int lBDamage;
    // LB時のパリィ受付時間
    [SerializeField] public float lBTime;
    // LB失敗時の硬直時間
    [SerializeField] public float lBCoolTime;

    public bool isLB = false;
    PlayerParry parry;

    private void Awake()
    {
        // コンポーネント取得
        parry = GetComponent<PlayerParry>();
    }

    private void OnLimitBreak(InputValue value)
    {
        // 既に使用中なら中断
        if (isLB) return;
        StartCoroutine(AttackLimitBreak());
    }

    private IEnumerator AttackLimitBreak()
    {
        // ゲージ残量チェック
        if (GameManager.Instance.NowGage >= lBUseGage)
        {
            isLB = true;
            // ゲージ消費
            GameManager.Instance.UseGage(lBUseGage);

            // パリィ処理を実行し完了を待機
            yield return StartCoroutine(parry.Parry());

            isLB = false;
        }
    }
}