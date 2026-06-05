using UnityEngine;

public class ReleaseLaser : MonoBehaviour
{
    public void Release()
    {
        ObjectPool_Lazer.instance.ReleaseLaser(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            ObjectPool_Lazer.instance.ReleaseLaser(gameObject);

        }
    }
}
