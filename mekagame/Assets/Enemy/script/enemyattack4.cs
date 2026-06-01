using UnityEngine;

public class enemyattack4 : MonoBehaviour
{
    private enemyattack enemymanager;
    [Header("攻撃Ⅳ")]
    [SerializeField] int attack4missile = 10;//攻撃４のミサイル範囲指定
    [Header("爆発ポイントプレハブ")]
    [SerializeField] GameObject bpoint;//爆発ポイント
    [SerializeField] GameObject ClustereffectPrefab;//爆発のエフェクト
    [Header("攻撃範囲指定")]
    [SerializeField] float rndm = -9;//フィールドごとの範囲指定マイナス
    [SerializeField] float rndp = 9;//フィールドごとの範囲指定プラス
    float ap;//random値確認用基本使わない
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //-----攻撃Ⅳ-----

    public void Attack4()
    {
        ap = Random.Range(rndm, rndp);//地面の広さによって変更
        
        GameObject objm4 = ObjectPool_Missile.Instance.GetMissile();
        objm4.transform.position = new Vector3(ap, 0.1f, ap);
        //objm4.transform.rotation = Quaternion.Euler(180, 0, 0);
        objm4.transform.rotation = Quaternion.identity;
        //objm4.SetActive(true);

        //Instantiate(missile4, new Vector3(ap, attackpointy, ap), Quaternion.Euler(180, 0, 0));//初弾
        Invoke("Attack4b", 1f);
    }

    //攻撃Ⅳクラスター

    void Attack4b()
    {
        for (int i = 1; i < attack4missile; i++)
        {

            GameObject Attack4bpoint1 = Instantiate(bpoint, new Vector3(ap, 0, ap + 10 * i), Quaternion.Euler(180, 0, 0));
            GameObject Attack4bpoint2 = Instantiate(bpoint, new Vector3(ap + 10 * i, 0, ap), Quaternion.Euler(180, 0, 0));//東
            GameObject Attack4bpoint3 = Instantiate(bpoint, new Vector3(ap, 0, ap - 10 * i), Quaternion.Euler(180, 0, 0));//南
            GameObject Attack4bpoint4 = Instantiate(bpoint, new Vector3(ap - 10 * i, 0, ap), Quaternion.Euler(180, 0, 0));//西
            Destroy(Attack4bpoint1, 1f);
            Destroy(Attack4bpoint2, 1f);
            Destroy(Attack4bpoint3, 1f);
            Destroy(Attack4bpoint4, 1f);
        }
        Invoke("Attack4Cluster", 1f);
    }
    void Attack4Cluster()
    {
        for (int i = 1; i < attack4missile; i++)
        {
            GameObject Attack4effectbpoint1 = Instantiate(ClustereffectPrefab, new Vector3(ap, 0, ap + 10 * i), Quaternion.identity);//北
            GameObject Attack4effectbpoint2 = Instantiate(ClustereffectPrefab, new Vector3(ap + 10 * i, 0, ap), Quaternion.identity);//東
            GameObject Attack4effectbpoint3 = Instantiate(ClustereffectPrefab, new Vector3(ap, 0, ap - 10 * i), Quaternion.identity);//南
            GameObject Attack4effectbpoint4 = Instantiate(ClustereffectPrefab, new Vector3(ap - 10 * i, 0, ap), Quaternion.identity);//西
            Destroy(Attack4effectbpoint1, 2f);
            Destroy(Attack4effectbpoint2, 2f);
            Destroy(Attack4effectbpoint3, 2f);
            Destroy(Attack4effectbpoint4, 2f);

        }
        //Debug.Log("攻撃Ⅳ");
    }
}
