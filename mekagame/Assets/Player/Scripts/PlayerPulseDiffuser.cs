using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Threading.Tasks;

public class PlayerPulseDiffuser : MonoBehaviour
{
    [SerializeField] private int pDUseGage;
    [SerializeField] private float pDTime;
    public bool isPD = false;

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
            yield return new WaitForSeconds(pDTime);
            isPD = false;
        }
    }
}
