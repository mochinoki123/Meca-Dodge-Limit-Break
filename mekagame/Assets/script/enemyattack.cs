using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using System.Data;

public class enemyattack : MonoBehaviour
{
    //Enemyスクリプト
    private Enemy enemyhpscripts;
    [SerializeField] private AudioClip lazerclip;
    [SerializeField] private AudioClip lazercharge;
    private AudioSource audioSource;
    //プレハブ
    [SerializeField] GameObject missile;//ミサイル攻撃のオブジェクト
    //[SerializeField] GameObject attackpoint;//攻撃発生地点
    //フィールド範囲
    [SerializeField] float rndm = -9;//フィールドごとの範囲指定マイナス
    [SerializeField] float rndp =  9;//フィールドごとの範囲指定プラス
    Vector3 play;

    //public float missilespeed = 10;

    //攻撃１
    [SerializeField] int attack1missile;//攻撃１のミサイル数　6
    //攻撃２
    [SerializeField] GameObject lazer;//レーザーオブジェクト
    [SerializeField] GameObject lazerchargeeffect;//レーザーチャージエフェクト
    [SerializeField] GameObject lazerattackpoint;//レーザー発生ポイントオブジェクト
    [SerializeField] int attack2lazerz;//50
    //攻撃３
    [SerializeField] int attack3missilex;//攻撃Ⅲxの範囲設定　10
    [SerializeField] int attack3missiley;//攻撃Ⅲ+の範囲設定　10
  //[SerializeField] int attackpointx3;//もしも用　使ってない
  //[SerializeField] int attackpointz3;//もしも用　使ってない
    public float attackbunki;//random値確認用基本使わない
    //攻撃４
    [SerializeField] int attack4missile;//攻撃４のミサイル範囲指定　10
    [SerializeField] GameObject bpoint;//爆発ポイント
    [SerializeField] GameObject ClustereffectPrefab;//爆発のエフェクト
    //攻撃５
    [SerializeField] GameObject lazerx;//レーザーオブジェクト
    [SerializeField] int Attack5ls;//攻撃５のレーザー数 10
    [SerializeField] GameObject lazerattackpointx;//レーザー発生ポイントオブジェクト
    [SerializeField] public float l5x = 60;//ｘ攻撃開始地点・範囲
    [SerializeField] public float l5z = 50;//ｚ攻撃開始地点・範囲
    [SerializeField] public float k;//13 攻撃数
    [SerializeField] int attack5lx;//-100
    //攻撃６
    [SerializeField] int Attack6ms;//攻撃６のミサイル数 5
    //攻撃座標関係
    [SerializeField] int attackf;//攻撃の間隔 5
    [SerializeField] int attackpointx;//攻撃発生の横 10
    [SerializeField] int attackpointy;//攻撃発生の高さ 25
    [SerializeField] int attackpointz;//攻撃発生の奥行 10
    public float ap;//random値確認用基本使わない
    public float groundx;//random値確認用基本使わない
    public float groundz;//random値確認用基本使わない
    //レーザーy座標関係
    [SerializeField] int lazerpointy = 7; // 7
    [SerializeField] float maxLength = -70f;   // 最終的な長さ
    [SerializeField] float extendSpeed = 100;  // 伸びるスピード

