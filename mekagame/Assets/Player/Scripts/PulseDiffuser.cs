using UnityEngine;

public class PulseDiffuser : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Missile"))
        {
            other.gameObject.GetComponent<enemymissile>().Kill();
        }
    }
}
