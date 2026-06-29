using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectParry : MonoBehaviour
{
    // パリィ成功時のエフェクト
    [SerializeField] private GameObject parryEffect;

    // パリィ成功フラグ
    private bool parrySuccess;

    // 同一オブジェクトへの重複判定を防ぐためInstanceIDで管理
    private HashSet<int> parriedInstanceIDs = new HashSet<int>();

    //外部が参照するためのイベント
    public static event Action<bool> OnParrySuccesState;

    public bool ParrySuccess
    {
        get => parrySuccess;
        set
        {
            if (parrySuccess != value) 
            {
                parrySuccess = value;
                OnParrySuccesState?.Invoke(parrySuccess);
            }
        }
    }

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
            var missile = other.GetComponentInParent<MissileRelease>();
            missile?.Release();
            return missile != null ? missile.gameObject : null;
        }

        if (other.CompareTag("Lazer"))
        {
            var script = other.GetComponentInParent<LaserCollider>();
            var laser = other.GetComponentInParent<ReleaseLaser>();
            laser?.Release();
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
        GameManager.Instance.AddGaugeStateBranch(GameManager.AddGaugeState.Parry);
    }

    private void OnDisable()
    {
        // 非アクティブ化時にパリィ済みIDをリセット
        // （SetActive(false)のタイミングでParrySuccessには触れない）
        parriedInstanceIDs.Clear();
    }

    // PlayerParryから呼ばれるパリィフラグのリセット
    public void ResetParry()
    {
        ParrySuccess = false;
    }
}