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
        if (other.CompareTag("AttackPoint"))
        {
            enemyAttack.Return(gameObject);
            b = Instantiate(attack1missileeffectPrefab, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.identity);
            audioSource.PlayOneShot(bakuhatuclip);
            Destroy(b, 1.2f);
        }
        if (other.CompareTag("Player"))
        {
            enemyAttack.Return(gameObject);
        }
        if (other.CompareTag("PlayerParry"))
        {
            enemyAttack.Return(gameObject);
        }
    }



    public void Kill()
    {
        Destroy(b, 1.2f);
        //Destroy(p);
        //enemyAttack.Returnp(p);
        enemyAttack.Return(gameObject);
    }
}
