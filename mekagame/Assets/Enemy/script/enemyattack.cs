using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Pool;
using UnityEngine.Rendering;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using static UnityEngine.Rendering.ObjectPool<T>;

public class enemyattack : MonoBehaviour
{
    //Enemyスクリプト
    private Enemy enemyhpscripts;
    #region オブジェクトプール
    [Header("プール数")]
    //ミサイル
    public int sizem = 50;
    private Queue<GameObject> missilepool = new Queue<GameObject>();
    //縦レーザー
    public int sizel = 50;
    private Queue<GameObject> lazerpool = new Queue<GameObject>();
    //横レーザー
    public int sizelx = 50;
    private Queue<GameObject> lazerxpool = new Queue<GameObject>();
    /*
    //攻撃Ⅳミサイル
    public int sizem2 = 10;  
    private Queue<GameObject> missile2pool = new Queue<GameObject>();
    */
    /*
    //爆発ポイント
    public int sizeb = 50;
    private Queue<GameObject> bpointpool = new Queue<GameObject>();
    //爆発エフェクト
    public int sizebe = 50;
    private Queue<GameObject> beffectpool = new Queue<GameObject>();
    */
    #endregion

    #region 効果音
    [Header("レーザー効果音")]
    [SerializeField] private AudioClip lazerclip;
    [SerializeField] private AudioClip lazercharge;
    private AudioSource audioSource;
    #endregion
    //プレハブ
    #region プレハブ
    [Header("爆発ポイントプレハブ")]
    [SerializeField] GameObject bpoint;//爆発ポイント
    [Header("ミサイルプレハブ")]
    [SerializeField] GameObject missile;//ミサイル攻撃のオブジェクト
    [SerializeField] GameObject missile4;//ミサイル攻撃のオブジェクト
    [Header("レーザープレハブ")]
    [SerializeField] GameObject lazer;//レーザーオブジェクト
    [SerializeField] GameObject lazerattackpoint;//レーザー発生ポイントオブジェクト
    //攻撃Ⅴ
    [SerializeField] GameObject lazerz;//レーザーオブジェクト
    [SerializeField] GameObject lazerattackpointz;//レーザー発生ポイントオブジェクト
    //フィールド範囲
    [Header("エフェクト")]
    [SerializeField] GameObject lazerchargeeffect;//レーザーチャージエフェクト
    [SerializeField] GameObject ClustereffectPrefab;//爆発のエフェクト
    #endregion
    [Header("攻撃範囲指定")]
    [SerializeField] float rndm = -9;//フィールドごとの範囲指定マイナス
    [SerializeField] float rndp = 9;//フィールドごとの範囲指定プラス
    //攻撃座標関係
    [Header("攻撃座標指定")]
    [SerializeField] int attackf;//攻撃の間隔 5
    [SerializeField] int attackpointx;//攻撃発生の横 10
    [SerializeField] int attackpointy;//攻撃発生の高さ 25
    [SerializeField] int attackpointz;//攻撃発生の奥行 10
    //レーザーy座標関係
    [Header("レーザー座標指定・レーザー長指定")]
    [SerializeField] int lazerpointy = 7; // 7
    [SerializeField] float maxLength = -50f;   // 最終的な長さ
    [SerializeField] float maxLengthx = 50f;   // 最終的な長さ
    [SerializeField] float extendSpeed = 100f;  // 伸びるスピード
    enemylazer enemyLazer;

