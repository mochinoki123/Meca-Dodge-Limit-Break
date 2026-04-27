using UnityEngine;

public class enemyattack4 : MonoBehaviour
{
    private enemyattack enemymanager;
    [Header("攻撃Ⅳ")]
    [SerializeField] int attack4missile = 10;//攻撃４のミサイル範囲指定
    [SerializeField] GameObject missile4;//ミサイル攻撃のオブジェクト
    [Header("爆発ポイントプレハブ")]
    [SerializeField] GameObject bpoint;//爆発ポイント
    [SerializeField] GameObject ClustereffectPrefab;//爆発のエフェクト
    [SerializeField] int attackpointy = 60;//攻撃発生の高さ
    [Header("攻撃範囲指定")]
    [SerializeField] float rndm = -9;//フィールドごとの範囲指定マイナス
    [SerializeField] float rndp = 9;//フィールドごとの範囲指定プラス
    float ap;//random値確認用基本使わない
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemymanager = GetComponent<enemyattack>();
        Invoke("Attack4", 11f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //-----攻撃Ⅳ-----

    public void Attack4()
    {
        ap = Random.Range(rndm, rndp);//地面の広さによって変更
        
        GameObject objm4 = enemymanager.Getm();
        objm4.transform.position = new Vector3(ap, attackpointy, ap);
        objm4.transform.rotation = Quaternion.Euler(180, 0, 0);
        objm4.SetActive(true);

      //Instantiate(missile4, new Vector3(ap, attackpointy, ap), Quaternion.Euler(180, 0, 0));//初弾
        Invoke("Attack4b", 1f);
    }

    //攻撃Ⅳクラスター

    void Attack4b()
    {
        for (int i = 1; i < attack4missile; i++)
        {
            /*
            GameObject objm4bu = Getb();
            objm4bu.transform.position = new Vector3(ap, 0, ap + 10 * i);//北
            objm4bu.transform.rotation = Quaternion.Euler(180, 0, 0);
            objm4bu.SetActive(true);
            GameObject objm4br = Getb();
            objm4br.transform.position = new Vector3(ap + 10 * i, 0, ap);//東
            objm4br.transform.rotation = Quaternion.Euler(180, 0, 0);
            objm4br.SetActive(true);
            GameObject objm4bd = Getb();
            objm4bd.transform.position = new Vector3(ap, 0, ap - 10 * i);//南
            objm4bd.transform.rotation = Quaternion.Euler(180, 0, 0);
            objm4bd.SetActive(true);
            GameObject objm4bl = Getb();
            objm4bl.transform.position = new Vector3(ap - 10 * i, 0, ap);//西
            objm4bl.transform.rotation = Quaternion.Euler(180, 0, 0);
            objm4bl.SetActive(true);*/

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
            /*
            GameObject objm4beu = Getbe();
            objm4beu.transform.position = new Vector3(ap, 0, ap + 10 * i);//北
            objm4beu.transform.rotation = Quaternion.Euler(180, 0, 0);
            objm4beu.SetActive(true);
            GameObject objm4ber = Getbe();
            objm4ber.transform.position = new Vector3(ap + 10 * i, 0, ap);//東
            objm4ber.transform.rotation = Quaternion.Euler(180, 0, 0);
            objm4ber.SetActive(true);
            GameObject objm4bed = Getbe();
            objm4bed.transform.position = new Vector3(ap, 0, ap - 10 * i);//南
            objm4bed.transform.rotation = Quaternion.Euler(180, 0, 0);
            objm4bed.SetActive(true);
            GameObject objm4bel = Getbe();
            objm4bel.transform.position = new Vector3(ap - 10 * i, 0, ap);//西
            objm4bel.transform.rotation = Quaternion.Euler(180, 0, 0);
            objm4bel.SetActive(true);
            */

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
