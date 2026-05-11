using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerParry : MonoBehaviour
{
    [SerializeField] private GameObject playerParry;
    [SerializeField] private Enemy enemy;

    // パリィ受付時間
    [SerializeField] private float parryTime = 0.5f;
    // 失敗時の硬直時間
    [SerializeField] private float parryCoolTime = 1.0f;
    // 成功時SE
    [SerializeField] private AudioClip parrySound;
    [SerializeField] private GameObject lBEffect;
    [SerializeField] private Transform enemyPos;

    public bool isParry = false;
    private bool isParryCoolTime = false;
    public bool notMove = false;
    private float alpha_Sin;

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

        // パリィ実行
        StartCoroutine(Parry());
    }

    public IEnumerator Parry()
    {
        // LB状態確認
        bool isLBMode = (lb != null) && lb.isLB;

        // パリィ開始フラグ設定
        isParry = true;
        playerParry.SetActive(true);
        ObjectParry.parrySuccess = false;

        float currentDuration = isLBMode ? lb.lBTime : parryTime;

        // 受付時間ループ
        float endTime = Time.time + currentDuration;
        yield return new WaitUntil(() => ObjectParry.parrySuccess || Time.time >= endTime);

        // パリィ判定終了
        playerParry.SetActive(false);
        isParry = false;

        // 結果分岐
        if (ObjectParry.parrySuccess)
        {
            // --- 成功時処理 ---
            textScript?.Set(TextScript.EffectType.Parry);
            if (audioSource != null && parrySound != null)
            {
                audioSource.PlayOneShot(parrySound);
            }
            yield return new WaitForSeconds(1.0f);
            textScript?.Removed(TextScript.EffectType.Parry);

            // LBモードなら追撃
            if (isLBMode)
            {
                StartCoroutine(LBAttack());
            }
        }
        else
        {
            // --- 失敗時処理（硬直） ---
            notMove = true;
            isParryCoolTime = true;

            float currentCoolTime = isLBMode ? lb.lBCoolTime : parryCoolTime;
            float coolTimer = 0f;

            float blinkInterval = 0.1f;
            float blinkTimer = 0f;

            // 硬直時間ループ
            while (coolTimer < currentCoolTime)
            {
                coolTimer += Time.deltaTime;
                blinkTimer += Time.deltaTime;

                // プレイヤー点滅処理
                if (blinkTimer >= blinkInterval)
                {
                    if (rend != null) rend.enabled = !rend.enabled;
                    blinkTimer = 0f;
                }

                yield return null;
            }

            // 状態復帰
            if (rend != null)
            {
                rend.enabled = true;
            }
            notMove = false;
            isParryCoolTime = false;
        }

        // フラグリセット
        ObjectParry.parrySuccess = false;
    }

    private IEnumerator LBAttack()
    {
        // アニメーション再生
        if (animator != null) animator.SetTrigger("LimitBreak");

        yield return new WaitForSeconds(2.0f);

        // エフェクト生成
        if (lBEffect != null && enemyPos != null)
        {
            Destroy(Instantiate(lBEffect, enemyPos.position, Quaternion.identity), 2.0f);
        }

        // ダメージ適用
        if (enemy != null && lb != null) enemy.Damage(lb.lBDamage);
    }
}