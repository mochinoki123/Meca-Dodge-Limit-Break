using UnityEngine;

public class enemyattack1 : MonoBehaviour
{
    private enemyattack enemymanager;
    //攻撃１
    [Header("攻撃Ⅰ")]
    public float missilespeed = 45f;
    [SerializeField] int attack1missile = 10;//攻撃１のミサイル数　6
    [SerializeField] float rndm = -9;//フィールドごとの範囲指定マイナス
    [SerializeField] float rndp = 9;//フィールドごとの範囲指定プラス
    [SerializeField] int attackf = 5;//攻撃の間隔 5
    [SerializeField] int attackpointx = 10;//攻撃発生の横 10
    [SerializeField] int attackpointz = 10;//攻撃発生の奥行 10
    
    float groundx;//random値確認用基本使わない
    float groundz;//random値確認用基本使わない
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack1()
    {
        for (int i = 0; i < attack1missile; i++)
        {
            GameObject objm1 = ObjectPool_Missile.Instance.GetMissile();

            groundx = Random.Range(rndm, rndp);//地面の広さによって変更
            groundz = Random.Range(rndm, rndp);//地面の広さによって変更

            objm1.transform.position = new Vector3((attackf * groundx) - groundx, 0.1f, (attackf * groundz) - groundz);
            //objm1.transform.rotation = Quaternion.Euler(180, 0, 0);
            objm1.transform.rotation = Quaternion.identity;
            //objm1.SetActive(true);
        }
        //objm1.SetActive(true);
        Debug.Log("攻撃Ⅰ");
    }
}
