using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Threading.Tasks;

public class PlayerPulseDiffuser : MonoBehaviour
{
    [SerializeField] private int pulseDiffuserUseGage;
    [SerializeField] private GameObject pulseDiffuser;
    [SerializeField] private float pulseTime;
    private bool isPulseDiffuser = false;
    public bool isGageAction = false;

    private void OnPulseDiffuser(InputValue value)
    {
        if (!isGageAction) return;
        if (isPulseDiffuser) return;
        StartCoroutine(PulseDiffuser());
    }
    private void OnGageAction(InputValue value)
    {
        isGageAction = value.isPressed;
    }
    private IEnumerator PulseDiffuser()
    {
        if (PlayerResource.Instance.GetterGage() >= pulseDiffuserUseGage)
        {
            PlayerResource.Instance.UseGage(pulseDiffuserUseGage);
            isPulseDiffuser = true;
            pulseDiffuser.SetActive(true);
            yield return new WaitForSeconds(pulseTime);
            pulseDiffuser.SetActive(false);
            isPulseDiffuser = false;
        }
    }
}
