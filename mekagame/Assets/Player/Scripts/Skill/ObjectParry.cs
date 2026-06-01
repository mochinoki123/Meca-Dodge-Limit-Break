using System.Collections.Generic;
using UnityEngine;

public class ObjectParry : MonoBehaviour
{
    // パリィ成功時のエフェクト
    [SerializeField] private GameObject parryEffect;

    // パリィ成功フラグ（外部読み取り専用・PlayerParryが参照）
    public static bool ParrySuccess { get; private set; }

    // 同一オブジェクトへの重複判定を防ぐためInstanceIDで管理
    private HashSet<int> parriedInstanceIDs = new HashSet<int>();

    private void OnTriggerEnter(Collider other)
    {
        // パリィ対象でなければ無視
        GameObject targetObj = TryGetParryTarget(other);
        Debug.Log($"targetObj: {(targetObj != null ? targetObj.name : "null")}");
        if (targetObj == null) return;

        // 既にパリィ済みのオブジェクトなら無視
        int id = targetObj.GetInstanceID();
        if (parriedInstanceIDs.Contains(id)) return;

        // パリィ済みとして登録し成功処理へ
        parriedInstanceIDs.Add(id);
        OnParrySuccess();
    }

    // タグからパリィ対象のルートオブジェクトを返す（対象外はnull）
    private GameObject TryGetParryTarget(Collider other)
    {
        if (other.CompareTag("Missile"))
        {
            var script = other.GetComponentInParent<enemymissile>();
            return script != null ? script.gameObject : null;
        }

        if (other.CompareTag("Lazer"))
        {
            var script = other.GetComponentInParent<LaserCollider>();
            return script != null ? script.gameObject : null;
        }

        return null;
    }

    // パリィ成功時の処理（エフェクト生成・フラグ設定・ゲージ加算）
    private void OnParrySuccess()
    {
        // エフェクトを自身の位置に生成し一定時間後に破棄
        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        GameObject effect = Instantiate(parryEffect, spawnPos, Quaternion.identity);
        Destroy(effect, 1.0f);

        // 成功フラグを立ててゲージを加算
        ParrySuccess = true;
        GameManager.Instance.AddGage(50);
    }

    private void OnDisable()
    {
        // 非アクティブ化時にパリィ済みIDをリセット
        // （SetActive(false)のタイミングでParrySuccessには触れない）
        parriedInstanceIDs.Clear();
    }

    // PlayerParryから呼ばれるパリィフラグのリセット
    public static void ResetParry()
    {
        ParrySuccess = false;
    }
}