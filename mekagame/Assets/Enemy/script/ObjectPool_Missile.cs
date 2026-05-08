using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool_Missile : MonoBehaviour
{
    ObjectPool<GameObject> pool;
    public GameObject MissilePrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pool = new ObjectPool<GameObject>(
            CreatePooledMissile,       // オブジェクト生成の際の処理
            OnTakeFromPool,         // オブジェクトを取り出す際の処理
            OnReturnedToPool,       // オブジェクトを返却する際の処理
            OnDestroyPoolMissile,    // プールが上限を超えた場合の処理
            true,                   // すでにプール内にいるオブジェクトを返却した際にエラー表示するか
            2,                      // 初期のプールの容量
            50);                    // プール内オブジェクトの上限数
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // オブジェクト生成の際の処理
    GameObject CreatePooledMissile()
    {
        return Instantiate(MissilePrefab);    // オブジェクトを生成してプールに渡す処理
    }

    // オブジェクトを取り出す際の処理
    void OnTakeFromPool(GameObject objm)
    {
        objm.SetActive(true);    // オブジェクトをアクティブにする処理
        objm.transform.position = new Vector2(Random.Range(-8f, 8f), Random.Range(-4.5f, 4.5f));  // オブジェクトの座標を指定する処理
    }

    // オブジェクトを返却する際の処理
    void OnReturnedToPool(GameObject objm)
    {
        objm.SetActive(false);   // オブジェクトを非アクティブにする処理
    }

    // プールが上限を超えた場合の処理
    void OnDestroyPoolMissile(GameObject objm)
    {
        Destroy(objm);    // オブジェクトを破壊する処理
    }
}
