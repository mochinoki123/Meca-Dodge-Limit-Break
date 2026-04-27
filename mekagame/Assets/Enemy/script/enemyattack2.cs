using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine.Rendering;

public class enemyattack2 : MonoBehaviour
{
    private enemyattack enemymanager;
    //攻撃２
    [Header("攻撃Ⅱ")]
    [SerializeField] int attack2lazerz;//50
    [Header("レーザー効果音")]
    [SerializeField] private AudioClip lazerclip;
    [SerializeField] private AudioClip lazercharge;
    private AudioSource audioSource;
    [Header("エフェクト")]
    [SerializeField] GameObject lazerchargeeffect;//レーザーチャージエフェクト
    [Header("レーザープレハブ")]
    [SerializeField] GameObject lazerattackpoint;//レーザー発生ポイントオブジェクト
    [Header("レーザー座標指定・レーザー長指定")]
    [SerializeField] int lazerpointy = 7; // 7
    [SerializeField] float maxLength = -50f;   // 最終的な長さ
    [SerializeField] float maxLengthx = 50f;   // 最終的な長さ
    [SerializeField] float extendSpeed = 100f;  // 伸びるスピード
    enemylazer enemyLazer;
    [SerializeField] float rndm = -11;//フィールドごとの範囲指定マイナス
    [SerializeField] float rndp = 11;//フィールドごとの範囲指定プラス
    public float groundx;//random値確認用基本使わない
    public float groundz;//random値確認用基本使わない
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        enemyLazer = FindAnyObjectByType<enemylazer>();
        enemymanager = GetComponent<enemyattack>();
        Invoke("Attack2", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //-----攻撃Ⅱ-----
    void Attack2()
    {
        groundx = Random.Range(rndm, rndp);//地面の広さによって変更
        groundz = Random.Range(rndm, rndp);//地面の広さによって変更
        audioSource.PlayOneShot(lazercharge);
        GameObject Attack2lazerattackpoint = Instantiate(lazerattackpoint, new Vector3(groundx, 0, 0), Quaternion.identity);//レーザー発射地点
        GameObject Attacklazerchargeeffect = Instantiate(lazerchargeeffect, new Vector3(groundx, lazerpointy, attack2lazerz), Quaternion.identity);
        Destroy(Attack2lazerattackpoint, 1.3f);//1.3秒後に破壊
        Destroy(Attacklazerchargeeffect, 2f);//2秒後に破壊
        StartCoroutine(LaserRoutine());
    }

    //攻撃Ⅱレーザー
    IEnumerator LaserRoutine()
    {
        yield return new WaitForSeconds(1.3f);

        audioSource.PlayOneShot(lazerclip);

        GameObject lazer2 = enemymanager.Getl();

        lazer2.transform.position = new Vector3(groundx, lazerpointy, attack2lazerz);

        StartCoroutine(ExtendLaser(lazer2));
    }

    IEnumerator ExtendLaser(GameObject lazer2)
    {
        Vector3 scale = lazer2.transform.localScale;
        scale.z = 0;
        lazer2.transform.localScale = scale;

        while (scale.z > maxLength)
        {
            scale.z -= extendSpeed * Time.deltaTime;
            lazer2.transform.localScale = scale;

            yield return null;
        }
        yield return new WaitForSeconds(1f);

        enemymanager.Returnl(lazer2);
    }
}
