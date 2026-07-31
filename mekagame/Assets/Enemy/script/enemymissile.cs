using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class enemymissile : MonoBehaviour
{
    [SerializeField] private AudioClip missilepointerclip;
    private AudioSource audioSource;
    private Rigidbody rb;
    public float missilespeed = 45f;
    //enemyattack enemyAttack;
    private GameObject p;
    public GameObject point;
    public bool useGravity = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        Vector3 mPos = new Vector3(transform.position.x, 0.01f, transform.position.z);
    }

    private void FixedUpdate()
    {
        
        AudioSource.PlayClipAtPoint(missilepointerclip, transform.position);
        rb.linearVelocity = Vector3.down * missilespeed;
        missile();
    }
    void missile()
    {
        Vector3 mPos = new Vector3(transform.position.x, 0.1f, transform.position.z);//着弾後の位置リセット
        
    }
    
}
