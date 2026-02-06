using UnityEngine;
using System.Collections;

public class MaterialScript : MonoBehaviour
{
    [SerializeField] private Material originalMaterial;
    [SerializeField] private Material pulseMaterial;
    [SerializeField] private Material overclockMaterial;
    [SerializeField] private Material damageMaterial;

    private Renderer rend;
    private Coroutine currentEffectCoroutine; // 一時演出用
    private Coroutine baseResetCoroutine;     // 状態リセット用
    private Material currentBaseMaterial;     // 復帰先マテリアル

    public enum EffectType { Original, Pulse, OverClock, Damage }

    private void Awake()
    {
        rend = GetComponentInChildren<Renderer>();
        currentBaseMaterial = originalMaterial;
    }

    public void ChangeMaterial(EffectType type, float duration)
    {
        if (rend == null) return;

        // 状態変化系（OC, Pulse）のベース管理
        if (type == EffectType.OverClock || type == EffectType.Pulse)
        {
            // タイマーリセット
            if (baseResetCoroutine != null) StopCoroutine(baseResetCoroutine);

            // ベース変更とリセット予約
            baseResetCoroutine = StartCoroutine(ResetBaseAfterTime(type, duration));
        }

        // 見た目の即時反映
        if (currentEffectCoroutine != null) StopCoroutine(currentEffectCoroutine);
        currentEffectCoroutine = StartCoroutine(ChangeVisualRoutine(type, duration));
    }

    // 一定時間後にベースを初期状態に戻す
    private IEnumerator ResetBaseAfterTime(EffectType type, float duration)
    {
        currentBaseMaterial = GetMaterialByType(type);
        yield return new WaitForSeconds(duration);
        currentBaseMaterial = originalMaterial;

        // 演出中でなければ即座に戻す
        if (currentEffectCoroutine == null) rend.material = originalMaterial;
    }

    // 一時的な見た目の切り替え
    private IEnumerator ChangeVisualRoutine(EffectType type, float duration)
    {
        rend.material = GetMaterialByType(type);
        yield return new WaitForSeconds(duration);

        // 現在のベースへ復帰
        rend.material = currentBaseMaterial;
        currentEffectCoroutine = null;
    }

    // マテリアル取得ヘルパー
    private Material GetMaterialByType(EffectType type)
    {
        switch (type)
        {
            case EffectType.Pulse: return pulseMaterial;
            case EffectType.OverClock: return overclockMaterial;
            case EffectType.Damage: return damageMaterial;
            default: return originalMaterial;
        }
    }
}