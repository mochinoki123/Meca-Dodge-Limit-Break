using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Threading.Tasks;

public class PlayerPulseDiffuser : MonoBehaviour
{
    [SerializeField] private int pDUseGage;
    [SerializeField] private GameObject pD;
    [SerializeField] private float pDTime;
    public bool isPD = false;
    SOCDkey key;

    private void Awake()
    {
        key = GetComponent<SOCDkey>();
    }
    //“ñ‚Â‚ÌƒL[‚Å”­“®
    private void OnPulseDiffuser(InputValue value)
    {
        if (!key.isGageAction) return;
        if (isPD) return;
        StartCoroutine(PulseDiffuser());
    }
    private IEnumerator PulseDiffuser()
    {
        if (GameManager.Instance.GetterGage() >= pDUseGage)
        {   
            GameManager.Instance.UseGage(pDUseGage);
            isPD = true;
            pD.SetActive(true);
            yield return new WaitForSeconds(pDTime);
            pD.SetActive(false);
            isPD = false;
        }
    }
}
