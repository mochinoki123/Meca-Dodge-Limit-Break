using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class PlayerGraze : MonoBehaviour
{
    private HashSet<GameObject> grazedMissiles = new HashSet<GameObject>();
    [SerializeField] private float grazeRange;
    [SerializeField] private int ocAddGage;
    [SerializeField] private int addGage;
    [SerializeField] private AudioClip graze;
    [SerializeField] private GameObject grazeEffect;

    private float range;
    private PlayerMove playerMove;
    OverClock oc;
    TextScript textScript;
    SphereCollider myCollider;
    AudioSource audioSource;

    private void Awake()
    {
        // コンポーネント取得
        myCollider = GetComponent<SphereCollider>();
        oc = GetComponentInParent<OverClock>();
        audioSource = GetComponentInParent<AudioSource>();
        playerMove = GetComponentInParent<PlayerMove>();
        textScript = GetComponentInParent<TextScript>();

        // 初期範囲設定
        Range();
    }

    private void OnTriggerStay(Collider other)
    {
        // 敵攻撃かつダッシュ中のみ判定
        if ((other.CompareTag("Missile") || other.CompareTag("Lazer")) && playerMove.isRun)
        {
            // 重複グレイズ防止
            if (!grazedMissiles.Contains(other.gameObject))
            {
                audioSource.PlayOneShot(graze);
                StartCoroutine(AddGraze(other.gameObject));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 範囲外に出たら再グレイズ可能にするため削除
        if (grazedMissiles.Contains(other.gameObject))
        {
            grazedMissiles.Remove(other.gameObject);
        }
    }

    public void OCRange(float range)
    {
        // OC用範囲適用
        myCollider.radius = range;
    }

    public void Range()
    {
        // 通常範囲適用
        myCollider.radius = grazeRange;
    }

    IEnumerator AddGraze(GameObject obj)
    {
        GameObject effect = Instantiate(grazeEffect, transform);
        textScript.Set(TextScript.EffectType.Graze);
        // グレイズ確定処理とゲージ加算
        grazedMissiles.Add(obj);
        if (oc.isOC) GameManager.Instance.AddGage(ocAddGage);
        else GameManager.Instance.AddGage(addGage);
        yield return new WaitForSeconds(1.0f);
        textScript.Removed(TextScript.EffectType.Graze);
    }
}