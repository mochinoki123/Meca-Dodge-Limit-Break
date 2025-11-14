using UnityEngine;

public class enemymissile : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("AttackPoint")) // 敵かどうかのタグをチェック
        {
            Destroy(collision.gameObject); // 着弾ポイントを破壊
            Destroy(gameObject); // 弾も消す
        }
    }
}
