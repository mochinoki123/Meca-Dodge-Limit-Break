using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class enemyattack : MonoBehaviour
{
    [SerializeField] GameObject ball;//弾・ミサイル攻撃のオブジェクト
    [SerializeField] GameObject attackpoint;//攻撃発生地点
    [SerializeField] int attackf;//攻撃開始地点
    [SerializeField] int attackpointy;//攻撃発生の高さ
    [SerializeField] int attackpointz;//攻撃発生の奥行

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float x = Random.Range(-2.5f, 2.5f);//地面の広さによって変更
        for (int i = 0; i < 6; i++)
        {
            Instantiate(ball, new Vector3(x, attackpointy, attackpointz - i * attackf), Quaternion.identity);//発射
            Instantiate(attackpoint, new Vector3(x, 0, attackpointz - i * attackf), Quaternion.identity);//攻撃範囲
        }
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
        
    }
}
