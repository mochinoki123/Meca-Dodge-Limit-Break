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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyAttack = FindAnyObjectByType<enemyattack>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("AttackPoint"))
        {
            ObjectPool_Missile.Instance.MissileRelease(gameObject);
            Kill();
        }
        else if (other.CompareTag("Player") || other.CompareTag("PlayerParry"))
        {
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
        audioSource.PlayOneShot(bakuhatuclip);
        Destroy(b, 1.2f);
    }
}
