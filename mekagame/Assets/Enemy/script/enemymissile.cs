using UnityEngine;
using UnityEngine.Audio;

public class enemymissile : MonoBehaviour
{
    [Header("効果音")]
    [SerializeField] private AudioClip bakuhatuclip;
    private AudioSource audioSource;
    public GameObject point;
    public GameObject attack1missileeffectPrefab;//ミサイル攻撃のエフェクト
    private GameObject p;
    private GameObject b;
    public float missilespeed = 10f;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        Vector3 mPos = new Vector3(transform.position.x, 0, transform.position.z);
        p = Instantiate(point, mPos, Quaternion.identity);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * missilespeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AttackPoint"))
        {
            b = Instantiate(attack1missileeffectPrefab, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.identity);
            Kill();
        }
        audioSource.PlayOneShot(bakuhatuclip);
    }

    public void Kill()
    {
        Destroy(p);
        Destroy(gameObject);
        Destroy(b,1.2f);
    }
}
