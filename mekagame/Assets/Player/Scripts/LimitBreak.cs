using UnityEngine;
using UnityEngine.InputSystem;

public class LimitBreak : MonoBehaviour
{
    [SerializeField]private int lBUseGage;
    [SerializeField] private float lBDamage;
    private bool isLimitBreak = false;
    SOCDkey key;

    private void Awake()
    {
        key = GetComponent<SOCDkey>();
    }
    private void OnLimitBreak(InputValue value)
    {
        if (!key.isGageAction) return;
        if (isLimitBreak) return;
    }
    void OnLimitBreak()
    {

    }
}
