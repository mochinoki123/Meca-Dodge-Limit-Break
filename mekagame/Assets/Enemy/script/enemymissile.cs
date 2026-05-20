using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class enemymissile : MonoBehaviour
{
    private Rigidbody rb;
    public float missilespeed = 45f;
    //enemyattack enemyAttack;
    private GameObject p;
    public GameObject point;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //enemymanager = GetComponent<enemyattack>();
        //audioSource = GetComponent<AudioSource>();
        //enemyAttack = FindAnyObjectByType<enemyattack>();
        rb = GetComponent<Rigidbody>();

        Vector3 mPos = new Vector3(transform.position.x, 0.01f, transform.position.z);
        /*
        p.transform.position = mPos;
        p.transform.rotation = Quaternion.identity;
        */
        //p.SetActive(true);

        //p = Instantiate(point, mPos, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        //transform.Translate(Vector3.down * missilespeed * Time.deltaTime);
        rb.linearVelocity = transform.up * missilespeed;
        missile();
    }
    void missile()
    {
        Vector3 mPos = new Vector3(transform.position.x, 0.1f, transform.position.z);
        //p = enemymanager.Getp();
        //p.transform.position = mPos;
        //p.transform.rotation = Quaternion.identity;
        //p.SetActive(true);
    }
    
}
