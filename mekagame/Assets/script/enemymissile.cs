using UnityEngine;

public class enemymissile : MonoBehaviour
{
    public GameObject point;
    private GameObject p;
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
            Kill();
        }
    }

    public void Kill()
    {
        Destroy(p);
        Destroy(gameObject);
    }
}
