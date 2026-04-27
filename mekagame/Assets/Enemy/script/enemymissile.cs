using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Splines;

public class enemymissile : MonoBehaviour
{
    enemyattack enemymanager;
    [Header("効果音")]
    [SerializeField] private AudioClip bakuhatuclip;
    private AudioSource audioSource;
    //public GameObject point;
    public GameObject attack1missileeffectPrefab;//ミサイル攻撃のエフェクト
    private GameObject p;
    private GameObject b;
    public float missilespeed = 45f;
    enemyattack enemyAttack;

    void Awake()
    {
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemymanager = GetComponent<enemyattack>();
        audioSource = GetComponent<AudioSource>();
        enemyAttack = FindAnyObjectByType<enemyattack>();
        /*
        Vector3 mPos = new Vector3(transform.position.x, 0.1f, transform.position.z);
        p.transform.position = mPos;
        p.transform.rotation = Quaternion.identity;
        p.SetActive(true);
        */
        //p = Instantiate(point, mPos, Quaternion.identity);
        missile();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * missilespeed * Time.deltaTime);
    }
    void missile()
    {
        Vector3 mPos = new Vector3(transform.position.x, 0.1f, transform.position.z);
        p = enemymanager.Getp();
        p.transform.position = mPos;
        p.transform.rotation = Quaternion.identity;
        p.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AttackPoint"))
        {
            b = Instantiate(attack1missileeffectPrefab, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.identity);
            audioSource.PlayOneShot(bakuhatuclip);
            Kill();
        }
    }

    public void Kill()
    {
        Destroy(b, 1.2f);
        //Destroy(p);
        enemyAttack.Returnp(p);
        enemyAttack.Return(gameObject);
    }
}
