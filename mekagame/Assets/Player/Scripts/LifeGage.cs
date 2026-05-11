using UnityEngine;

public class LifeGage : MonoBehaviour
{
    [SerializeField] private GameObject lifeObj;
    [SerializeField] private int HP;

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < HP; i++)
        {
            Instantiate<GameObject>(lifeObj, transform);
        }
    }
    public void Damage()
    {
        if (transform.childCount <= 0) return; 

        Destroy(transform.GetChild(0).gameObject);

        if (transform.childCount == 0)
        {
            GameManager.Instance.Die();
        }
    }
}
