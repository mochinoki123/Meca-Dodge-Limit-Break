using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using DG.Tweening;

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
    // LB攻撃アニメーションジャンプの時間
    [SerializeField] private float lbAnimationDuration;
    // LB攻撃アニメーションジャンプの高さ
    [SerializeField] private float jumpPosition;
    // 失敗時の点滅間隔
    [SerializeField] private float blinkInterval = 0.1f;
    // 成功時SE
    [SerializeField] private AudioClip parrySound;
    // LB追撃エフェクト (※プレハブを想定)
    [SerializeField] private GameObject lBEffect;

    // パリィ受付中フラグ（外部読み取り専用）
    public bool isParry { get; private set; } = false;
    // 硬直中フラグ（外部読み取り専用）
    public bool notMove { get; private set; } = false;

    // クールタイム中フラグ（内部管理）
    private bool isParryCoolTime = false;

    private LimitBreak lb;
    private PlayerPulseDiffuser pd;
    private TextScript textScript;
    private Animator animator;
    private AudioSource audioSource;
    private Renderer rend;
    private ObjectParry objectParryComponent;
    
    private void Awake()
    {
        // 各コンポーネント取得
        lb = GetComponent<LimitBreak>();
        pd = GetComponent<PlayerPulseDiffuser>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        rend = GetComponentInChildren<Renderer>();
        textScript = GetComponentInChildren<TextScript>();

        // パリィ判定オブジェクトからコンポーネントを取得
        if (playerParry != null)
        {
            objectParryComponent = playerParry.GetComponent<ObjectParry>();
        }
    }

    // パリィが実行可能か一括チェックするヘルパーメソッド
    private bool CanParry()
    {
        //if (pd != null && pd.isPD) return false;
        if (isParry) return false;
        if (isParryCoolTime) return false;
        if (notMove) return false;
        return true;
    }

    private void OnParry(InputValue value)
    {
        if (value.isPressed && CanParry())
        {
            StartCoroutine(Parry());
        }
    }

    private IEnumerator Parry()
    {
        // 割り込み防止のため、開始直後に即座にフラグを立てる
        isParry = true;

        animator?.SetTrigger("IsParry");

        // LBモードかどうかを開始時点で確定
        bool isLBMode = lb != null && lb.isLB;

        // パリィ受付開始
        playerParry.SetActive(true);
        objectParryComponent?.ResetParry(); // インスタンス経由に変更

        // LBモードかどうかで受付時間を切り替え
        float currentDuration = isLBMode ? lb.lBTime : parryTime;
        float endTime = Time.time + currentDuration;

        // 成功 or タイムアップまで待機
        yield return new WaitUntil(() => (objectParryComponent != null && objectParryComponent.ParrySuccess) || Time.time >= endTime);

        // パリィ受付終了
        playerParry.SetActive(false);
        isParry = false;

        // 成否をローカル変数にキャッシュ（リセット前に確定させるため）
        bool isSuccess = objectParryComponent != null && objectParryComponent.ParrySuccess;

        // 成否で処理を分岐
        if (isSuccess)
        {
            yield return StartCoroutine(HandleParrySuccess(isLBMode));
        }
        else
        {
            yield return StartCoroutine(HandleParryFailure(isLBMode));
        }

        // フラグをリセット
        objectParryComponent?.ResetParry();
    }

    // パリィ成功時の処理
    private IEnumerator HandleParrySuccess(bool isLBMode)
    {
        if (isLBMode)
        {
            textScript?.Set(TextScript.EffectType.LimitBreak);
            StartCoroutine(LBAttack());

            yield return new WaitForSeconds(parrySuccessDisplayTime);
        }
        else
        {
            textScript?.Set(TextScript.EffectType.Parry);
            audioSource?.PlayOneShot(parrySound);

            yield return new WaitForSeconds(parrySuccessDisplayTime);
        }

        textScript?.Removed(TextScript.EffectType.All);
    }

    // パリィ失敗時の処理（硬直 + 点滅）
    private IEnumerator HandleParryFailure(bool isLBMode)
    {
        notMove = true;
        isParryCoolTime = true;

        // LBモードかどうかで硬直時間を切り替え
        float currentCoolTime = isLBMode ? lb.lBCoolTime : parryCoolTime;

        if (isLBMode)
        {
            GameManager.Instance.AddGaugeStateBranch(GameManager.AddGaugeState.LBfailed);
        }

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
        //transform.DOMoveY(jumpPosition, lbAnimationDuration);

        yield return new WaitForSeconds(lbAttackDelay);

        // ダメージ適用
        if (enemy != null && lb != null) enemy.Damage(lb.lBDamage);
        
        // エフェクト生成（バグ防止のため、生成して自動破棄する安全な方式に変更）
        if (lBEffect != null)
        {
            lBEffect.SetActive(true);
            yield return new WaitForSeconds(1.0f);
            lBEffect.SetActive(false);
        }
    }

    /// <summary>外部から状態チェック込みでパリィを起動する（戻り値で成否確認可能）</summary>
    public bool TryParry()
    {
        if (!CanParry()) return false;

        StartCoroutine(Parry());
        return true;
    }

    /// <summary>外部コルーチンからパリィ完了まで待機する場合に使用（LimitBreakから呼ばれる）</summary>
    public IEnumerator ExecuteParry()
    {
        if (!CanParry()) yield break;

        yield return StartCoroutine(Parry());
    }
}