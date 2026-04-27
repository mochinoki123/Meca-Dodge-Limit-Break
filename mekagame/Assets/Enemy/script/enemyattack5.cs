using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class enemyattack5 : MonoBehaviour
{
    private enemyattack enemymanager;
    enemylazer enemyLazer;
    [Header("レーザープレハブ")]
    [SerializeField] int attack2lazerz = 50;
    [SerializeField] GameObject lazerattackpoint;//レーザー発生ポイントオブジェクト
    [Header("レーザー効果音")]
    [SerializeField] private AudioClip lazerclip;
    [SerializeField] private AudioClip lazercharge;
    private AudioSource audioSource;
    [Header("エフェクト")]
    [SerializeField] GameObject lazerchargeeffect;//レーザーチャージエフェクト
    [Header("攻撃Ⅴ")]
    //攻撃Ⅴ
    [SerializeField] GameObject lazerz;//レーザーオブジェクト
    [SerializeField] GameObject lazerattackpointz;//レーザー発生ポイントオブジェクト
    [SerializeField] int Attack5ls = 10;//攻撃５のレーザー数
    [SerializeField] public float l5x = 60;//ｘ攻撃開始地点・範囲
    [SerializeField] public float l5z = 50;//ｚ攻撃開始地点・範囲
    [SerializeField] public float k = 13;// 攻撃数
    [SerializeField] int attack5lx = -100;
                                   //レーザーy座標関係
    [Header("レーザー座標指定・レーザー長指定")]
    [SerializeField] int lazerpointy = 7; // 
    [SerializeField] float maxLength = -50f;   // 最終的な長さ
    [SerializeField] float maxLengthx = 50f;   // 最終的な長さ
    [SerializeField] float extendSpeed = 100f;  // 伸びるスピード
    float attackbunki;//random値確認用基本使わない
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        enemyLazer = FindAnyObjectByType<enemylazer>();
        enemymanager = GetComponent<enemyattack>();
        Invoke("Attack5", 16f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //-----攻撃Ⅴ-----
    public void Attack5()
    {
        attackbunki = Random.Range(0f, 1f);//攻撃分岐

        if (attackbunki < 0.5f)
        {
            StartCoroutine(Attack5lxCoroutine());//奥から攻撃
        }
        else
        {
            StartCoroutine(Attack5lzCoroutine());//横から攻撃
        }


    }

    //攻撃Ⅴ縦レーザー
    IEnumerator Attack5lxCoroutine()
    {
        int i = 0;
        while (i < Attack5ls)//連続縦レーザー攻撃
        {
            Attack5lpx();//縦レーザーポイント突入
            i++;
            yield return new WaitForSeconds(2f);//2秒ごとにループする
        }
        l5x = 60;
        /*
        l5x = 60f;

        for (int i = 0; i < Attack5ls; i++)
        {
            Attack5lpx(new Vector3(l5x, lazerpointy, attack2lazerz));

            l5x -= k;

            yield return new WaitForSeconds(2f);
        }*/
    }

    //攻撃Ⅴ横レーザー
    IEnumerator Attack5lzCoroutine()
    {
        int i = 0;
        while (i < Attack5ls)//連続横レーザー攻撃
        {
            Attack5lpz();//横レーザーポイント突入
            i++;
            yield return new WaitForSeconds(2f);//2秒ごとにループする
        }
        //Debug.Log("攻撃Ⅴ");
        l5z = 50;
    }

    //攻撃Ⅴ縦レーザーポイント
    void Attack5lpx()
    {
        audioSource.PlayOneShot(lazercharge);
        GameObject Attack5lazerattackpoint = Instantiate(lazerattackpoint, new Vector3(l5x, 0, 0), Quaternion.identity);//縦レーザー発射地点
        GameObject Attacklazerchargeeffect = Instantiate(lazerchargeeffect, new Vector3(l5x, lazerpointy, attack2lazerz), Quaternion.identity);
        Destroy(Attack5lazerattackpoint, 2f);
        Destroy(Attacklazerchargeeffect, 2f);
        Invoke("Attack5lx", 2f);
    }

    //攻撃Ⅴ縦レーザー
    void Attack5lx()
    {
        audioSource.PlayOneShot(lazerclip);
        GameObject lazerObj = enemymanager.Getl();
        lazerObj.transform.position = new Vector3(l5x, lazerpointy, attack2lazerz);
        //StartCoroutine(ExtendLazer5x(lazerObj));
    }
    //IEnumerator ExtendLazer5x(GameObject lazerObj)
    //{
    //    Vector3 scale = lazerObj.transform.localScale;
    //    scale.z = 0;
    //    l5x = l5x - k;//発射地点を横にずらす
    //    lazerObj.transform.localScale = scale;

    //    while (scale.z > maxLength)
    //    {
    //        scale.z -= extendSpeed * Time.deltaTime;
    //        lazerObj.transform.localScale = scale;

    //        yield return null;
    //    }
    //    yield return new WaitForSeconds(1f);

    //    Returnl(lazerObj);
    //    /*
    //    Vector3 scale = lazerObj.transform.localScale;
    //    scale.z = 0f; // 最初は長さ0
    //    l5x = l5x - k;//発射地点を横にずらす
    //    lazerObj.transform.localScale = scale;
    //    while (scale.z > maxLength)
    //    {
    //        scale.z -= extendSpeed * Time.deltaTime;
    //        lazerObj.transform.localScale = scale;
    //        Destroy(lazerObj, 1f);
    //        //Debug.Log("攻撃Ⅴx");
    //        yield return null; // 次のフレームへ
    //    }

    //    // 最終値を保証
    //    //scale.z = maxLength;
    //    lazerObj.transform.localScale = scale;

    //    Returnl(lazerObj);*/
    //}

    //攻撃Ⅴ横レーザーポイント
    void Attack5lpz()
    {
        audioSource.PlayOneShot(lazercharge);
        GameObject Attack5lazerattackpointx = Instantiate(lazerattackpointz, new Vector3(0, 0, l5z), Quaternion.identity);//横レーザー発射地点
        GameObject Attacklazerchargeeffect = Instantiate(lazerchargeeffect, new Vector3(attack5lx, lazerpointy, l5z), Quaternion.identity);
        Destroy(Attack5lazerattackpointx, 2f);
        Destroy(Attacklazerchargeeffect, 2f);
        Invoke("Attack5lz", 2f);
    }

    //攻撃Ⅴ横レーザー
    void Attack5lz()
    {
        audioSource.PlayOneShot(lazerclip);
        GameObject lazerObjx = enemymanager.Getlx();
        lazerObjx.transform.position = new Vector3(attack5lx, lazerpointy, l5z);//発射
        //lazerObjx.AngleLazer(enemylazer.LazerAngle.Hor);
        //Rigidbody cubeRigidbody = Attack5lazerx.GetComponent<Rigidbody>();
        //cubeRigidbody.AddForce(new Vector3(1, 0, 0) * 10, ForceMode.Impulse);
        //StartCoroutine(ExtendLazer5z(lazerObjx));

        //Debug.Log("攻撃Ⅴz");
    }

    //IEnumerator ExtendLazer5z(GameObject lazerObjx)
    //{
    //    Vector3 scale = lazerObjx.transform.localScale;
    //    scale.x = 0; // 最初は長さ0
    //    l5z = l5z - k;//発射地点を縦にずらす
    //    lazerObjx.transform.localScale = scale;
    //    while (scale.x < maxLengthx)
    //    {
    //        scale.x += extendSpeed * Time.deltaTime;
    //        lazerObjx.transform.localScale = scale;
    //        yield return null; // 次のフレームへ
    //    }
    //    yield return new WaitForSeconds(1f);
    //    // 最終値を保証
    //    //scale.x = maxLength;
    //    Returnlx(lazerObjx);
    //}
}
