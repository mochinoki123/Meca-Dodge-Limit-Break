using UnityEngine;

public class PulseDiffuser : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Missile"))
        {
            other.gameObject.GetComponent<enemymissile>().Kill();
        }
        if (other.gameObject.CompareTag("Lazer"))
        {
            other.gameObject.GetComponent<enemylazer>().Kill();
        }
    }
}
