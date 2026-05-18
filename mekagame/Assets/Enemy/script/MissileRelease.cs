using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Splines;

public class MissileRelease : MonoBehaviour
{
    enemyattack enemyAttack;

    [Header("効果音")]
    [SerializeField] private AudioClip bakuhatuclip;
    private AudioSource audioSource;
    [SerializeField] GameObject attack1missileeffectPrefab;//ミサイル攻撃のエフェクト
    private GameObject b;

    public bool isDead = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyAttack = FindAnyObjectByType<enemyattack>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDead) return;

        if (other.CompareTag("Missile"))
        {
            isDead = true;
            Kill();
            ObjectPool_Missile.Instance.MissileRelease(gameObject);
            
        }
        else if (other.CompareTag("Player") || other.CompareTag("PlayerParry"))
        {
            isDead = true;
            ObjectPool_Missile.Instance.MissileRelease(gameObject);
        }

        /*
        if (other.CompareTag("PlayerParry"))
        {
            ObjectPool_Missile.Instance.MissileRelease(gameObject);
        }
        */
    }
    
    public void Kill()
    {
        b = Instantiate(attack1missileeffectPrefab, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.identity);
        AudioSource.PlayClipAtPoint(bakuhatuclip, transform.position);
        Destroy(b, 1.2f);
    }
    
}
