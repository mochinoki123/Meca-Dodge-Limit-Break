using UnityEngine;
using System.Collections;

public class PlayerDamage : MonoBehaviour
{
    [Header("Settings")]
    // 無敵時間
    [SerializeField] private float mutekiTime = 2.0f;
    // 点滅間隔
    [SerializeField] private float blinkInterval = 0.1f;

    private PlayerMove playerMove;
    private PlayerParry playerParry;
    private PlayerPulseDiffuser playerPulseDiffuser;
    private Renderer rend;
    private MaterialScript materialScript;

    private bool isMuteki = false;

    private void Awake()
    {
        // コンポーネント取得
        playerMove = GetComponent<PlayerMove>();
        playerParry = GetComponent<PlayerParry>();
        playerPulseDiffuser = GetComponent<PlayerPulseDiffuser>();
        rend = GetComponentInChildren<Renderer>();
        materialScript = GetComponent<MaterialScript>();
    }

    // 被弾可否判定
    private bool CanTakeDamage()
    {
        if (isMuteki) return false;
        if (playerMove.isRun) return false;
        if (playerParry.isParry) return false;
        if (playerPulseDiffuser.isPD) return false;
        return true;
    }

    private void OnParticleCollision(GameObject other)
    {
        // パーティクル被弾処理
        if (!CanTakeDamage()) return;

        if (other.CompareTag("FirePoint"))
        {
            ApplyDamage();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 接触判定
        if (!CanTakeDamage()) return;

        // ミサイル処理
        if (other.CompareTag("Missile"))
        {
            var missile = other.GetComponentInParent<enemymissile>();
            missile?.Kill();
            ApplyDamage();
        }
        // レーザー処理
        else if (other.CompareTag("Lazer"))
        {
            var lazer = other.GetComponentInParent<enemylazer>();
            lazer?.Kill();
            ApplyDamage();
        }
    }

    private void ApplyDamage()
    {
        // ダメージ適用と無敵開始
        GameManager.Instance.Damage();
        StartCoroutine(MutekiRoutine());
    }

    private IEnumerator MutekiRoutine()
    {
        // 無敵設定とマテリアル変更
        isMuteki = true;
        materialScript.ChangeMaterial(MaterialScript.EffectType.Damage, 2f);

        // 点滅ループ
        float elapsed = 0;
        while (elapsed < mutekiTime)
        {
            rend.enabled = !rend.enabled;
            yield return new WaitForSeconds(blinkInterval);
            elapsed += blinkInterval;
        }

        // 復帰処理
        rend.enabled = true;
        isMuteki = false;
    }
}