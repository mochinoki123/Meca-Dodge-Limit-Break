using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerParry : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    public static bool parrySuccess;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();   
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Missile"))
        {
            Destroy(other.gameObject);
            parrySuccess = true;
            PlayerResource.Instance.AddGage(50);
        }
    }
}
