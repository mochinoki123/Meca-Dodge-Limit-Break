using UnityEngine;

public class ReleaseLaser : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ObjectPool_Lazer.instance.ReleaseLaser(gameObject);
        }
        if (other.CompareTag("Wall"))
        {
            ObjectPool_Lazer.instance.ReleaseLaser(gameObject);
        }
    }
}
