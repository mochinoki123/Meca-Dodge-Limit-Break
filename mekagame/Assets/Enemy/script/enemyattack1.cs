using UnityEngine;

public class enemyattack1 : MonoBehaviour
{
    private enemyattack enemymanager;
    [SerializeField] float rndm = -9;//フィールドごとの範囲指定マイナス
    [SerializeField] float rndp = 9;//フィールドごとの範囲指定プラス
    [SerializeField] int attackf = 5;//攻撃の間隔 5
    [SerializeField] int attackpointx = 10;//攻撃発生の横 10
    [SerializeField] int attackpointy = 60;//攻撃発生の高さ 25
    [SerializeField] int attackpointz = 10;//攻撃発生の奥行 10
    //攻撃１
    [Header("攻撃Ⅰ")]
    [SerializeField] int attack1missile= 10;//攻撃１のミサイル数　6
    float groundx;//random値確認用基本使わない
    float groundz;//random値確認用基本使わない
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemymanager = GetComponent<enemyattack>();
        Invoke("Attack1", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack1()
    {
        for (int i = 0; i < attack1missile; i++)
        {
            GameObject objm1 = enemymanager.Get();

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
            objm1.transform.position = new Vector3((attackf * groundx) - groundx, 0f, (attackf * groundz) - groundz);
            //objm1.transform.rotation = Quaternion.Euler(180, 0, 0);
            objm1.transform.rotation = Quaternion.identity;
            objm1.SetActive(true);

        }
        //Debug.Log("攻撃Ⅰ");
    }
}