    //攻撃分岐関係
    public int attack123;//random値確認用基本使わない
    public int attack12345;//random値確認用基本使わない
    public int attack123456;//random値確認用基本使わない
    //プレイヤー座標取得
    public float x;
    public float y;//攻撃発生高 15
    public float z;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyhpscripts = GetComponent<Enemy>();//敵データ呼び出し
        audioSource = GetComponent<AudioSource>();
        EnemyAttackController1();//攻撃パターンⅠ
    }

    // Update is called once per frame
    void Update()
    {
        
     
    }

    //-----攻撃パターンⅠ-----
    void EnemyAttackController1()
    {
        Invoke("Attack1", 3f);
        Invoke("Attack2", 5f);
        Invoke("Attack3", 8f);
        Invoke("AttackLoop", 10f);
    }

    //-----攻撃パターンⅡ-----
    void EnemyAttackController2()
    {
        CancelInvoke("AttackLoop");
        Invoke("Attack4", 3f);
        Invoke("Attack5", 7f);
        Invoke("AttackLoop2", 22f);
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
        while (enemyhpscripts.EnemyHP > 750)//敵のHP条件
        {
            attack123 = Random.Range(0, 99);//ランダムで攻撃分岐
            Attackrnd();//攻撃パターンⅠ

            yield return new WaitForSeconds(2f);//2秒ごとにループする
        }

        Debug.Log("攻撃追加");
        EnemyAttackController2();//攻撃パターンⅡ突入
    }

    //-----攻撃パターンⅠ分岐-----
    void Attackrnd()
    {
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
    }

    //-----攻撃パターンⅡループ-----
    void AttackLoop2()
    {
        StartCoroutine(AttackLoop2Coroutine());//ループ突入
    }

    //-----攻撃パターンⅡループ脱出条件-----
    IEnumerator AttackLoop2Coroutine()
    {
        while (enemyhpscripts.EnemyHP > 500)//敵のHP条件
        {
            attack12345 = Random.Range(0, 99);//ランダムで攻撃分岐
            Attackrndv2();//攻撃パターンⅡ

            yield return new WaitForSeconds(2f);//2秒ごとにループする
        }

        Debug.Log("攻撃追加Ⅱ");
        EnemyAttackController3();//攻撃パターンⅢ突入
    }

    //-----攻撃パターンⅡ分岐-----
    void Attackrndv2()
    {
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

    //-----攻撃パターンⅢループ-----
    void AttackLoop3()
    {
        StartCoroutine(AttackLoop3Coroutine());//ループ突入
    }

    //-----攻撃パターンⅢループ脱出条件-----
    IEnumerator AttackLoop3Coroutine()
    {
        while (enemyhpscripts.EnemyHP > 250)//敵のHP条件
        {
            attack123456 = Random.Range(0, 99);//ランダムで攻撃分岐
            Attackrndv3();//攻撃パターンⅢ

            yield return new WaitForSeconds(2f);//2秒ごとにループする
        }

        Debug.Log("攻撃追加");
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
            groundx = Random.Range(rndm, rndp);//地面の広さによって変更
            groundz = Random.Range(rndm, rndp);//地面の広さによって変更

            /*
            Instantiate(missile, new Vector3(x, attackpointy, attackpointz - i * attackf), Quaternion.identity);//発射
            Instantiate(attackpoint, new Vector3(x, 0, attackpointz - i * attackf), Quaternion.identity);//攻撃範囲
            */

            Instantiate(missile, new Vector3((attackf * groundx) - groundx, attackpointy, (attackf * groundz) - groundz),  Quaternion.Euler(180, 0, 0));//発射
            /*
            Rigidbody missileRigidbody = missile.GetComponent<Rigidbody>();//リジッドボディ
            missileRigidbody.useGravity = false;
            missileRigidbody.linearVelocity = Vector3.down * missilespeed;*/
            //Instantiate(attackpoint, new Vector3((attackf * x) - x, 0, (attackf * z) - z), Quaternion.identity);//攻撃範囲
        }
        Debug.Log("攻撃Ⅰ");
    }

    //-----攻撃Ⅱ-----
    void Attack2()
    {
        audioSource.PlayOneShot(lazercharge);
        GameObject Attack2lazerattackpoint = Instantiate(lazerattackpoint, new Vector3(groundx, 0, 0), Quaternion.identity);//レーザー発射地点
        GameObject Attacklazerchargeeffect = Instantiate(lazerchargeeffect, new Vector3(groundx, lazerpointy, attack2lazerz), Quaternion.identity); 
        Destroy(Attack2lazerattackpoint, 1.3f);//1.3秒後に破壊
        Destroy(Attacklazerchargeeffect, 2f);//2秒後に破壊
        Invoke("Attack2l", 1.3f);
    }

    //攻撃Ⅱレーザー
    void Attack2l()
    {
        audioSource.PlayOneShot(lazerclip);
        GameObject Attack2lazer = Instantiate(lazer, new Vector3(groundx, lazerpointy, attack2lazerz), Quaternion.identity);//発射
        StartCoroutine(ExtendLazer2(Attack2lazer));
    }

    IEnumerator ExtendLazer2(GameObject Attack2lazer)
    {
        Vector3 scale = Attack2lazer.transform.localScale;
        scale.z = 0f; // 最初は長さ0
        Attack2lazer.transform.localScale = scale;

        while (scale.z > maxLength)
        {
            scale.z -= extendSpeed * Time.deltaTime;
            Attack2lazer.transform.localScale = scale;

            Destroy(Attack2lazer, 1f);
            Debug.Log("攻撃Ⅱ");
            yield return null; // 次のフレームへ
        }

        // 最終値を保証
        scale.z = maxLength;
        Attack2lazer.transform.localScale = scale;
    }

    //-----攻撃Ⅲ-----
    void Attack3()
    {
        attackbunki = Random.Range(0f, 1f);//攻撃分岐

        if (attackbunki < 0.5f)//クロス型
        {
            Instantiate(missile, new Vector3(0, attackpointy, 0), Quaternion.Euler(180, 0, 0));//中心地点発射
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
                Instantiate(missile, new Vector3(attackpointx * i, attackpointy, -attackpointz * i), Quaternion.Euler(180, 0, 0));//　左下
                Instantiate(missile, new Vector3(-attackpointx * i, attackpointy, attackpointz * i), Quaternion.Euler(180, 0, 0));//　右上
                Instantiate(missile, new Vector3(attackpointx * i, attackpointy, attackpointz * i), Quaternion.Euler(180, 0, 0));//　左上
                Instantiate(missile, new Vector3(-attackpointx * i, attackpointy, -attackpointz * i), Quaternion.Euler(180, 0, 0));//　右下
            }
            Debug.Log("攻撃Ⅲx");
        }
        else//十字型
        {
            Instantiate(missile, new Vector3(0, attackpointy, 0), Quaternion.Euler(180, 0, 0));//中心地点発射
            for (int i = 1; i < attack3missiley; i++)//十字になるように繰り返す
            {
                /*
                Instantiate(missile, new Vector3(0, attackpointy, attackpointz3 - i * attackf), Quaternion.identity);
                //Instantiate(attackpoint, new Vector3(0, 0, attackpointz3ty - i * attackf), Quaternion.identity);
                Instantiate(missile, new Vector3(attackpointx3 - i * attackf, attackpointy, 0), Quaternion.identity);
                //Instantiate(attackpoint, new Vector3(attackpointx3ty - i * attackf, 0, 0), Quaternion.identity);
                */
                Instantiate(missile, new Vector3(0 , attackpointy, -attackpointz * i), Quaternion.Euler(180, 0, 0));//南
                Instantiate(missile, new Vector3(-attackpointx * i, attackpointy, 0), Quaternion.Euler(180, 0, 0));//西
                Instantiate(missile, new Vector3(0, attackpointy, attackpointz * i), Quaternion.Euler(180, 0, 0));//北
                Instantiate(missile, new Vector3(attackpointx * i, attackpointy, 0), Quaternion.Euler(180, 0, 0));//東
            }
            Debug.Log("攻撃Ⅲ+");
        }
        Debug.Log("攻撃Ⅲ");
    }

    //-----攻撃Ⅳ-----
    void Attack4()
    {
        ap = Random.Range(rndm, rndp);//地面の広さによって変更
        Instantiate(missile, new Vector3(ap, attackpointy, ap), Quaternion.Euler(180, 0, 0));//初弾
        Invoke("Attack4b", 1.3f);
    }

    //攻撃Ⅳクラスター
    void Attack4b()
    {
        for (int i = 1; i < attack4missile; i++)
        {
            GameObject Attack4bpoint1 = Instantiate(bpoint, new Vector3(ap, 0, ap + 10 * i), Quaternion.Euler(180, 0, 0));//北
            GameObject Attack4bpoint2 = Instantiate(bpoint, new Vector3(ap + 10 * i, 0, ap), Quaternion.Euler(180, 0, 0));//東
            GameObject Attack4bpoint3 = Instantiate(bpoint, new Vector3(ap, 0, ap - 10 * i), Quaternion.Euler(180, 0, 0));//南
            GameObject Attack4bpoint4 = Instantiate(bpoint, new Vector3(ap - 10 * i, 0, ap), Quaternion.Euler(180, 0, 0));//西
            Destroy(Attack4bpoint1, 1.3f);
            Destroy(Attack4bpoint2, 1.3f);
            Destroy(Attack4bpoint3, 1.3f);
            Destroy(Attack4bpoint4, 1.3f);
            Invoke("Attack4Cluster", 1.4f);
        }
        
    }
    void Attack4Cluster()
    {
        for (int i = 1; i < attack4missile; i++)
        {
            GameObject Attack4effectbpoint1 = Instantiate(ClustereffectPrefab, new Vector3(ap, 0, ap + 10 * i), Quaternion.identity);//北
            GameObject Attack4effectbpoint2 = Instantiate(ClustereffectPrefab, new Vector3(ap + 10 * i, 0, ap), Quaternion.identity);//東
            GameObject Attack4effectbpoint3 = Instantiate(ClustereffectPrefab, new Vector3(ap, 0, ap - 10 * i), Quaternion.identity);//南
            GameObject Attack4effectbpoint4 = Instantiate(ClustereffectPrefab, new Vector3(ap - 10 * i, 0, ap), Quaternion.identity);//西
            Destroy(Attack4effectbpoint1, 2f);
            Destroy(Attack4effectbpoint2, 2f);
            Destroy(Attack4effectbpoint3, 2f);
            Destroy(Attack4effectbpoint4, 2f);
        }
        Debug.Log("攻撃Ⅳ");
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
        Debug.Log("攻撃Ⅴ");
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
        Invoke("Attack5lx",2f);
    }

    //攻撃Ⅴ縦レーザー
    void Attack5lx()
    {
        audioSource.PlayOneShot(lazerclip);
        GameObject Attack5lazer = Instantiate(lazer, new Vector3(l5x, lazerpointy, attack2lazerz), Quaternion.identity);//発射
      
        StartCoroutine(ExtendLazer5x(Attack5lazer));
    }
    IEnumerator ExtendLazer5x(GameObject Attack5lazer)
    {
        Vector3 scale = Attack5lazer.transform.localScale;
        scale.z = 0f; // 最初は長さ0
        l5x = l5x - k;//発射地点を横にずらす
        Attack5lazer.transform.localScale = scale;
        while (scale.z > maxLength)
        {
            scale.z -= extendSpeed * Time.deltaTime;
            Attack5lazer.transform.localScale = scale;
            Destroy(Attack5lazer, 1f);
            Debug.Log("攻撃Ⅴx");
            yield return null; // 次のフレームへ
        }
        
        // 最終値を保証
        //scale.z = maxLength;
        Attack5lazer.transform.localScale = scale;
    }

    //攻撃Ⅴ横レーザーポイント
    void Attack5lpz()
    {
        audioSource.PlayOneShot(lazercharge);
        GameObject Attack5lazerattackpointx = Instantiate(lazerattackpointx, new Vector3(0, 0, l5z), Quaternion.identity);//横レーザー発射地点
        GameObject Attacklazerchargeeffect = Instantiate(lazerchargeeffect, new Vector3(attack5lx, lazerpointy, l5z), Quaternion.identity);
        Destroy(Attack5lazerattackpointx, 2f);
        Destroy(Attacklazerchargeeffect, 2f);
        Invoke("Attack5lz", 2f);
    }

    //攻撃Ⅴ横レーザー
    void Attack5lz()
    {
        audioSource.PlayOneShot(lazerclip);
        GameObject Attack5lazerx = Instantiate(lazerx, new Vector3(attack5lx, lazerpointy, l5z), Quaternion.identity);//発射
      //Rigidbody cubeRigidbody = Attack5lazerx.GetComponent<Rigidbody>();
      //cubeRigidbody.AddForce(new Vector3(1, 0, 0) * 10, ForceMode.Impulse);
        StartCoroutine(ExtendLazer5z(Attack5lazerx));
        
        Debug.Log("攻撃Ⅴz");
    }

    IEnumerator ExtendLazer5z(GameObject Attack5lazerx)
    {
        Vector3 scale = Attack5lazerx.transform.localScale;
        scale.x = 0f; // 最初は長さ0
        l5z = l5z - k;//発射地点を縦にずらす
        Attack5lazerx.transform.localScale = scale;
        while (scale.x > maxLength)
        {
            scale.x += extendSpeed * Time.deltaTime;
            Attack5lazerx.transform.localScale = scale;

            Destroy(Attack5lazerx, 1f);

            Debug.Log("攻撃Ⅴx");
            yield return null; // 次のフレームへ
        }
        // 最終値を保証
        //scale.x = maxLength;
        Attack5lazerx.transform.localScale = scale;
    }

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

        Debug.Log("攻撃Ⅵ");
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
        Debug.Log("攻撃Ⅵ missile");
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
        // transform.position で現在のワールド座標を取得
        Vector3 currentPosition = transform.position;
        Debug.Log("プレイヤーの座標: " + currentPosition);

        // x, y, z 座標を個別に取得
        float x = currentPosition.x;
        float z = currentPosition.z;
        Debug.Log("X座標: " + x + ", Z座標: " + z);

        Vector3 play = GameObject.Find("Player").transform.position;//プレイヤーの座標取得
        Instantiate(missile, new Vector3(play.x, y, play.z), Quaternion.Euler(180, 0, 0));//プレイヤーのいる座標に向かって発射
        


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
        audioSource.PlayOneShot(lazerclip);
        GameObject Attack6lazerp = Instantiate(lazer, new Vector3(30, lazerpointy, attack2lazerz), Quaternion.identity);//発射
        StartCoroutine(ExtendLazer6p1(Attack6lazerp));
    }
    IEnumerator ExtendLazer6p1(GameObject Attack6lazerp)
    {
        Vector3 scale = Attack6lazerp.transform.localScale;
        scale.z = 0f; // 最初は長さ0
        Attack6lazerp.transform.localScale = scale;
        while (scale.z > maxLength)
        {
            scale.z -= extendSpeed * Time.deltaTime;
            Attack6lazerp.transform.localScale = scale;

            Destroy(Attack6lazerp, 1f);

           
            yield return null; // 次のフレームへ
        }
        // 最終値を保証
        scale.z = maxLength;
        Attack6lazerp.transform.localScale = scale;
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
        audioSource.PlayOneShot(lazerclip);
        GameObject Attack6lazerm2 = Instantiate(lazer, new Vector3(-30, lazerpointy, attack2lazerz), Quaternion.identity);//発射
        StartCoroutine(ExtendLazer6m2(Attack6lazerm2));
        Destroy(Attack6lazerm2, 1f);
        
    }
    IEnumerator ExtendLazer6m2(GameObject Attack6lazerm2)
    {
        Vector3 scale = Attack6lazerm2.transform.localScale;
        scale.z = 0f; // 最初は長さ0
        Attack6lazerm2.transform.localScale = scale;
        while (scale.z > maxLength)
        {
            scale.z -= extendSpeed * Time.deltaTime;
            Attack6lazerm2.transform.localScale = scale;

            Destroy(Attack6lazerm2, 1f);

            
            yield return null; // 次のフレームへ
        }
        // 最終値を保証
        scale.z = maxLength;
        Attack6lazerm2.transform.localScale = scale;

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
    {
        audioSource.PlayOneShot(lazerclip);
        GameObject Attack6lazerm = Instantiate(lazer, new Vector3(-30, lazerpointy, attack2lazerz), Quaternion.identity);//発射
        StartCoroutine(ExtendLazer6m1(Attack6lazerm));
    }
    IEnumerator ExtendLazer6m1(GameObject Attack6lazerm)
    {
        Vector3 scale = Attack6lazerm.transform.localScale;
        scale.z = 0f; // 最初は長さ0
        Attack6lazerm.transform.localScale = scale;
       
        while (scale.z > maxLength)
        {
            scale.z -= extendSpeed * Time.deltaTime;
            Attack6lazerm.transform.localScale = scale;

            Destroy(Attack6lazerm, 1f);


            yield return null; // 次のフレームへ
        }
        // 最終値を保証
        scale.z = maxLength;
        Attack6lazerm.transform.localScale = scale;
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
        audioSource.PlayOneShot(lazerclip);
        GameObject Attack6lazerp2 = Instantiate(lazer, new Vector3(30, lazerpointy, attack2lazerz), Quaternion.identity);//発射
        StartCoroutine(ExtendLazer6p2(Attack6lazerp2));
        Debug.Log("攻撃Ⅵ パターン2");
    }
    IEnumerator ExtendLazer6p2(GameObject Attack6lazerp2)
    {
        Vector3 scale = Attack6lazerp2.transform.localScale;
        scale.z = 0f; // 最初は長さ0
        Attack6lazerp2.transform.localScale = scale;

        while (scale.z > maxLength)
        {
            scale.z -= extendSpeed * Time.deltaTime;
            Attack6lazerp2.transform.localScale = scale;

            Destroy(Attack6lazerp2, 1f);


            yield return null; // 次のフレームへ
        }
        // 最終値を保証
        scale.z = maxLength;
        Attack6lazerp2.transform.localScale = scale;
    }
}
