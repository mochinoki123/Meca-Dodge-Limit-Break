using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
// using Unity.VisualScripting; // 基本的に不要であれば削除推奨

public class PlayerParry : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameObject playerParry;
    [SerializeField] private Enemy enemy;
    [SerializeField] private float parryTime = 0.5f;      // パリィ受付時間
    [SerializeField] private float parryCoolTime = 1.0f;  // 失敗時の硬直時間（秒）
    [SerializeField] private AudioClip parrySound;        // 変数名を分かりやすく変更
    [SerializeField] private GameObject lBEffect;
    [SerializeField] private Transform enemyPos;

    [Header("Visuals")]
    [SerializeField] private Material rockColor;
    [SerializeField] private Material originalColor;

    // 状態フラグ
    public bool isParry = false;
    private bool isParryCoolTime = false;
    public bool notMove = false;
    // public bool isParryCommand = false; // 未使用なら削除推奨
    private float alpha_Sin;

    // 参照
    LimitBreak lb;
    PlayerPulseDiffuser pd;
    Animator animator;
    AudioSource audioSource;
    private Renderer rend;

    private void Awake()
    {
        lb = GetComponent<LimitBreak>();
        pd = GetComponent<PlayerPulseDiffuser>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        rend = GetComponentInChildren<Renderer>();
    }

    private void Update()
    {
        alpha_Sin = Mathf.Sin(Time.time) / 2 + 0.5f;
    }

    private void OnParry(InputValue value)
    {
        if (pd != null && pd.isPD) return;
        if (isParry) return;
        if (isParryCoolTime) return;
        if (notMove) return; // 既に硬直中なら入力を受け付けない

        StartCoroutine(Parry());
    }

    public IEnumerator Parry()
    {
        // 1. LB状態の確認
        bool isLBMode = (lb != null) && lb.isLB;

        // 2. パリィ開始
        isParry = true;
        playerParry.SetActive(true);
        ObjectParry.parrySuccess = false;

        float currentDuration = isLBMode ? lb.lBTime : parryTime;
        float timer = 0f;

        // 3. パリィ受付ループ
        while (timer < currentDuration)
        {
            if (ObjectParry.parrySuccess) break; // 成功したら即抜ける

            timer += Time.deltaTime;
            yield return null;
        }

        // 4. パリィ判定終了（成功・失敗に関わらず判定枠を消す）
        playerParry.SetActive(false);
        isParry = false;

        // 5. 結果分岐
        if (ObjectParry.parrySuccess)
        {
            // ===========================
            //  成功時：硬直なしで攻撃へ
            // ===========================
            if (audioSource != null && parrySound != null)
            {
                audioSource.PlayOneShot(parrySound);
            }

            // LBモードなら追加攻撃
            if (isLBMode)
            {
                StartCoroutine(LBAttack());
            }

            // 成功時はここで処理終了（クールタイム発生させない場合）
            // ※成功時もクールタイムが欲しい場合はここに処理を追加
        }
        else
        {
            // ===========================
            //  失敗時：硬直（ペナルティ）発生
            // ===========================

            // 硬直開始
            notMove = true;
            isParryCoolTime = true;

            // 色を変える
            if (rend != null) rend.material.color = rockColor.color;

            float currentCoolTime = isLBMode ? lb.lBCoolTime : parryCoolTime;
            float coolTimer = 0f;

            // 点滅用タイマー
            float blinkInterval = 0.1f;
            float blinkTimer = 0f;

            // 指定された秒数(currentCoolTime)だけループする
            while (coolTimer < currentCoolTime)
            {
                coolTimer += Time.deltaTime;
                blinkTimer += Time.deltaTime;

                // 一定間隔で表示・非表示を切り替え（点滅）
                if (blinkTimer >= blinkInterval)
                {
                    if (rend != null) rend.enabled = !rend.enabled;
                    blinkTimer = 0f;
                }

                yield return null;
            }

            // 硬直終了処理
            if (rend != null)
            {
                rend.enabled = true; // 確実に表示状態に戻す
                rend.material.color = originalColor.color;
            }
            notMove = false;
            isParryCoolTime = false;
        }

        // 最後にフラグをリセット
        ObjectParry.parrySuccess = false;
    }

    private IEnumerator LBAttack()
    {
        if (animator != null) animator.SetTrigger("LimitBreak");

        // 攻撃中は動けないようにするならここで notMove = true もあり
        // notMove = true; 

        yield return new WaitForSeconds(2.0f);

        if (lBEffect != null)
        {
            GameObject effect = Instantiate(lBEffect, enemyPos.position, Quaternion.identity);
            Destroy(effect, 2.0f); // Destroyの第二引数で遅延破壊できます
        }

        if (enemy != null && lb != null) enemy.Damage(lb.lBDamage);

        // 攻撃終了
        // notMove = false;
    }
}