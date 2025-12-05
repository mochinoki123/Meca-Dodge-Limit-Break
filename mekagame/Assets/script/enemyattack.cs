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
    [SerializeField] GameObject bpoint;//爆発ポイント
    //攻撃座標関係
    [SerializeField] int attackf;//攻撃の間隔
    [SerializeField] int attackpointx;
    [SerializeField] int attackpointy;//攻撃発生の高さ
    [SerializeField] int attackpointz;//攻撃発生の奥行
    public float ap;//random値確認用基本使わない
    public float x;//random値確認用基本使わない
    public float z;//random値確認用基本使わない
    //レーザーy座標
    [SerializeField] int lazerpointy;
    public int attack123;//random値確認用基本使わない

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
        Rigidbody cubeRigidbody = lazer.GetComponent<Rigidbody>();
    }

    void EnemyAttackController1()
    {
        Invoke("Attack1", 3f);
        Invoke("Attack2lp", 7f);
        Invoke("Attack3", 12f);
        Invoke("AttackLoop", 15f);
    }
    void EnemyAttackController2()
    {
      Invoke("Attack4", 3f);
      Invoke("Attack5lp", 7f);
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
            Attack2lp();
        }
        else
        {
            Attack3();
        }
    }

    void Attack1()
    {
        for (int i = 0; i < attack1missile; i++)
        {
            x = Random.Range(rndm, rndp);//地面の広さによって変更
            z = Random.Range(rndm, rndp);//地面の広さによって変更

            /*
            Instantiate(missile, new Vector3(x, attackpointy, attackpointz - i * attackf), Quaternion.identity);//発射
            Instantiate(attackpoint, new Vector3(x, 0, attackpointz - i * attackf), Quaternion.identity);//攻撃範囲
            */

            Instantiate(missile, new Vector3((attackf * x) - x, attackpointy, (attackf * z) - z), Quaternion.identity);//発射
          //Instantiate(attackpoint, new Vector3((attackf * x) - x, 0, (attackf * z) - z), Quaternion.identity);//攻撃範囲
        }
        Debug.Log("攻撃Ⅰ");
    }

    void Attack2lp()
    {
      //float z = Random.Range(rndm, rndp);//地面の広さによって変更
        x = Random.Range(rndm, rndp);

        GameObject Attack2lazerattackpoint = Instantiate(lazerattackpoint, new Vector3(x, 0, 0), Quaternion.identity);
        Destroy(Attack2lazerattackpoint, 3f);
        Invoke("Attack2l", 2f);
    }

    void Attack2l()
    {
        GameObject Attack2lazer = Instantiate(lazer, new Vector3(x, lazerpointy, 0), Quaternion.identity);//発射
        Destroy(Attack2lazer, 1f);
        Debug.Log("攻撃Ⅱ");
    }

    void Attack3()
    {
        attackbunki = Random.Range(0, 1);

        if (attackbunki < 0.5f)
        {
            for (int i = 1; i <= attack3missilex; i++)
            {
                Instantiate(missile, new Vector3(attackpointx - i * attackf, attackpointy, attackpointz - i * attackf), Quaternion.identity);
                //Instantiate(attackpoint, new Vector3(attackpointx - i * attackf, 0, attackpointz - i * attackf), Quaternion.identity);
                Instantiate(missile, new Vector3(attackpointx - i * attackf, attackpointy, attackpointz + i * attackf), Quaternion.identity);
                //Instantiate(attackpoint, new Vector3(attackpointx - i * attackf, 0, attackpointz + i * attackf), Quaternion.identity);
                Instantiate(missile, new Vector3(attackpointx + i * attackf, attackpointy, attackpointz - i * attackf), Quaternion.identity);
                //Instantiate(attackpoint, new Vector3(attackpointx + i * attackf, 0, attackpointz - i * attackf), Quaternion.identity);
                Instantiate(missile, new Vector3(attackpointx + i * attackf, attackpointy, attackpointz + i * attackf), Quaternion.identity);
                //Instantiate(attackpoint, new Vector3(attackpointx + i * attackf, 0, attackpointz + i * attackf), Quaternion.identity);
                
            }
            Debug.Log("攻撃Ⅲx");
        }
        else
        {
            for (int i = 1; i <= attack3missiley; i++)
            {
                Instantiate(missile, new Vector3(0, attackpointy, attackpointz3 - i * attackf), Quaternion.identity);
                //Instantiate(attackpoint, new Vector3(0, 0, attackpointz3ty - i * attackf), Quaternion.identity);
                Instantiate(missile, new Vector3(attackpointx3 - i * attackf, attackpointy, 0), Quaternion.identity);
                //Instantiate(attackpoint, new Vector3(attackpointx3ty - i * attackf, 0, 0), Quaternion.identity);
                
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
        GameObject Attack4bpoint1 = Instantiate(bpoint, new Vector3(ap, 0, ap + 5), Quaternion.identity);
        GameObject Attack4bpoint2 = Instantiate(bpoint, new Vector3(ap + 5, 0, ap), Quaternion.identity);
        GameObject Attack4bpoint3 = Instantiate(bpoint, new Vector3(ap, 0, ap - 5), Quaternion.identity);
        GameObject Attack4bpoint4 = Instantiate(bpoint, new Vector3(ap - 5, 0, ap), Quaternion.identity);
        Destroy(Attack4bpoint1, 2f);
        Destroy(Attack4bpoint2, 2f);
        Destroy(Attack4bpoint3, 2f);
        Destroy(Attack4bpoint4, 2f);
        Debug.Log("攻撃Ⅳ");
    }

    void Attack5lp()
    {
        attackbunki = Random.Range(0, 1);
        if (attackbunki < 0.5f)
        {
            
            GameObject Attack5lazerattackpoint = Instantiate(lazerattackpoint, new Vector3(x, 0, 0), Quaternion.identity);
            Destroy(Attack5lazerattackpoint, 3f);
            Invoke("Attack5lx", 2f);
        }
        else 
        {

            GameObject Attack5lazerattackpoint = Instantiate(lazerattackpoint, new Vector3(0, 0, z), Quaternion.identity);
            Destroy(Attack5lazerattackpoint, 3f);
            Invoke("Attack5lz", 2f);
        }
    }

    void Attack5lx()
    {
        GameObject Attack5lazer = Instantiate(lazer, new Vector3(x, lazerpointy, 0), Quaternion.identity);//発射
        Destroy(Attack5lazer, 1f);
        Debug.Log("攻撃Ⅴx");
    }

    void Attack5lz()
    {
        GameObject Attack5lazer = Instantiate(lazer, new Vector3(0, lazerpointy, z), Quaternion.identity);//発射
        Destroy(Attack5lazer, 1f);
        Debug.Log("攻撃Ⅴz");
    }
}
