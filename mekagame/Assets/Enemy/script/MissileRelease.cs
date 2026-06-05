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

    private void OnEnable()
    {
        isDead = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDead) return;

        if (
         other.CompareTag("Missile") ||
         other.CompareTag("Player") ||
         other.CompareTag("PlayerParry")
        )
        {
            isDead = true;

            if (other.CompareTag("Missile"))
            {
                Kill();
            }

            ResetMissile();

            ObjectPool_Missile.Instance.MissileRelease(gameObject);
        }
    }

    
    public void Kill()
    {
        b = Instantiate(attack1missileeffectPrefab, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.identity);
        AudioSource.PlayClipAtPoint(bakuhatuclip, transform.position);
        Destroy(b, 1.2f);
    }

    private void ResetMissile()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        if (transform.childCount > 0)
        {
            Transform missileChild = transform.GetChild(0);

            missileChild.localPosition = new Vector3(0,60,0);
            missileChild.localRotation = Quaternion.identity;

            Rigidbody rb = missileChild.GetComponent<Rigidbody>();
            
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;

                rb.useGravity = false;
                rb.position = transform.position;
                rb.rotation = transform.rotation;
                rb.Sleep();
                rb.WakeUp();
            }
        }
    }
}
