using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool_Lazer : MonoBehaviour
{
    //シングルトンの作成
    public static ObjectPool_Lazer instance;    //ObjectPool_Laszer型の変数を宣言

    private void Awake()
    {
        //インスタンスが入っていない場合
        if(instance == null)
        {
            instance = this;  //インスタンス自身を入れる処理 
        }
        //インスタンスが既に入っている場合
        else
        {
            Destroy(this.gameObject);   //インスタンスに紐づくオブジェクトを破壊
        }
    }

    //オブジェクトプールの作成
    ObjectPool<GameObject> pool;    //ObjectPoolがたの変数を宣言
    public GameObject laserPrefab;  //オブジェクトプールで管理するオブジェクトを指定

    private void Start()
    {
        //オブジェクトプールのインスタンスを生成する
        pool = new ObjectPool<GameObject>(
            CreateLaser,
            OnGetLaser,
            OnReturnLaser,
            OnDestroyLaser,
            false,
            1,
            20);

        var prewarmed = new GameObject[10];
        for (int i = 0; i < 10; i++)
            prewarmed[i] = pool.Get();
        for (int i = 0; i < 10; i++)
            pool.Release(prewarmed[i]);
    }

    public GameObject Spawn(Vector3 position, Quaternion rotation)
    {
        GameObject spawnedObj = pool.Get();

        spawnedObj.transform.SetPositionAndRotation(position, rotation);

        return spawnedObj;
    }

    GameObject CreateLaser()
    {
        return Instantiate(laserPrefab);    //prefabオブジェクトを生成する処理
    }

    void OnGetLaser(GameObject obj)
    {
        obj.SetActive(true);    //オブジェクトをアクティブにする処理
    }

    void OnReturnLaser(GameObject obj)
    {
        obj.SetActive(false);   //オブジェクトを非アクティブにする処理
    }

    void OnDestroyLaser(GameObject obj)
    {
        Destroy(obj);           //オブジェクトを破壊する処理
    }

    public void GetLaser()
    {
        pool.Get();
    }

    public void ReleaseLaser(GameObject obj)
    {
        pool.Release(obj);
    }
}
