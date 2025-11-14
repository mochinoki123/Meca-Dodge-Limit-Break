using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class enemyattack : MonoBehaviour
{
    [SerializeField] GameObject missile;//ミサイル攻撃のオブジェクト
    [SerializeField] GameObject attackpoint;//攻撃発生地点

    [SerializeField] float rndm;//フィールドごとの範囲指定マイナス
    [SerializeField] float rndp;//フィールドごとの範囲指定プラス

    [SerializeField] int attackf;//攻撃の間隔

    [SerializeField] int attackpointx;//攻撃発生左右
    [SerializeField] int attackpointy;//攻撃発生の高さ
    [SerializeField] int attackpointz;//攻撃発生の奥行




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        float x = Random.Range(rndm, rndp);//地面の広さによって変更
        float z = Random.Range(rndm, rndp);//地面の広さによって変更
        for (int i = 0; i < 6; i++)
        {
            float numx = Random.Range(rndm, rndp);
            float numz = Random.Range(rndm, rndp);

            /*
            Instantiate(missile, new Vector3(x, attackpointy, attackpointz - i * attackf), Quaternion.identity);//発射
            Instantiate(attackpoint, new Vector3(x, 0, attackpointz - i * attackf), Quaternion.identity);//攻撃範囲
            */

            Instantiate(missile, new Vector3((attackf * numx) - numx, attackpointy, (attackf * numz) - numz), Quaternion.identity);//発射
            Instantiate(attackpoint, new Vector3((attackf * numx) - numx, 0, (attackf * numz) - numz), Quaternion.identity);//攻撃範囲

        }


        
    }

    
    // Update is called once per frame
    void Update()
    {
        Rigidbody ballRigidbody = missile.GetComponent<Rigidbody>();

    }
}
