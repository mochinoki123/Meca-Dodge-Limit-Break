using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using Unity.VisualScripting;



public class PlayerParry : MonoBehaviour
{
    [SerializeField] private GameObject playerParry;
    [SerializeField] private  Enemy enemy;
    [SerializeField] private float parryTime;
    [SerializeField] private float parryCoolTime;
    [SerializeField] private AudioClip parry;
    [SerializeField] private GameObject lBEffect;
    [SerializeField] private Transform enemyPos;
    public bool isParry = false;
    bool isParryCoolTime = false;
    public bool notMove = false;
    public bool isParryCommand = false;
    LimitBreak lb;
    PlayerPulseDiffuser pd;
    Animator animator;
    AudioSource audioSource;

    private void Awake()
    {
        lb = GetComponent<LimitBreak>();
        pd = GetComponent<PlayerPulseDiffuser>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    private void OnParry(InputValue value)
    {
        if (pd.isPD) return;
        if (isParry) return;
        if (isParryCoolTime) return;
        StartCoroutine(Parry());
    }
    public IEnumerator Parry()
    {
        // 1. LB状態と安全性の確認
        bool isLBMode = (lb != null) && lb.isLB;

        // 2. パリィ開始設定
        isParry = true;
        playerParry.SetActive(true);
        ObjectParry.parrySuccess = false; // 開始時に必ずリセット

        float currentDuration = isLBMode ? lb.lBTime : parryTime;
        float timer = 0f;

        // 3. 【変更点】パリィ受付ループ
        // 指定時間が経過するか、パリィが成功するまでループします
        while (timer < currentDuration)
        {
            // もしパリィ成功フラグが立ったら、ループを即座に抜ける（残り時間を待たない）
            if (ObjectParry.parrySuccess)
            {
                break;
            }

            timer += Time.deltaTime; // 時間を進める
            yield return null;       // 1フレーム待機
        }

        // 4. 判定終了（成功でも失敗でも、判定枠はここで消す）
        playerParry.SetActive(false);
        isParry = false; // パリィ動作自体は終了

        // 5. 結果処理
        if (ObjectParry.parrySuccess)
        {
            // --- 成功時（即座に実行されます） ---
            if (audioSource != null)
            {
                audioSource.PlayOneShot(parry);
            }

            // LBモードなら追加攻撃
            if (lb != null && lb.isLB)
            {
                StartCoroutine(LBAttack());
            }
        }
        else
        {
            // --- 失敗時（時間切れ） ---
            // 失敗時のみ硬直ペナルティ
            notMove = true;
        }

        // フラグ使用済みのためリセット
        ObjectParry.parrySuccess = false;

        // 6. クールダウン処理
        isParryCoolTime = true;
        float currentCoolTime = isLBMode ? lb.lBCoolTime : parryCoolTime;

        yield return new WaitForSeconds(currentCoolTime);

        // 7. 全処理完了
        isParryCoolTime = false;
        notMove = false;
    }
    private IEnumerator LBAttack()
    {
        if (animator != null) animator.SetTrigger("LimitBreak");
        yield return new WaitForSeconds(2.0f);
        GameObject effect = Instantiate(lBEffect, enemyPos.position, Quaternion.identity);
        yield return new WaitForSeconds(2.0f);
        Destroy(effect);
        if (enemy != null) enemy.Damage(lb.lBDamage);

    }
}
