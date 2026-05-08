using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class enemymissile : MonoBehaviour
{
    
    public float missilespeed = 45f;
    //enemyattack enemyAttack;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //enemymanager = GetComponent<enemyattack>();
        //audioSource = GetComponent<AudioSource>();
        //enemyAttack = FindAnyObjectByType<enemyattack>();
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
        //p = enemymanager.Getp();
        //p.transform.position = mPos;
        //p.transform.rotation = Quaternion.identity;
        //p.SetActive(true);
    }

}
