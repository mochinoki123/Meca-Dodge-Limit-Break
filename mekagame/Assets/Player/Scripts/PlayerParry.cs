using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerParry : MonoBehaviour
{
    public static bool parrySuccess;

    private void Start()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Missile"))
        {
            other.gameObject.GetComponent<enemymissile>().Kill();
            parrySuccess = true;
            PlayerResource.Instance.AddGage(50);
        }
    }
}