    //攻撃１
    [Header("攻撃Ⅰ")]
    [SerializeField] int attack1missile;//攻撃１のミサイル数　6
    //攻撃２
    [Header("攻撃Ⅱ")]
    [SerializeField] int attack2lazerz;//50
    //攻撃３
    [Header("攻撃Ⅲ")]
    [SerializeField] int attack3missilex;//攻撃Ⅲxの範囲設定　10
    [SerializeField] int attack3missiley;//攻撃Ⅲ+の範囲設定　10
                                         //[SerializeField] int attackpointx3;//もしも用　使ってない
                                         //[SerializeField] int attackpointz3;//もしも用　使ってない
                                         //攻撃４
    [Header("攻撃Ⅳ")]
    [SerializeField] int attack4missile;//攻撃４のミサイル範囲指定　10
    //攻撃５
    [Header("攻撃Ⅴ")]
    [SerializeField] int Attack5ls;//攻撃５のレーザー数 10
    [SerializeField] public float l5x = 60;//ｘ攻撃開始地点・範囲
    [SerializeField] public float l5z = 50;//ｚ攻撃開始地点・範囲
    [SerializeField] public float k;//13 攻撃数
    [SerializeField] int attack5lx;//-100
    //攻撃６
    [Header("攻撃Ⅵ")]
    [SerializeField] int Attack6ms;//攻撃６のミサイル数 5
    public float y;//攻撃発生高
    Vector3 play;
    //攻撃分岐関係
    [Header("ランダム値確認")]
    public float attackbunki;//random値確認用基本使わない
    public int attack123;//random値確認用基本使わない
    public int attack12345;//random値確認用基本使わない
    public int attack123456;//random値確認用基本使わない
    public float ap;//random値確認用基本使わない
    public float groundx;//random値確認用基本使わない
    public float groundz;//random値確認用基本使わない
    //プレイヤー座標取得
    [Header("攻撃プレイヤー座標取得")]
    public float x;
    public float z;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyhpscripts = GetComponent<Enemy>();//敵データ呼び出し
        audioSource = GetComponent<AudioSource>();
        enemyLazer = FindAnyObjectByType<enemylazer>();
        CreatePool();
    }
    // Update is called once per frame
    void Update()
    {

    }

    void CreatePool()
    {
        for (int i = 0; i < sizem; i++)
        {
            GameObject objm = Instantiate(missile);
            objm.SetActive(false);
            missilepool.Enqueue(objm);
        }
        for (int i = 0; i < sizel; i++)
        {
            GameObject objl = Instantiate(lazer);
            objl.SetActive(false);
            lazerpool.Enqueue(objl);
        }
        for (int i = 0; i < sizelx; i++)
        {
            GameObject objlx = Instantiate(lazerz);
            objlx.SetActive(false);
            lazerxpool.Enqueue(objlx);
        }
        /*
        for (int i = 0; i < sizem2; i++)
        {
            GameObject objm2 = Instantiate(missile4);
            objm2.SetActive(false);
            missile2pool.Enqueue(objm2);
        }*/
        /*
        for (int i = 0; i < sizeb; i++)
        {
            GameObject objb = Instantiate(bpoint);
            objb.SetActive(false);
            missile2pool.Enqueue(objb);
        }
        for (int i = 0; i < sizebe; i++)
        {
            GameObject objbe = Instantiate(ClustereffectPrefab);
            objbe.SetActive(false);
            missile2pool.Enqueue(objbe);
        }
        */
        EnemyAttackController1();//攻撃パターンⅠ
    }

    public GameObject Get()
    {
        if (missilepool.Count > 0)
        {
            GameObject objm = missilepool.Dequeue();
            objm.SetActive(true);
            return objm;
        }
        return Instantiate(missile);
    }
    public GameObject Getl()
    {
        if (lazerpool.Count > 0)
        {
            GameObject objl = lazerpool.Dequeue();
            objl.SetActive(true);
            return objl;
        }
        return Instantiate(lazer);
    }
    public GameObject Getlx()
    {
        if (lazerxpool.Count > 0)
        {
            GameObject objlx = lazerxpool.Dequeue();
            objlx.SetActive(true);
            return objlx;
        }
        return Instantiate(lazerz);
    }
    /*
    public GameObject Getm()
    {
        if (missile2pool.Count > 0)
        {
            GameObject objm2 = missile2pool.Dequeue();
            objm2.SetActive(true);
            return objm2;
        }
        return Instantiate(missile4);
    }*/
    /*
    public GameObject Getb()
    {
        if (bpointpool.Count > 0)
        {
            GameObject objb = bpointpool.Dequeue();
            objb.SetActive(true);
            return objb;
        }
        return Instantiate(bpoint);
    }
    */
    /*
    public GameObject Getbe()
    {
        if (beffectpool.Count > 0)
        {
            GameObject objbe = beffectpool.Dequeue();
            objbe.SetActive(true);
            return objbe;
        }
        return Instantiate(ClustereffectPrefab);
    }*/
    public void Return(GameObject objm)
    {
        objm.SetActive(false);
        missilepool.Enqueue(objm);
    }
    public void Returnl(GameObject objl)
    {
        objl.transform.localScale = new Vector3(15, 15, 0);
        objl.SetActive(false);
        lazerpool.Enqueue(objl);
    }
    public void Returnlx(GameObject objlx)
    {
        objlx.transform.localScale = new Vector3(0, 15, 15);
        objlx.SetActive(false);
        lazerxpool.Enqueue(objlx);
    }
    /*
    public void Returnm(GameObject objm2)
    {
        objm2.SetActive(false);
        missile2pool.Enqueue(objm2);
    }*/
    /*
    public void Returnb(GameObject objb)
    {
        objb.SetActive(false);
        bpointpool.Enqueue(objb);
    }
    */
    /*
    public void Returnbe(GameObject objbe)
    {
        objbe.SetActive(false);
        beffectpool.Enqueue(objbe);
    }
    */
    //-----攻撃パターンⅠ-----
    void EnemyAttackController1()
    {
        Invoke("Attack1", 3f);
        Invoke("Attack2", 5f);
        Invoke("Attack3", 8f);
        //Invoke("AttackLoop", 10f);
        Invoke("Attack4", 11f);
        Invoke("Attack5", 16f);
        Invoke("AttackLoop", 33f);
    }

    //-----攻撃パターンⅡ-----
    void EnemyAttackController2()
    {
        CancelInvoke("AttackLoop");
        /*
        Invoke("Attack4", 3f);
        Invoke("Attack5", 7f);
        Invoke("AttackLoop2", 22f);
        */
        Invoke("Attack6", 3f);
        Invoke("AttackLoop2", 8f);
    }

    //-----攻撃パターンⅢ-----
    void EnemyAttackController3()
    {
        CancelInvoke("AttackLoop2");
        Invoke("Attack6", 3f);
        Invoke("AttackLoop3", 8f);
    }

    //-----攻撃パターンⅠループ-----
    void AttackLoop()
    {
        StartCoroutine(AttackLoopCoroutine());//ループ突入
    }

    //-----攻撃パターンⅠループ脱出条件-----
    IEnumerator AttackLoopCoroutine()
    {
        //ループ脱出条件
        while (enemyhpscripts.CurrentHP > 750)//敵のHP条件
        {
            //attack123 = Random.Range(0, 99);//ランダムで攻撃分岐
            attack12345 = Random.Range(0, 99);//ランダムで攻撃分岐
            Attackrnd();//攻撃パターンⅠ

            yield return new WaitForSeconds(2f);//2秒ごとにループする
        }

        //Debug.Log("攻撃追加");
        EnemyAttackController2();//攻撃パターンⅡ突入
    }

    //-----攻撃パターンⅠ分岐-----
    void Attackrnd()
    {
        /*
        if (attack123 <=33)
        {
            Attack1();//攻撃Ⅰ
        }
        else if (attack123 <=66)
        {
            Attack2();//攻撃Ⅱ
        }
        else
        {
            Attack3();//攻撃Ⅲ
        }
        */
        if (attack12345 <= 20)
        {
            Attack1();//攻撃Ⅰ
        }
        else if (attack12345 <= 40)
        {
            Attack2();//攻撃Ⅱ
        }
        else if (attack12345 <= 60)
        {
            Attack3();//攻撃Ⅲ
        }
        else if (attack12345 <= 80)
        {
            Attack4();//攻撃Ⅳ
        }
        else
        {
            Attack5();//攻撃Ⅴ
        }
    }

    //-----攻撃パターンⅡループ-----
    void AttackLoop2()
    {
        StartCoroutine(AttackLoop2Coroutine());//ループ突入
    }

    //-----攻撃パターンⅡループ脱出条件-----
    IEnumerator AttackLoop2Coroutine()
    {
        while (enemyhpscripts.CurrentHP > 500)//敵のHP条件
        {
            attack123456 = Random.Range(0, 99);//ランダムで攻撃分岐
            Attackrndv2();//攻撃パターンⅡ

            yield return new WaitForSeconds(1.5f);//2秒ごとにループする
        }

        //Debug.Log("攻撃追加Ⅱ");
        EnemyAttackController3();//攻撃パターンⅢ突入
    }

    //-----攻撃パターンⅡ分岐-----
    void Attackrndv2()
    {
        /*
        if (attack12345 <= 20)
        {
            Attack1();//攻撃Ⅰ
        }
        else if (attack12345 <= 40)
        {
            Attack2();//攻撃Ⅱ
        }
        else if (attack12345 <= 60)
        {
            Attack3();//攻撃Ⅲ
        }
        else if (attack12345 <= 80)
        {
            Attack4();//攻撃Ⅳ
        }
        else
        {
            Attack5();//攻撃Ⅴ
        }
        */
        if (attack123456 <= 16)
        {
            Attack1();//攻撃Ⅰ
        }
        else if (attack123456 <= 32)
        {
            Attack2();//攻撃Ⅱ
        }
        else if (attack123456 <= 48)
        {
            Attack3();//攻撃Ⅲ
        }
        else if (attack123456 <= 64)
        {
            Attack4();//攻撃Ⅳ
        }
        else if (attack123456 <= 80)
        {
            Attack5();//攻撃Ⅴ
        }
        else
        {
            Attack6();//攻撃Ⅵ
        }
    }

    //-----攻撃パターンⅢループ-----
    void AttackLoop3()
    {
        StartCoroutine(AttackLoop3Coroutine());//ループ突入
    }

    //-----攻撃パターンⅢループ脱出条件-----
    IEnumerator AttackLoop3Coroutine()
    {
        while (enemyhpscripts.CurrentHP > 250)//敵のHP条件
        {
            attack123456 = Random.Range(0, 99);//ランダムで攻撃分岐
            Attackrndv3();//攻撃パターンⅢ

            yield return new WaitForSeconds(1f);//2秒ごとにループする
        }

        //Debug.Log("攻撃追加");
        EnemyAttackController3();//攻撃パターン　突入
    }

    //-----攻撃パターンⅢ分岐-----
    void Attackrndv3()
    {
        if (attack123456 <= 16)
        {
            Attack1();//攻撃Ⅰ
        }
        else if (attack123456 <= 32)
        {
            Attack2();//攻撃Ⅱ
        }
        else if (attack123456 <= 48)
        {
            Attack3();//攻撃Ⅲ
        }
        else if (attack123456 <= 64)
        {
            Attack4();//攻撃Ⅳ
        }
        else if (attack123456 <= 80)
        {
            Attack5();//攻撃Ⅴ
        }
        else
        {
            Attack6();//攻撃Ⅵ
        }
    }

    //-----攻撃Ⅰ-----
    void Attack1()
    {
        for (int i = 0; i < attack1missile; i++)
        {
            GameObject objm1 = Get();

            groundx = Random.Range(rndm, rndp);//地面の広さによって変更
            groundz = Random.Range(rndm, rndp);//地面の広さによって変更

            /*
            Instantiate(missile, new Vector3(x, attackpointy, attackpointz - i * attackf), Quaternion.identity);//発射
            Instantiate(attackpoint, new Vector3(x, 0, attackpointz - i * attackf), Quaternion.identity);//攻撃範囲
            */

            //Instantiate(missile, new Vector3((attackf * groundx) - groundx, attackpointy, (attackf * groundz) - groundz),  Quaternion.Euler(180, 0, 0));//発射
            /*
            Rigidbody missileRigidbody = missile.GetComponent<Rigidbody>();//リジッドボディ
            missileRigidbody.useGravity = false;
            missileRigidbody.linearVelocity = Vector3.down * missilespeed;
            */
            //Instantiate(attackpoint, new Vector3((attackf * x) - x, 0, (attackf * z) - z), Quaternion.identity);//攻撃範囲
            objm1.transform.position = new Vector3((attackf * groundx) - groundx, attackpointy, (attackf * groundz) - groundz);
            objm1.transform.rotation = Quaternion.Euler(180, 0, 0);

            objm1.SetActive(true);

        }
        //Debug.Log("攻撃Ⅰ");
    }

    //-----攻撃Ⅱ-----
    void Attack2()
    {
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

        GameObject lazer2 = Getl();

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

        Returnl(lazer2);
    }


    //-----攻撃Ⅲ-----
    void Attack3()
    {
        attackbunki = Random.Range(0f, 1f);//攻撃分岐

        if (attackbunki < 0.5f)//クロス型
        {
            GameObject objm3 = Get();
            objm3.transform.position = new Vector3(0, attackpointy, 0);
            objm3.transform.rotation = Quaternion.Euler(180, 0, 0);
            //Instantiate(missile, new Vector3(0, attackpointy, 0), Quaternion.Euler(180, 0, 0));//中心地点発射
            for (int i = 1; i <= attack3missilex; i++)//クロスになるように繰り返す
            {
                /*
                Instantiate(missile, new Vector3(attackpointx - i * attackf, attackpointy, attackpointz - i * attackf), Quaternion.identity);
                //Instantiate(attackpoint, new Vector3(attackpointx - i * attackf, 0, attackpointz - i * attackf), Quaternion.identity);
                Instantiate(missile, new Vector3(attackpointx - i * attackf, attackpointy, attackpointz + i * attackf), Quaternion.identity);
                //Instantiate(attackpoint, new Vector3(attackpointx - i * attackf, 0, attackpointz + i * attackf), Quaternion.identity);
                Instantiate(missile, new Vector3(attackpointx + i * attackf, attackpointy, attackpointz - i * attackf), Quaternion.identity);
                //Instantiate(attackpoint, new Vector3(attackpointx + i * attackf, 0, attackpointz - i * attackf), Quaternion.identity);
                Instantiate(missile, new Vector3(attackpointx + i * attackf, attackpointy, attackpointz + i * attackf), Quaternion.identity);
                //Instantiate(attackpoint, new Vector3(attackpointx + i * attackf, 0, attackpointz + i * attackf), Quaternion.identity);
                */

                /*
                Instantiate(missile, new Vector3(attackpointx * i, attackpointy, -attackpointz * i), Quaternion.Euler(180, 0, 0));
                Instantiate(missile, new Vector3(-attackpointx * i, attackpointy, attackpointz * i), Quaternion.Euler(180, 0, 0));
                Instantiate(missile, new Vector3(attackpointx * i, attackpointy, attackpointz * i), Quaternion.Euler(180, 0, 0));
                Instantiate(missile, new Vector3(-attackpointx * i, attackpointy, -attackpointz * i), Quaternion.Euler(180, 0, 0));
                */
                GameObject m3ldx = Get();
                m3ldx.transform.position = new Vector3(attackpointx * i, attackpointy, -attackpointz * i);//　左下
                m3ldx.transform.rotation = Quaternion.Euler(180, 0, 0);
                m3ldx.SetActive(true);
                GameObject m3rdx = Get();
                m3rdx.transform.position = new Vector3(-attackpointx * i, attackpointy, attackpointz * i);//　右上
                m3rdx.transform.rotation = Quaternion.Euler(180, 0, 0);
                m3rdx.SetActive(true);
                GameObject m3lux = Get();
                m3lux.transform.position = new Vector3(attackpointx * i, attackpointy, attackpointz * i);//　左上
                m3lux.transform.rotation = Quaternion.Euler(180, 0, 0);
                m3lux.SetActive(true);
                GameObject m3rux = Get();
                m3rux.transform.position = new Vector3(-attackpointx * i, attackpointy, -attackpointz * i);//　右下
                m3rux.transform.rotation = Quaternion.Euler(180, 0, 0);
                m3rux.SetActive(true);
            }
            //Debug.Log("攻撃Ⅲx");
        }
        else//十字型
        {
            GameObject objm3 = Get();
            objm3.transform.position = new Vector3(0, attackpointy, 0);
            objm3.transform.rotation = Quaternion.Euler(180, 0, 0);
            //Instantiate(missile, new Vector3(0, attackpointy, 0), Quaternion.Euler(180, 0, 0));//中心地点発射
            for (int i = 1; i < attack3missiley; i++)//十字になるように繰り返す
            {
                /*
                Instantiate(missile, new Vector3(0, attackpointy, attackpointz3 - i * attackf), Quaternion.identity);
                //Instantiate(attackpoint, new Vector3(0, 0, attackpointz3ty - i * attackf), Quaternion.identity);
                Instantiate(missile, new Vector3(attackpointx3 - i * attackf, attackpointy, 0), Quaternion.identity);
                //Instantiate(attackpoint, new Vector3(attackpointx3ty - i * attackf, 0, 0), Quaternion.identity);
                */
                /*
                Instantiate(missile, new Vector3(0 , attackpointy, -attackpointz * i), Quaternion.Euler(180, 0, 0));
                Instantiate(missile, new Vector3(-attackpointx * i, attackpointy, 0), Quaternion.Euler(180, 0, 0));
                Instantiate(missile, new Vector3(0, attackpointy, attackpointz * i), Quaternion.Euler(180, 0, 0));
                Instantiate(missile, new Vector3(attackpointx * i, attackpointy, 0), Quaternion.Euler(180, 0, 0));
                */
                GameObject m3d = Get();
                m3d.transform.position = new Vector3(0, attackpointy, -attackpointz * i);//南
                m3d.transform.rotation = Quaternion.Euler(180, 0, 0);
                m3d.SetActive(true);
                GameObject m3l = Get();
                m3l.transform.position = new Vector3(-attackpointx * i, attackpointy, 0);//西
                m3l.transform.rotation = Quaternion.Euler(180, 0, 0);
                m3l.SetActive(true);
                GameObject m3u = Get();
                m3u.transform.position = new Vector3(0, attackpointy, attackpointz * i);//北
                m3u.transform.rotation = Quaternion.Euler(180, 0, 0);
                m3u.SetActive(true);
                GameObject m3r = Get();
                m3r.transform.position = new Vector3(attackpointx * i, attackpointy, 0);//東
                m3r.transform.rotation = Quaternion.Euler(180, 0, 0);
                m3r.SetActive(true);

            }
            //Debug.Log("攻撃Ⅲ+");
        }
        //Debug.Log("攻撃Ⅲ");
    }

    //-----攻撃Ⅳ-----

    void Attack4()
    {
        ap = Random.Range(rndm, rndp);//地面の広さによって変更
        /*
        GameObject objm4 = Getm();
        objm4.transform.position = new Vector3(ap, attackpointy, ap);
        objm4.transform.rotation = Quaternion.Euler(180, 0, 0);
        objm4.SetActive(true);*/

        Instantiate(missile4, new Vector3(ap, attackpointy, ap), Quaternion.Euler(180, 0, 0));//初弾
        Invoke("Attack4b", 1f);
    }

    //攻撃Ⅳクラスター

    void Attack4b()
    {
        for (int i = 1; i < attack4missile; i++)
        {
            /*
            GameObject objm4bu = Getb();
            objm4bu.transform.position = new Vector3(ap, 0, ap + 10 * i);//北
            objm4bu.transform.rotation = Quaternion.Euler(180, 0, 0);
            objm4bu.SetActive(true);
            GameObject objm4br = Getb();
            objm4br.transform.position = new Vector3(ap + 10 * i, 0, ap);//東
            objm4br.transform.rotation = Quaternion.Euler(180, 0, 0);
            objm4br.SetActive(true);
            GameObject objm4bd = Getb();
            objm4bd.transform.position = new Vector3(ap, 0, ap - 10 * i);//南
            objm4bd.transform.rotation = Quaternion.Euler(180, 0, 0);
            objm4bd.SetActive(true);
            GameObject objm4bl = Getb();
            objm4bl.transform.position = new Vector3(ap - 10 * i, 0, ap);//西
            objm4bl.transform.rotation = Quaternion.Euler(180, 0, 0);
            objm4bl.SetActive(true);*/

            GameObject Attack4bpoint1 = Instantiate(bpoint, new Vector3(ap, 0, ap + 10 * i), Quaternion.Euler(180, 0, 0));
            GameObject Attack4bpoint2 = Instantiate(bpoint, new Vector3(ap + 10 * i, 0, ap), Quaternion.Euler(180, 0, 0));//東
            GameObject Attack4bpoint3 = Instantiate(bpoint, new Vector3(ap, 0, ap - 10 * i), Quaternion.Euler(180, 0, 0));//南
            GameObject Attack4bpoint4 = Instantiate(bpoint, new Vector3(ap - 10 * i, 0, ap), Quaternion.Euler(180, 0, 0));//西
            Destroy(Attack4bpoint1, 1f);
            Destroy(Attack4bpoint2, 1f);
            Destroy(Attack4bpoint3, 1f);
            Destroy(Attack4bpoint4, 1f);
        }
        Invoke("Attack4Cluster", 1f);
    }
    void Attack4Cluster()
    {
        for (int i = 1; i < attack4missile; i++)
        {
            /*
            GameObject objm4beu = Getbe();
            objm4beu.transform.position = new Vector3(ap, 0, ap + 10 * i);//北
            objm4beu.transform.rotation = Quaternion.Euler(180, 0, 0);
            objm4beu.SetActive(true);
            GameObject objm4ber = Getbe();
            objm4ber.transform.position = new Vector3(ap + 10 * i, 0, ap);//東
            objm4ber.transform.rotation = Quaternion.Euler(180, 0, 0);
            objm4ber.SetActive(true);
            GameObject objm4bed = Getbe();
            objm4bed.transform.position = new Vector3(ap, 0, ap - 10 * i);//南
            objm4bed.transform.rotation = Quaternion.Euler(180, 0, 0);
            objm4bed.SetActive(true);
            GameObject objm4bel = Getbe();
            objm4bel.transform.position = new Vector3(ap - 10 * i, 0, ap);//西
            objm4bel.transform.rotation = Quaternion.Euler(180, 0, 0);
            objm4bel.SetActive(true);
            */

            GameObject Attack4effectbpoint1 = Instantiate(ClustereffectPrefab, new Vector3(ap, 0, ap + 10 * i), Quaternion.identity);//北
            GameObject Attack4effectbpoint2 = Instantiate(ClustereffectPrefab, new Vector3(ap + 10 * i, 0, ap), Quaternion.identity);//東
            GameObject Attack4effectbpoint3 = Instantiate(ClustereffectPrefab, new Vector3(ap, 0, ap - 10 * i), Quaternion.identity);//南
            GameObject Attack4effectbpoint4 = Instantiate(ClustereffectPrefab, new Vector3(ap - 10 * i, 0, ap), Quaternion.identity);//西
            Destroy(Attack4effectbpoint1, 2f);
            Destroy(Attack4effectbpoint2, 2f);
            Destroy(Attack4effectbpoint3, 2f);
            Destroy(Attack4effectbpoint4, 2f);

        }
        //Debug.Log("攻撃Ⅳ");
    }

    //-----攻撃Ⅴ-----
    void Attack5()
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
        GameObject lazerObj = Getl();
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
        GameObject lazerObjx = Getlx();
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

    //-----攻撃Ⅵ-----
    void Attack6()
    {
        StartCoroutine(Attack6missileCoroutine());//追尾攻撃スタート
        attackbunki = Random.Range(0, 1);//レーザー攻撃分岐
        if (attackbunki < 0.5f)
        {
            StartCoroutine(Attack6lazer1Coroutine());//レーザー攻撃パターンⅠ
        }
        else
        {
            StartCoroutine(Attack6lazer2Coroutine());//レーザー攻撃パターンⅡ
        }

        //Debug.Log("攻撃Ⅵ");
    }

    //攻撃Ⅵ連続追尾ミサイル
    IEnumerator Attack6missileCoroutine()
    {
        int i = 0;
        while (i < Attack6ms)//追尾ミサイル上限数分繰り返す
        {
            Attack6missile();//攻撃Ⅵミサイル
            i++;
            yield return new WaitForSeconds(1.2f);
        }
        //Debug.Log("攻撃Ⅵ missile");
    }

    //攻撃ⅥレーザーパターンⅠ
    IEnumerator Attack6lazer1Coroutine()
    {
        int i = 0;
        while (i < 1)
        {
            Attack6lazerppoint();
            i++;
            yield return new WaitForSeconds(2f);
        }
    }

    //攻撃ⅥレーザーパターンⅡ

    IEnumerator Attack6lazer2Coroutine()
    {
        int i = 0;
        while (i < 1)
        {
            Attack6lazermpoint();
            i++;
            yield return new WaitForSeconds(2f);
        }
    }

    //攻撃Ⅵ追尾ミサイル
    void Attack6missile()
    {
        GameObject objm6 = Get();
        // transform.position で現在のワールド座標を取得
        Vector3 currentPosition = transform.position;
        //Debug.Log("プレイヤーの座標: " + currentPosition);

        // x, y, z 座標を個別に取得
        float x = currentPosition.x;
        float z = currentPosition.z;
        //Debug.Log("X座標: " + x + ", Z座標: " + z);

        Vector3 play = GameObject.Find("Player").transform.position;//プレイヤーの座標取得
        objm6.transform.position = new Vector3(play.x, y, play.z);
        objm6.transform.rotation = Quaternion.Euler(180, 0, 0);
        /*
        Instantiate(missile, new Vector3(play.x, y, play.z), Quaternion.Euler(180, 0, 0));//プレイヤーのいる座標に向かって発射
        */

        //Instantiate(missile, new Vector3(x,y,z), Quaternion.identity);
    }

    //-----攻撃ⅥパターンⅠ-----

    //攻撃ⅥレーザーポイントパターンⅠ右

    void Attack6lazerppoint()
    {
        audioSource.PlayOneShot(lazercharge);
        GameObject Attack6lazerattackpointp = Instantiate(lazerattackpoint, new Vector3(30, 0, 0), Quaternion.identity);
        GameObject Attacklazerchargeeffect = Instantiate(lazerchargeeffect, new Vector3(30, lazerpointy, attack2lazerz), Quaternion.identity);
        Destroy(Attack6lazerattackpointp, 1.3f);
        Destroy(Attacklazerchargeeffect, 2f);
        Invoke("Attack6lazerp", 1.3f);
    }

    //攻撃ⅥレーザーパターンⅠ右
    void Attack6lazerp()
    {
        /*
        audioSource.PlayOneShot(lazerclip);
        GameObject Attack6lazerp = Instantiate(lazer, new Vector3(30, lazerpointy, attack2lazerz), Quaternion.identity);//発射
        StartCoroutine(ExtendLazer6p1(Attack6lazerp));
        yield return new WaitForSeconds(1.3f);
        */
        audioSource.PlayOneShot(lazerclip);

        GameObject lazer6p1 = Getl();

        lazer6p1.transform.position = new Vector3(30, lazerpointy, attack2lazerz);

        StartCoroutine(ExtendLazer6p1(lazer6p1));
    }
    IEnumerator ExtendLazer6p1(GameObject lazer6p1)
    {
        Vector3 scale = lazer6p1.transform.localScale;
        scale.z = 0f; // 最初は長さ0
        lazer6p1.transform.localScale = scale;
        while (scale.z > maxLength)
        {
            scale.z -= extendSpeed * Time.deltaTime;
            lazer6p1.transform.localScale = scale;

            yield return null; // 次のフレームへ
        }
        yield return new WaitForSeconds(1f);

        Returnl(lazer6p1);
        Invoke("Attack6lazerm2point", 2f);
    }

    //攻撃ⅥレーザーポイントパターンⅠ左
    void Attack6lazerm2point()
    {
        audioSource.PlayOneShot(lazercharge);
        GameObject Attack6lazerattackpointm2 = Instantiate(lazerattackpoint, new Vector3(-30, 0, 0), Quaternion.identity);
        GameObject Attacklazerchargeeffect = Instantiate(lazerchargeeffect, new Vector3(-30, lazerpointy, attack2lazerz), Quaternion.identity);
        Destroy(Attack6lazerattackpointm2, 1.3f);
        Destroy(Attacklazerchargeeffect, 2f);
        Invoke("Attack6lazerm2", 1.3f);
    }

    //攻撃ⅥレーザーパターンⅠ左
    void Attack6lazerm2()
    {
        /*
        audioSource.PlayOneShot(lazerclip);
        GameObject Attack6lazerm2 = Instantiate(lazer, new Vector3(-30, lazerpointy, attack2lazerz), Quaternion.identity);//発射
        StartCoroutine(ExtendLazer6m2(Attack6lazerm2));
        Destroy(Attack6lazerm2, 1f);
        */
        audioSource.PlayOneShot(lazerclip);
        GameObject lazer6m2 = Getl();

        lazer6m2.transform.position = new Vector3(-30, lazerpointy, attack2lazerz);

        StartCoroutine(ExtendLazer6m2(lazer6m2));
    }
    IEnumerator ExtendLazer6m2(GameObject lazer6m2)
    {
        Vector3 scale = lazer6m2.transform.localScale;
        scale.z = 0f; // 最初は長さ0
        lazer6m2.transform.localScale = scale;
        while (scale.z > maxLength)
        {
            scale.z -= extendSpeed * Time.deltaTime;
            lazer6m2.transform.localScale = scale;

            yield return null; // 次のフレームへ
        }
        yield return new WaitForSeconds(1f);

        Returnl(lazer6m2);
    }

    //-----攻撃ⅥパターンⅡ-----
    //攻撃ⅥレーザーポイントパターンⅡ左
    void Attack6lazermpoint()
    {
        audioSource.PlayOneShot(lazercharge);
        GameObject Attack6lazerattackpointm = Instantiate(lazerattackpoint, new Vector3(-30, 0, 0), Quaternion.identity);
        GameObject Attacklazerchargeeffect = Instantiate(lazerchargeeffect, new Vector3(-30, lazerpointy, attack2lazerz), Quaternion.identity);
        Destroy(Attack6lazerattackpointm, 1.3f);
        Destroy(Attacklazerchargeeffect, 2f);
        Invoke("Attack6lazerm", 1.3f);
    }

    //攻撃ⅥレーザーパターンⅡ左
    void Attack6lazerm()
    {/*
        audioSource.PlayOneShot(lazerclip);
        GameObject Attack6lazerm = Instantiate(lazer, new Vector3(-30, lazerpointy, attack2lazerz), Quaternion.identity);//発射
        StartCoroutine(ExtendLazer6m1(Attack6lazerm));*/
        audioSource.PlayOneShot(lazerclip);
        GameObject lazer6m1 = Getl();

        lazer6m1.transform.position = new Vector3(-30, lazerpointy, attack2lazerz);

        StartCoroutine(ExtendLazer6m1(lazer6m1));
    }
    IEnumerator ExtendLazer6m1(GameObject lazer6m1)
    {
        Vector3 scale = lazer6m1.transform.localScale;
        scale.z = 0f; // 最初は長さ0
        lazer6m1.transform.localScale = scale;

        while (scale.z > maxLength)
        {
            scale.z -= extendSpeed * Time.deltaTime;
            lazer6m1.transform.localScale = scale;

            yield return null; // 次のフレームへ
        }
        yield return new WaitForSeconds(1f);

        Returnl(lazer6m1);
        Invoke("Attack6lazerp2point", 2f);
    }

    //攻撃ⅥレーザーポイントパターンⅡ右

    void Attack6lazerp2point()
    {
        audioSource.PlayOneShot(lazercharge);
        GameObject Attack6lazerattackpointp2 = Instantiate(lazerattackpoint, new Vector3(30, 0, 0), Quaternion.identity);
        GameObject Attacklazerchargeeffect = Instantiate(lazerchargeeffect, new Vector3(30, lazerpointy, attack2lazerz), Quaternion.identity);
        Destroy(Attack6lazerattackpointp2, 1.3f);
        Destroy(Attacklazerchargeeffect, 2f);
        Invoke("Attack6lazerp2", 1.3f);
    }

    //攻撃ⅥレーザーパターンⅡ右
    void Attack6lazerp2()
    {
        /*
        audioSource.PlayOneShot(lazerclip);
        GameObject Attack6lazerp2 = Instantiate(lazer, new Vector3(30, lazerpointy, attack2lazerz), Quaternion.identity);//発射
        StartCoroutine(ExtendLazer6p2(Attack6lazerp2));
        //Debug.Log("攻撃Ⅵ パターン2");*/
        audioSource.PlayOneShot(lazerclip);

        GameObject lazer6p2 = Getl();

        lazer6p2.transform.position = new Vector3(30, lazerpointy, attack2lazerz);

        StartCoroutine(ExtendLazer6p2(lazer6p2));
    }
    IEnumerator ExtendLazer6p2(GameObject lazer6p2)
    {
        Vector3 scale = lazer6p2.transform.localScale;
        scale.z = 0f; // 最初は長さ0
        lazer6p2.transform.localScale = scale;

        while (scale.z > maxLength)
        {
            scale.z -= extendSpeed * Time.deltaTime;
            lazer6p2.transform.localScale = scale;

            yield return null; // 次のフレームへ
        }
        yield return new WaitForSeconds(1f);

        Returnl(lazer6p2);
    }
}
