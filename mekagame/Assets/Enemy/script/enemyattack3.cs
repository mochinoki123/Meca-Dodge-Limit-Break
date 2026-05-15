using UnityEngine;

public class enemyattack3 : MonoBehaviour
{
    private enemyattack enemymanager;
    [Header("UŒ‚‡V")]
    [SerializeField] int attack3missilex = 6;//UŒ‚‡Vx‚Ì”ÍˆÍİ’è
    [SerializeField] int attack3missiley = 6;//UŒ‚‡V+‚Ì”ÍˆÍİ’è
    [SerializeField] int attackpointx = 10;//UŒ‚”­¶‚Ì‰¡
    [SerializeField] int attackpointy = 60;//UŒ‚”­¶‚Ì‚‚³
    [SerializeField] int attackpointz = 10;//UŒ‚”­¶‚Ì‰œs
    float attackbunki;//random’lŠm”F—pŠî–{g‚í‚È‚¢
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemymanager = GetComponent<enemyattack>();
        Invoke("Attack3", 8f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //-----UŒ‚‡V-----
    public void Attack3()
    {
        attackbunki = Random.Range(0f, 1f);//UŒ‚•ªŠò

        if (attackbunki < 0.5f)//ƒNƒƒXŒ^
        {
            GameObject objm3 = ObjectPool_Missile.Instance.GetMissile();
            objm3.transform.position = new Vector3(0, attackpointy, 0);
            objm3.transform.rotation = Quaternion.Euler(180, 0, 0);
            //Instantiate(missile, new Vector3(0, attackpointy, 0), Quaternion.Euler(180, 0, 0));//’†S’n“_”­Ë
            for (int i = 1; i <= attack3missilex; i++)//ƒNƒƒX‚É‚È‚é‚æ‚¤‚ÉŒJ‚è•Ô‚·
            {
                /*
                Instantiate(missile, new Vector3(attackpointx - i * attackf, attackpointy, attackpointz - i * attackf), Quaternion.identity);
                //Instantiate(attackpoint, new Vector3(attackpointx - i * attackf, 0, attackpointz - i * attackf), Quaternion.identity);
                Instantiate(missile, new Vector3(attackpointx - i * attackf, attackpointy, attackpointz + i * attackf), Quaternion.identity);
                //Instantiate(attackpoint, new Vector3(attackpointx - i * attackf, 0, attackpointz + i * attackf), Quaternion.identity);
                Instantiate(missile, new Vector3(attackpointx + i * attackf, attackpointy, attackpointz - i * attackf), Quaternion.identity);
                //Instantiate(attackpoint, new Vector3(attackpointx + i * attackf, 0, attackpointz - i * attackf), Quaternion.identity);
                Instantiate(missile, new Vector3(attackpointx + i * attackf, attackpointy, attackpointz + i * attackf), Quaternion.identity);
                //Instantiate(attackpoint, new Vector3(attackpointx + i * attackf, 0, attackpointz + i * attackf), Quaternion.identity);
                */

                /*
                Instantiate(missile, new Vector3(attackpointx * i, attackpointy, -attackpointz * i), Quaternion.Euler(180, 0, 0));
                Instantiate(missile, new Vector3(-attackpointx * i, attackpointy, attackpointz * i), Quaternion.Euler(180, 0, 0));
                Instantiate(missile, new Vector3(attackpointx * i, attackpointy, attackpointz * i), Quaternion.Euler(180, 0, 0));
                Instantiate(missile, new Vector3(-attackpointx * i, attackpointy, -attackpointz * i), Quaternion.Euler(180, 0, 0));
                */
                GameObject m3ldx = ObjectPool_Missile.Instance.GetMissile();
                m3ldx.transform.position = new Vector3(attackpointx * i, attackpointy, -attackpointz * i);//@¶‰º
                m3ldx.transform.rotation = Quaternion.Euler(180, 0, 0);
                m3ldx.SetActive(true);
                GameObject m3rdx = ObjectPool_Missile.Instance.GetMissile();
                m3rdx.transform.position = new Vector3(-attackpointx * i, attackpointy, attackpointz * i);//@‰Eã
                m3rdx.transform.rotation = Quaternion.Euler(180, 0, 0);
                m3rdx.SetActive(true);
                GameObject m3lux = ObjectPool_Missile.Instance.GetMissile();
                m3lux.transform.position = new Vector3(attackpointx * i, attackpointy, attackpointz * i);//@¶ã
                m3lux.transform.rotation = Quaternion.Euler(180, 0, 0);
                m3lux.SetActive(true);
                GameObject m3rux =  ObjectPool_Missile.Instance.GetMissile();
                m3rux.transform.position = new Vector3(-attackpointx * i, attackpointy, -attackpointz * i);//@‰E‰º
                m3rux.transform.rotation = Quaternion.Euler(180, 0, 0);
                m3rux.SetActive(true);
            }
            //Debug.Log("UŒ‚‡Vx");
        }
        else//\šŒ^
        {
            GameObject objm3 = ObjectPool_Missile.Instance.GetMissile();
            objm3.transform.position = new Vector3(0, attackpointy, 0);
            objm3.transform.rotation = Quaternion.Euler(180, 0, 0);
            //Instantiate(missile, new Vector3(0, attackpointy, 0), Quaternion.Euler(180, 0, 0));//’†S’n“_”­Ë
            for (int i = 1; i < attack3missiley; i++)//\š‚É‚È‚é‚æ‚¤‚ÉŒJ‚è•Ô‚·
            {
                /*
                Instantiate(missile, new Vector3(0, attackpointy, attackpointz3 - i * attackf), Quaternion.identity);
                //Instantiate(attackpoint, new Vector3(0, 0, attackpointz3ty - i * attackf), Quaternion.identity);
                Instantiate(missile, new Vector3(attackpointx3 - i * attackf, attackpointy, 0), Quaternion.identity);
                //Instantiate(attackpoint, new Vector3(attackpointx3ty - i * attackf, 0, 0), Quaternion.identity);
                */
                /*
                Instantiate(missile, new Vector3(0 , attackpointy, -attackpointz * i), Quaternion.Euler(180, 0, 0));
                Instantiate(missile, new Vector3(-attackpointx * i, attackpointy, 0), Quaternion.Euler(180, 0, 0));
                Instantiate(missile, new Vector3(0, attackpointy, attackpointz * i), Quaternion.Euler(180, 0, 0));
                Instantiate(missile, new Vector3(attackpointx * i, attackpointy, 0), Quaternion.Euler(180, 0, 0));
                */
                GameObject m3d =    ObjectPool_Missile.Instance.GetMissile();
                m3d.transform.position = new Vector3(0, attackpointy, -attackpointz * i);//“ì
                m3d.transform.rotation = Quaternion.Euler(180, 0, 0);
                m3d.SetActive(true);
                GameObject m3l =        ObjectPool_Missile.Instance.GetMissile();
                m3l.transform.position = new Vector3(-attackpointx * i, attackpointy, 0);//¼
                m3l.transform.rotation = Quaternion.Euler(180, 0, 0);
                m3l.SetActive(true);
                GameObject m3u =    ObjectPool_Missile.Instance.GetMissile();
                m3u.transform.position = new Vector3(0, attackpointy, attackpointz * i);//–k
                m3u.transform.rotation = Quaternion.Euler(180, 0, 0);
                m3u.SetActive(true);
                GameObject m3r = ObjectPool_Missile.Instance.GetMissile();  
                m3r.transform.position = new Vector3(attackpointx * i, attackpointy, 0);//“Œ
                m3r.transform.rotation = Quaternion.Euler(180, 0, 0);
                m3r.SetActive(true);

            }
            //Debug.Log("UŒ‚‡V+");
        }
        //Debug.Log("UŒ‚‡V");
    }
}
