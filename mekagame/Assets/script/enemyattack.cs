using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class enemyattack : MonoBehaviour
{
    [SerializeField] GameObject missile;//ミサイル攻撃のオブジェクト
    [SerializeField] GameObject attackpoint;//攻撃発生地点
    [SerializeField] GameObject lazer;
    [SerializeField] GameObject lazerattackpoint;

    [SerializeField] float rndm;//フィールドごとの範囲指定マイナス
    [SerializeField] float rndp;//フィールドごとの範囲指定プラス
    
    [SerializeField] int attackf;//攻撃の間隔

    [SerializeField] int attack1missile;

    [SerializeField] int attack3missilex;
    [SerializeField] int attack3missilety;
    [SerializeField] int attackpointx3ty;
    [SerializeField] int attackpointz3ty;

    [SerializeField] int attackpointx;
    [SerializeField] int attackpointy;//攻撃発生の高さ
    [SerializeField] int attackpointz;//攻撃発生の奥行

    [SerializeField] int lazerpointy;

    public float x;
    public float z;

    public int attack3bunki;//random値確認用基本使わない

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
            Instantiate(attackpoint, new Vector3((attackf * x) - x, 0, (attackf * z) - z), Quaternion.identity);//攻撃範囲
        }
    }
    void Attack2lp()
    { 
      //float z = Random.Range(rndm, rndp);//地面の広さによって変更

        Instantiate(lazerattackpoint, new Vector3(x, 0, 0), Quaternion.identity);
        Destroy(lazerattackpoint, 3f);
        Invoke("Attack2l",2f);
        
    }
    void Attack2l() 
    {
        Instantiate(lazer, new Vector3(x, lazerpointy, 0), Quaternion.identity);//発射
        Destroy(lazer,1f);
    }
    void Attack3()
    {
        
        //攻撃3kari
        if (attack3bunki < 50)
        {
            for (int i = 1; i <= attack3missilex; i++)
            {
                Instantiate(missile, new Vector3(attackpointx - i * attackf, attackpointy, attackpointz - i * attackf), Quaternion.identity);
                Instantiate(attackpoint, new Vector3(attackpointx - i * attackf, 0, attackpointz - i * attackf), Quaternion.identity);
                Instantiate(missile, new Vector3(attackpointx - i * attackf, attackpointy, attackpointz + i * attackf), Quaternion.identity);
                Instantiate(attackpoint, new Vector3(attackpointx - i * attackf, 0, attackpointz + i * attackf), Quaternion.identity);
                Instantiate(missile, new Vector3(attackpointx + i * attackf, attackpointy, attackpointz - i * attackf), Quaternion.identity);
                Instantiate(attackpoint, new Vector3(attackpointx + i * attackf, 0, attackpointz - i * attackf), Quaternion.identity);
                Instantiate(missile, new Vector3(attackpointx + i * attackf, attackpointy, attackpointz + i * attackf), Quaternion.identity);
                Instantiate(attackpoint, new Vector3(attackpointx + i * attackf, 0, attackpointz + i * attackf), Quaternion.identity);
            }
        }
        else
        {
            for (int i = 1; i <= attack3missilety; i++)
            {
                Instantiate(missile, new Vector3(0, attackpointy, attackpointz3ty - i * attackf), Quaternion.identity);
                Instantiate(attackpoint, new Vector3(0, 0, attackpointz3ty - i * attackf), Quaternion.identity);
                Instantiate(missile, new Vector3(attackpointx3ty - i * attackf, attackpointy, 0), Quaternion.identity);
                Instantiate(attackpoint, new Vector3(attackpointx3ty - i * attackf, 0, 0), Quaternion.identity);
            }
        }
            
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        x = Random.Range(rndm, rndp);//地面の広さによって変更
        z = Random.Range(rndm, rndp);//地面の広さによって変更
        attack3bunki = Random.Range(0, 100);

        Invoke("Attack1", 3f);
      //Invoke("Attack2lp", 7f);
        Invoke("Attack3", 12.5f);
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody ballRigidbody = missile.GetComponent<Rigidbody>();
    }
}
