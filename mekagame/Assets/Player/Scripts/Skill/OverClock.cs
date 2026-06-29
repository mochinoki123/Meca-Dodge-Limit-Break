using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using R3;

public class OverClock : MonoBehaviour
{
    // 効果時間
    [SerializeField] private float oCTime;
    // 効果中の速度
    [SerializeField] public float oCSpeed;
    // クールタイム
    [SerializeField] public float oCCoolTime;
    // 効果中のグレイズ範囲
    [SerializeField] public float oCGrazeRange;
    // 発動時SE
    [SerializeField] private AudioClip overClock;

    private readonly ReactiveProperty<bool> isOC = new ReactiveProperty<bool>(false);

    public ReadOnlyReactiveProperty<bool> IsOC => isOC;
    PlayerGraze pg;
    AudioSource audioSource;
    MaterialScript materialScript;
    Animator animator;

    private PlayerPulseDiffuser pulsediffuser;

    private void Awake()
    {
        // コンポーネント取得
        pg = GetComponentInChildren<PlayerGraze>();
        audioSource = GetComponent<AudioSource>();
        materialScript = GetComponent<MaterialScript>();
        animator = GetComponent<Animator>();
        pulsediffuser = GetComponent<PlayerPulseDiffuser>();

        pulsediffuser.IsPD
            .Where (isPD => isPD == true)
            .Subscribe(isPD =>
            {
                StopAllCoroutines();
                ResetOverClock();
            })
            .AddTo(this);
    }

    private void OnOverClock(InputValue value)
    {
        // 既に使用中なら中断
        if (isOC.Value) return;
        StartCoroutine(PlayOverClock());
    }

    private IEnumerator PlayOverClock()
    {
        // ゲージ残量チェック
        if (GameManager.Instance.NowGage >= GameManager.Instance.GetterUseGauge(GameManager.UseGaugeState.OverClock))
        {
            // SE再生
            audioSource.PlayOneShot(overClock);
            // ゲージ消費とフラグ設定
            GameManager.Instance.UseGaugeStateBranch(GameManager.UseGaugeState.OverClock);
            isOC.Value = true;

            animator?.SetTrigger("IsOC");

            materialScript.ChangeMaterial(MaterialScript.EffectType.OverClock, oCTime);

            // グレイズ範囲拡大
            pg.SetOCRange(oCGrazeRange);

            // 効果時間待機
            yield return new WaitForSeconds(oCTime);

            ResetOverClock();
        }
    }

    private void ResetOverClock()
    {
        // 終了処理・範囲リセット
        isOC.Value = false;
        pg.ResetRange();
    }
}