using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerParry : MonoBehaviour
{
    // パリィ判定オブジェクト
    [SerializeField] private GameObject playerParry;
    [SerializeField] private Enemy enemy;

    // 通常パリィの受付時間
    [SerializeField] private float parryTime = 0.5f;
    // 失敗時の硬直時間
    [SerializeField] private float parryCoolTime = 1.0f;
    // 成功テキストの表示時間
    [SerializeField] private float parrySuccessDisplayTime = 1.0f;
    // LB追撃エフェクトまでのディレイ
    [SerializeField] private float lbAttackDelay = 2.0f;
    // 失敗時の点滅間隔
    [SerializeField] private float blinkInterval = 0.1f;
    // 成功時SE
    [SerializeField] private AudioClip parrySound;
    // LB追撃エフェクト
    [SerializeField] private GameObject lBEffect;
    // 追撃対象の位置
    [SerializeField] private Transform enemyPos;

    // パリィ受付中フラグ（外部読み取り専用）
    public bool isParry { get; private set; } = false;
    // 硬直中フラグ（外部読み取り専用）
    public bool notMove { get; private set; } = false;

    // クールタイム中フラグ（内部管理）
    private bool isParryCoolTime = false;

    LimitBreak lb;
    PlayerPulseDiffuser pd;
    TextScript textScript;
    Animator animator;
    AudioSource audioSource;
    private Renderer rend;

    private void Awake()
    {
        // 各コンポーネント取得
        lb = GetComponent<LimitBreak>();
        pd = GetComponent<PlayerPulseDiffuser>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        rend = GetComponentInChildren<Renderer>();
        textScript = GetComponentInChildren<TextScript>();
    }

    private void OnParry(InputValue value)
    {
        // 各種状態チェック（実行不可なら中断）
        if (pd != null && pd.isPD) return;
        if (isParry) return;
        if (isParryCoolTime) return;
        if (notMove) return;

        StartCoroutine(Parry());
    }

    private IEnumerator Parry()
    {
        // LBモードかどうかを開始時点で確定
        bool isLBMode = lb != null && lb.isLB;

        // パリィ受付開始
        isParry = true;
        playerParry.SetActive(true);
        ObjectParry.ResetParry();

        // LBモードかどうかで受付時間を切り替え
        float currentDuration = isLBMode ? lb.lBTime : parryTime;
        float endTime = Time.time + currentDuration;

        // 成功 or タイムアップまで待機
        yield return new WaitUntil(() => ObjectParry.ParrySuccess || Time.time >= endTime);

        // パリィ受付終了
        playerParry.SetActive(false);
        isParry = false;

        // 成否で処理を分岐
        if (ObjectParry.ParrySuccess)
        {
            yield return StartCoroutine(HandleParrySuccess(isLBMode));
        }
        else
        {
            yield return StartCoroutine(HandleParryFailure(isLBMode));
        }

        // フラグをリセット
        ObjectParry.ResetParry();
    }

    // パリィ成功時の処理
    private IEnumerator HandleParrySuccess(bool isLBMode)
    {
        // 成功テキスト表示 & SE再生
        textScript?.Set(TextScript.EffectType.Parry);
        audioSource?.PlayOneShot(parrySound);

        yield return new WaitForSeconds(parrySuccessDisplayTime);

        // テキスト非表示
        textScript?.Removed(TextScript.EffectType.Parry);

        // LBモード時は追撃コルーチンを起動
        if (isLBMode)
        {
            StartCoroutine(LBAttack());
        }
    }

    // パリィ失敗時の処理（硬直 + 点滅）
    private IEnumerator HandleParryFailure(bool isLBMode)
    {
        notMove = true;
        isParryCoolTime = true;

        // LBモードかどうかで硬直時間を切り替え
        float currentCoolTime = isLBMode ? lb.lBCoolTime : parryCoolTime;

        // 硬直時間ぶん点滅
        yield return StartCoroutine(BlinkForDuration(currentCoolTime));

        // 状態復帰
        if (rend != null) rend.enabled = true;
        notMove = false;
        isParryCoolTime = false;
    }

    // 指定時間プレイヤーを点滅させる
    private IEnumerator BlinkForDuration(float duration)
    {
        float coolTimer = 0f;
        float blinkTimer = 0f;

        while (coolTimer < duration)
        {
            coolTimer += Time.deltaTime;
            blinkTimer += Time.deltaTime;

            // 一定間隔でRendererのON/OFFを切り替え
            if (blinkTimer >= blinkInterval)
            {
                if (rend != null) rend.enabled = !rend.enabled;
                blinkTimer = 0f;
            }

            yield return null;
        }
    }

    // LB追撃処理
    private IEnumerator LBAttack()
    {
        // 追撃アニメーション再生
        animator?.SetTrigger("LimitBreak");

        yield return new WaitForSeconds(lbAttackDelay);

        // エフェクト生成（一定時間後に自動破棄）
        if (lBEffect != null && enemyPos != null)
        {
            Destroy(Instantiate(lBEffect, enemyPos.position, Quaternion.identity), lbAttackDelay);
        }

        // ダメージ適用
        if (enemy != null && lb != null) enemy.Damage(lb.lBDamage);
    }

    /// <summary>外部から状態チェック込みでパリィを起動する（戻り値で成否確認可能）</summary>
    public bool TryParry()
    {
        if (pd != null && pd.isPD) return false;
        if (isParry) return false;
        if (isParryCoolTime) return false;
        if (notMove) return false;

        StartCoroutine(Parry());
        return true;
    }

    /// <summary>外部コルーチンからパリィ完了まで待機する場合に使用（LimitBreakから呼ばれる）</summary>
    public IEnumerator ExecuteParry()
    {
        if (pd != null && pd.isPD) yield break;
        if (isParry) yield break;
        if (isParryCoolTime) yield break;
        if (notMove) yield break;

        yield return StartCoroutine(Parry());
    }
}