using UnityEngine;

public class enemymissile : MonoBehaviour
{
    public GameObject point;
    public GameObject attack1missileeffectPrefab;//ミサイル攻撃のエフェクト
    private GameObject p;
    private GameObject b;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector3 mPos = new Vector3(transform.position.x, 0, transform.position.z);
        p = Instantiate(point, mPos, Quaternion.identity);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("AttackPoint"))
        {
            b = Instantiate(attack1missileeffectPrefab, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.identity);
            Kill();
        }
    }

    public void Kill()
    {
        Destroy(p);
        Destroy(gameObject);
        Destroy(b,1f);
    }
}
