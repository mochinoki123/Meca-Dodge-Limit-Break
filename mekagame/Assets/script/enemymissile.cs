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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AttackPoint")) //タグをチェック
        {
            Destroy(other.gameObject); // 着弾ポイントを破壊
            Destroy(gameObject); // 攻撃も消す
        }
    }
}
