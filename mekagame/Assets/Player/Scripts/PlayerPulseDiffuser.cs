using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Threading.Tasks;

public class PlayerPulseDiffuser : MonoBehaviour
{
    [SerializeField] private int pDUseGage;
    [SerializeField] private float pDTime;
    [SerializeField] private Material pulseColor;
    [SerializeField] private Material originalColor;
    public bool isPD = false;
    private Renderer rend;

    private void Awake()
    {
        rend = GetComponentInChildren<Renderer>();
    }
    private void OnPulseDiffuser(InputValue value)
    {
        if (isPD) return;
        StartCoroutine(PulseDiffuser());
    }
    private IEnumerator PulseDiffuser()
    {
        if (GameManager.Instance.GetterGage() >= pDUseGage)
        {   
            GameManager.Instance.UseGage(pDUseGage);
            isPD = true;
            rend.material.color = pulseColor.color;
            yield return new WaitForSeconds(pDTime);
            isPD = false;
            rend.material.color = originalColor.color;
        }
    }
}
