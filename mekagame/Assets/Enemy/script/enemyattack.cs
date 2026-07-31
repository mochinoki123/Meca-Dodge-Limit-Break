using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Pool;
using UnityEngine.Rendering;
using static UnityEngine.Rendering.ObjectPool<T>;

public class enemyattack : MonoBehaviour
{
    //Enemyスクリプト
    private Enemy enemyhpscripts;
    enemyattack1 a1;
    enemyattack2 a2;
    enemyattack3 a3;
    enemyattack4 a4;
    enemyattack5 a5;
    enemyattack6 a6;

    #region オブジェクトプール
    [Header("プール数")]
    //ミサイル
    public int sizem = 50;
    private Queue<GameObject> missilepool = new Queue<GameObject>();
    //縦レーザー
    public int sizel = 20;
    private Queue<GameObject> lazerpool = new Queue<GameObject>();
    //横レーザー
    public int sizelx = 20;
    private Queue<GameObject> lazerxpool = new Queue<GameObject>();
    //攻撃Ⅳミサイル
    public int sizem2 = 10;
    private Queue<GameObject> missile2pool = new Queue<GameObject>();


    //ポイント
    public int sizep = 50;
    private Queue<GameObject> pointpool = new Queue<GameObject>();
    /*
    //爆発エフェクト
    public int sizebe = 50;
    private Queue<GameObject> beffectpool = new Queue<GameObject>();
    */
    #endregion
    [SerializeField] GameObject missile;//ミサイル攻撃のオブジェクト
    [SerializeField] GameObject missile4;//ミサイル攻撃のオブジェクト
    [SerializeField] GameObject lazer;//レーザーオブジェクト
    [SerializeField] GameObject lazerz;//レーザーオブジェクト

    int attack12345;//random値確認用基本使わない
    int attack123456;//random値確認用基本使わない
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyhpscripts = GetComponent<Enemy>();//敵データ呼び出し
        //audioSource = GetComponent<AudioSource>();
        //enemyLazer = FindAnyObjectByType<enemylazer>();
        a1 = FindAnyObjectByType<enemyattack1>();
        a2 = FindAnyObjectByType<enemyattack2>();
        a3 = FindAnyObjectByType<enemyattack3>();
        a4 = FindAnyObjectByType<enemyattack4>();
        a5 = FindAnyObjectByType<enemyattack5>();
        a6 = FindAnyObjectByType<enemyattack6>();
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
            GameObject objm1 = Instantiate(missile);
            objm1.SetActive(false);
            missilepool.Enqueue(objm1);
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

        for (int i = 0; i < sizem2; i++)
        {
            GameObject objm2 = Instantiate(missile4);
            objm2.SetActive(false);
            missile2pool.Enqueue(objm2);
        }
        
        EnemyAttackController1();//攻撃パターンⅠ
    }

    public GameObject Getm()
    {
        if (missilepool.Count > 0)
        {
            GameObject objm1 = missilepool.Dequeue();
            objm1.SetActive(true);
            return objm1;
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
    public GameObject Getm1()
    {
        if (missile2pool.Count > 0)
        {
            GameObject objm2 = missile2pool.Dequeue();
            objm2.SetActive(true);
            return objm2;
        }
        return Instantiate(missile4);
    }
    
    public void Return(GameObject objm1)
    {
        objm1.SetActive(false);
        missilepool.Enqueue(objm1);
        Debug.Log(objm1.name);
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

    public void Returnm(GameObject objm2)
    {
        objm2.SetActive(false);
        missile2pool.Enqueue(objm2);
    }
    
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
        
        if (attack12345 <= 20)
        {
            a1.Attack1();//攻撃Ⅰ
        }
        else if (attack12345 <= 40)
        {
            a2.Attack2();//攻撃Ⅱ
        }
        else if (attack12345 <= 60)
        {
            a3.Attack3();//攻撃Ⅲ
        }
        else if (attack12345 <= 80)
        {
            a4.Attack4();//攻撃Ⅳ
        }
        else
        {
            a5.Attack5();//攻撃Ⅴ
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
        
        if (attack123456 <= 16)
        {
            a1.Attack1();//攻撃Ⅰ
        }
        else if (attack123456 <= 32)
        {
            a2.Attack2();//攻撃Ⅱ
        }
        else if (attack123456 <= 48)
        {
            a3.Attack3();//攻撃Ⅲ
        }
        else if (attack123456 <= 64)
        {
            a4.Attack4();//攻撃Ⅳ
        }
        else if (attack123456 <= 80)
        {
            a5.Attack5();//攻撃Ⅴ
        }
        else
        {
            a6.Attack6();//攻撃Ⅵ
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
            a1.Attack1();//攻撃Ⅰ
        }
        else if (attack123456 <= 32)
        {
            a2.Attack2();//攻撃Ⅱ
        }
        else if (attack123456 <= 48)
        {
            a3.Attack3();//攻撃Ⅲ
        }
        else if (attack123456 <= 64)
        {
            a4.Attack4();//攻撃Ⅳ
        }
        else if (attack123456 <= 80)
        {
            a5.Attack5();//攻撃Ⅴ
        }
        else
        {
            a6.Attack6();//攻撃Ⅵ
        }
    }
}