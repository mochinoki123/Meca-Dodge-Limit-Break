using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class OverClock : MonoBehaviour
{
    // 消費ゲージ量
    [SerializeField] private int oCUseGage;
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

    public bool isOC { get; private set; } = false;
    PlayerGraze pg;
    AudioSource audioSource;
    MaterialScript materialScript;

    private void Awake()
    {
        // コンポーネント取得
        pg = GetComponentInChildren<PlayerGraze>();
        audioSource = GetComponent<AudioSource>();
        materialScript = GetComponent<MaterialScript>();
    }

    private void OnOverClock(InputValue value)
    {
        // 既に使用中なら中断
        if (isOC) return;
        StartCoroutine(PlayOverClock());
    }

    private IEnumerator PlayOverClock()
    {
        // ゲージ残量チェック
        if (GameManager.Instance.NowGage >= oCUseGage)
        {
            // SE再生
            audioSource.PlayOneShot(overClock);
            // ゲージ消費とフラグ設定
            GameManager.Instance.UseGage(oCUseGage);
            isOC = true;

            materialScript.ChangeMaterial(MaterialScript.EffectType.OverClock, oCTime);

            // グレイズ範囲拡大
            pg.SetOCRange(oCGrazeRange);

            // 効果時間待機
            yield return new WaitForSeconds(oCTime);

            // 終了処理・範囲リセット
            isOC = false;
            pg.ResetRange();
        }
    }
}