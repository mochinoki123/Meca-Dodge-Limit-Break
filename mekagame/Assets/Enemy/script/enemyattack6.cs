using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;


public class enemyattack6 : MonoBehaviour
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
    //攻撃６
    [Header("攻撃Ⅵ")]
    [SerializeField] int Attack6ms = 5;//攻撃６のミサイル数
    public float y = 60;//攻撃発生高
    Vector3 play;
    [Header("レーザー座標指定・レーザー長指定")]
    [SerializeField] int lazerpointy = 7;
    [SerializeField] float maxLength = -50f;   // 最終的な長さ
    [SerializeField] float maxLengthx = 50f;   // 最終的な長さ
    [SerializeField] float extendSpeed = 100f;  // 伸びるスピード
    //プレイヤー座標取得
    float x;
    float z;
    float attackbunki;//random値確認用基本使わない
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        enemyLazer = FindAnyObjectByType<enemylazer>();
        enemymanager = GetComponent<enemyattack>();
        Invoke("Attack6", 30f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //-----攻撃Ⅵ-----
    public void Attack6()
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
        GameObject objm6 = enemymanager.Get();
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

        GameObject lazer6p1 = enemymanager.Getl();

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

        enemymanager.Returnl(lazer6p1);
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
        GameObject lazer6m2 = enemymanager.Getl();

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

        enemymanager.Returnl(lazer6m2);
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
        GameObject lazer6m1 = enemymanager.Getl();

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

        enemymanager.Returnl(lazer6m1);
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

        GameObject lazer6p2 = enemymanager.Getl();

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

        enemymanager.Returnl(lazer6p2);
    }
}
