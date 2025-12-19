using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class enemyattack : MonoBehaviour
{
    //Enemyスクリプト
    private Enemy enemyhpscripts;
    //プレハブ
    [SerializeField] GameObject missile;//ミサイル攻撃のオブジェクト
  //[SerializeField] GameObject attackpoint;//攻撃発生地点
    //フィールド範囲
    [SerializeField] float rndm;//フィールドごとの範囲指定マイナス
    [SerializeField] float rndp;//フィールドごとの範囲指定プラス
    Vector3 play;
    //攻撃１
    [SerializeField] int attack1missile;
    //攻撃２
    [SerializeField] GameObject lazer;//レーザーオブジェクト
    [SerializeField] GameObject lazerattackpoint;//レーザー発生ポイントオブジェクト
    //攻撃３
    [SerializeField] int attack3missilex;
    [SerializeField] int attack3missiley;
    [SerializeField] int attackpointx3;
    [SerializeField] int attackpointz3;
    public float attackbunki;//random値確認用基本使わない
    //攻撃４
    [SerializeField] int attack4missile;
    [SerializeField] GameObject bpoint;//爆発ポイント
    //攻撃５
    [SerializeField] GameObject lazerx;//レーザーオブジェクト
    [SerializeField] int Attack5ls;
    [SerializeField] GameObject lazerattackpointx;//レーザー発生ポイントオブジェクト
    [SerializeField] public float jx = 60;
    [SerializeField] public float jz = 50;
    [SerializeField] public float k;
    //攻撃６
    [SerializeField] int Attack6ms;
    //攻撃座標関係
    [SerializeField] int attackf;//攻撃の間隔
    [SerializeField] int attackpointx;
    [SerializeField] int attackpointy;//攻撃発生の高さ
    [SerializeField] int attackpointz;//攻撃発生の奥行
    public float ap;//random値確認用基本使わない
    public float groundx;//random値確認用基本使わない
    public float groundz;//random値確認用基本使わない
    //レーザーy座標
    [SerializeField] int lazerpointy;
    public int attack123;//random値確認用基本使わない
    public int attack12345;//random値確認用基本使わない
    public int attack123456;

    public float x;
    public float y;
    public float z;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyhpscripts = GetComponent<Enemy>();
        EnemyAttackController1();
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody ballRigidbody = missile.GetComponent<Rigidbody>();
     // transform.position で現在のワールド座標を取得
        Vector3 currentPosition = transform.position;
        Debug.Log("プレイヤーの座標: " + currentPosition);

        // x, y, z 座標を個別に取得
        float x = currentPosition.x;
        float z = currentPosition.z;
        Debug.Log("X座標: " + x + ", Z座標: " + z);
    }

    void EnemyAttackController1()
    {
        Invoke("Attack1", 3f);
        Invoke("Attack2", 7f);
        Invoke("Attack3", 12f);
        Invoke("AttackLoop", 15f);
    }
    void EnemyAttackController2()
    {
        Invoke("Attack4", 3f);
        Invoke("Attack5", 7f);
        Invoke("AttackLoop2", 25f);
    }
    void EnemyAttackController3()
    {
        Invoke("Attack6", 3f);
        Invoke("AttackLoop3", 10f);
    }

    void AttackLoop()
    {
        StartCoroutine(AttackLoopCoroutine());
    }

    IEnumerator AttackLoopCoroutine()
    {

        while (enemyhpscripts.EnemyHP > 750)
        {
            attack123 = Random.Range(0, 99);
            Attackrnd();

            yield return new WaitForSeconds(3f);
        }

        Debug.Log("攻撃追加");
        EnemyAttackController2();
    }

    void Attackrnd()
    {
        if (attack123 <=33)
        {
            Attack1();
        }
        else if (attack123 <=66)
        {
            Attack2();
        }
        else
        {
            Attack3();
        }
    }
    void AttackLoop2()
    {
        StartCoroutine(AttackLoop2Coroutine());
    }
    IEnumerator AttackLoop2Coroutine()
    {
        while (enemyhpscripts.EnemyHP > 500)
        {
            attack12345 = Random.Range(0, 99);
            Attackrndv2();

            yield return new WaitForSeconds(3f);
        }

        Debug.Log("攻撃追加Ⅱ");
        EnemyAttackController3();
    }
    void Attackrndv2()
    {
        if (attack12345 <= 20)
        {
            Attack1();
        }
        else if (attack12345 <= 40)
        {
            Attack2();
        }
        else if (attack12345 <= 60)
        {
            Attack3();
        }
        else if (attack12345 <= 80)
        {
            Attack4();
        }
        else
        {
            Attack5();
        }
    }
    void AttackLoop3()
    {
        StartCoroutine(AttackLoop3Coroutine());
    }
    IEnumerator AttackLoop3Coroutine()
    {
        while (enemyhpscripts.EnemyHP > 250)
        {
            attack123456 = Random.Range(0, 99);
            Attackrndv3();

            yield return new WaitForSeconds(3f);
        }

        Debug.Log("攻撃追加");
        EnemyAttackController3();
    }
    void Attackrndv3()
    {
        if (attack123456 <= 16)
        {
            Attack1();
        }
        else if (attack123456 <= 32)
        {
            Attack2();
        }
        else if (attack123456 <= 48)
        {
            Attack3();
        }
        else if (attack123456 <= 64)
        {
            Attack4();
        }
        else if (attack123456 <= 80)
        {
            Attack5();
        }
        else
        {
            Attack6();
        }
    }

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

            Instantiate(missile, new Vector3((attackf * groundx) - groundx, attackpointy, (attackf * groundz) - groundz), Quaternion.identity);//発射
          //Instantiate(attackpoint, new Vector3((attackf * x) - x, 0, (attackf * z) - z), Quaternion.identity);//攻撃範囲
        }
        Debug.Log("攻撃Ⅰ");
    }

    void Attack2()
    {

        GameObject Attack2lazerattackpoint = Instantiate(lazerattackpoint, new Vector3(groundx, 0, 0), Quaternion.identity);
        Destroy(Attack2lazerattackpoint, 3f);
        Invoke("Attack2l", 2f);
    }

    void Attack2l()
    {
        GameObject Attack2lazer = Instantiate(lazer, new Vector3(groundx, lazerpointy, 0), Quaternion.identity);//発射
        Destroy(Attack2lazer, 1f);
        Debug.Log("攻撃Ⅱ");
    }

    void Attack3()
    {
        attackbunki = Random.Range(0, 1);

        if (attackbunki < 0.5f)
        {
            for (int i = 0; i < attack3missilex; i++)
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
                Instantiate(missile, new Vector3(attackpointx * i, attackpointy, -attackpointz * i), Quaternion.identity);
                Instantiate(missile, new Vector3(-attackpointx * i, attackpointy, attackpointz * i), Quaternion.identity);
                Instantiate(missile, new Vector3(attackpointx * i, attackpointy, attackpointz * i), Quaternion.identity);
                Instantiate(missile, new Vector3(-attackpointx * i, attackpointy, -attackpointz * i), Quaternion.identity);
            }
            Debug.Log("攻撃Ⅲx");
        }
        else
        {
            for (int i = 0; i < attack3missiley; i++)
            {
                /*
                Instantiate(missile, new Vector3(0, attackpointy, attackpointz3 - i * attackf), Quaternion.identity);
                //Instantiate(attackpoint, new Vector3(0, 0, attackpointz3ty - i * attackf), Quaternion.identity);
                Instantiate(missile, new Vector3(attackpointx3 - i * attackf, attackpointy, 0), Quaternion.identity);
                //Instantiate(attackpoint, new Vector3(attackpointx3ty - i * attackf, 0, 0), Quaternion.identity);
                */
                Instantiate(missile, new Vector3(0 , attackpointy, -attackpointz * i), Quaternion.identity);
                Instantiate(missile, new Vector3(-attackpointx * i, attackpointy, 0), Quaternion.identity);
                Instantiate(missile, new Vector3(0, attackpointy, attackpointz * i), Quaternion.identity);
                Instantiate(missile, new Vector3(attackpointx * i, attackpointy, 0), Quaternion.identity);
            }
            Debug.Log("攻撃Ⅲ+");
        }
        Debug.Log("攻撃Ⅲ");
    }

    void Attack4()
    {
        ap = Random.Range(rndm, rndp);//地面の広さによって変更
        Instantiate(missile, new Vector3(ap, attackpointy, ap), Quaternion.identity);
        Invoke("Attack4b", 2f);
    }
    void Attack4b()
    {
        for (int i = 1; i < attack4missile; i++)
        {
            GameObject Attack4bpoint1 = Instantiate(bpoint, new Vector3(ap, 0, ap + 10 * i), Quaternion.identity);
            GameObject Attack4bpoint2 = Instantiate(bpoint, new Vector3(ap + 10 * i, 0, ap), Quaternion.identity);
            GameObject Attack4bpoint3 = Instantiate(bpoint, new Vector3(ap, 0, ap - 10 * i), Quaternion.identity);
            GameObject Attack4bpoint4 = Instantiate(bpoint, new Vector3(ap - 10 * i, 0, ap), Quaternion.identity);
            Destroy(Attack4bpoint1, 2f);
            Destroy(Attack4bpoint2, 2f);
            Destroy(Attack4bpoint3, 2f);
            Destroy(Attack4bpoint4, 2f);
        }
        Debug.Log("攻撃Ⅳ");
    }

    void Attack5() 
    {
        attackbunki = Random.Range(0, 1);
        if (attackbunki < 0.5f)
        {
            StartCoroutine(Attack5lxCoroutine());
        }
        else
        {
            StartCoroutine(Attack5lzCoroutine());
        }
    }
    IEnumerator Attack5lxCoroutine()
    {
        int i = 0;
        while (i < Attack5ls)
        {
            Attack5lpx();
            i++;
            yield return new WaitForSeconds(2f);
        }
        jx = 60;
    }
    IEnumerator Attack5lzCoroutine()
    {
        int i = 0;
        while (i < Attack5ls)
        {
            Attack5lpz();
            i++;
            yield return new WaitForSeconds(2f);
        }
        Debug.Log("攻撃Ⅴ");
        jz = 50;
    }
    void Attack5lpx()
    {
        GameObject Attack5lazerattackpoint = Instantiate(lazerattackpoint, new Vector3(jx, 0, 0), Quaternion.identity);
        Destroy(Attack5lazerattackpoint, 3f);
        Invoke("Attack5lx", 2f);
    }

    void Attack5lx()
    {
        GameObject Attack5lazer = Instantiate(lazer, new Vector3(jx, lazerpointy, 0), Quaternion.identity);//発射
      //Rigidbody cubeRigidbody = Attack5lazer.GetComponent<Rigidbody>();
      //cubeRigidbody.AddForce(new Vector3(0, 0, 1) * 10, ForceMode.Impulse);
        Destroy(Attack5lazer, 1f);
        jx = jx - k;
        Debug.Log("攻撃Ⅴx");
    }

    void Attack5lpz()
    {
        GameObject Attack5lazerattackpointx = Instantiate(lazerattackpointx, new Vector3(0, 0, jz), Quaternion.identity);
        Destroy(Attack5lazerattackpointx, 3f);
        Invoke("Attack5lz", 2f);
    }

    void Attack5lz()
    {
        GameObject Attack5lazerx = Instantiate(lazerx, new Vector3(0, lazerpointy, jz), Quaternion.identity);//発射
      //Rigidbody cubeRigidbody = Attack5lazerx.GetComponent<Rigidbody>();
      //cubeRigidbody.AddForce(new Vector3(1, 0, 0) * 10, ForceMode.Impulse);
        Destroy(Attack5lazerx, 1f);
        jz = jz - k;
        Debug.Log("攻撃Ⅴz");
    }

    void Attack6()
    {
        StartCoroutine(Attack6missileCoroutine());
        attackbunki = Random.Range(0, 1);
        if (attackbunki < 0.5f)
        {
            StartCoroutine(Attack6lazer1Coroutine());
        }
        else
        {
            StartCoroutine(Attack6lazer2Coroutine());
        }

        Debug.Log("攻撃Ⅵ");
    }

    IEnumerator Attack6missileCoroutine()
    {
        int i = 0;
        while (i < Attack6ms)
        {
            Attack6missile();
            i++;
            yield return new WaitForSeconds(1f);
        }
        Debug.Log("攻撃Ⅵ missile");
    }
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
    void Attack6missile()
    {
        /*
        Vector3 play = GameObject.Find("Player").transform.position;
        Instantiate(missile, play, Quaternion.identity);
        */


        Instantiate(missile, new Vector3(x,y,z), Quaternion.identity);
    }

    void Attack6lazerppoint()
    {
        GameObject Attack6lazerattackpointp = Instantiate(lazerattackpoint, new Vector3(30, 0, 0), Quaternion.identity);
        Destroy(Attack6lazerattackpointp, 3f);
        Invoke("Attack6lazerp", 2f);
    }

    void Attack6lazerp2point()
    {
        GameObject Attack6lazerattackpointp2 = Instantiate(lazerattackpoint, new Vector3(30, 0, 0), Quaternion.identity);
        Destroy(Attack6lazerattackpointp2, 3f);
        Invoke("Attack6lazerp2", 2f);
    }
    void Attack6lazermpoint()
    {
        GameObject Attack6lazerattackpointm = Instantiate(lazerattackpoint, new Vector3(-30, 0, 0), Quaternion.identity);
        Destroy(Attack6lazerattackpointm, 3f);
        Invoke("Attack6lazerm", 2f);
    }
    void Attack6lazerm2point()
    {
        GameObject Attack6lazerattackpointm2 = Instantiate(lazerattackpoint, new Vector3(-30, 0, 0), Quaternion.identity);
        Destroy(Attack6lazerattackpointm2, 3f);
        Invoke("Attack6lazerm2", 2f);
    }
    void Attack6lazerp()
    {
        GameObject Attack6lazerp = Instantiate(lazer, new Vector3(30, lazerpointy, 0), Quaternion.identity);//発射
        Destroy(Attack6lazerp, 1f);
        Invoke("Attack6lazerm2point", 2f);
    }
    void Attack6lazerm()
    {
        GameObject Attack6lazerm = Instantiate(lazer, new Vector3(-30, lazerpointy, 0), Quaternion.identity);//発射
        Destroy(Attack6lazerm, 1f);
        Invoke("Attack6lazerp2point", 2f);
    }
    void Attack6lazerp2()
    {
        GameObject Attack6lazerp2 = Instantiate(lazer, new Vector3(30, lazerpointy, 0), Quaternion.identity);//発射
        Destroy(Attack6lazerp2, 1f);
        Debug.Log("攻撃Ⅵ パターン2");
    }
    void Attack6lazerm2()
    {
        GameObject Attack6lazerm2 = Instantiate(lazer, new Vector3(-30, lazerpointy, 0), Quaternion.identity);//発射
        Destroy(Attack6lazerm2, 1f);
        Debug.Log("攻撃Ⅵ パターン1");
    }
}
