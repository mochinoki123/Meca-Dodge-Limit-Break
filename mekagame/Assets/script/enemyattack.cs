using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class enemyattack : MonoBehaviour
{
    [SerializeField] GameObject missile;//ミサイル攻撃のオブジェクト
    [SerializeField] GameObject attackpoint;//攻撃発生地点

    [SerializeField] float rndm;//フィールドごとの範囲指定マイナス
    [SerializeField] float rndp;//フィールドごとの範囲指定プラス

    [SerializeField] int attackf;//攻撃の間隔
    [SerializeField] int attackpointy;//攻撃発生の高さ
    [SerializeField] int attackpointz;//攻撃発生の奥行




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float x = Random.Range(rndm, rndp);//地面の広さによって変更
        for (int i = 0; i < 6; i++)
        {
            Instantiate(missile, new Vector3(x, attackpointy, attackpointz - i * attackf), Quaternion.identity);//発射
            Instantiate(attackpoint, new Vector3(x, 0, attackpointz - i * attackf), Quaternion.identity);//攻撃範囲

        }


        
    }

    
    // Update is called once per frame
    void Update()
    {
        Rigidbody ballRigidbody = missile.GetComponent<Rigidbody>();

    }
}
