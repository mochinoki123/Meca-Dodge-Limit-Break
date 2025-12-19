using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;


public class LimitBreak : MonoBehaviour
{
    [SerializeField] private int lBUseGage;
    [SerializeField] public int lBDamage;
    [SerializeField] public float lBTime;
    [SerializeField] public float lBCoolTime;
    public bool isLB = false;
    SOCDkey key;
    PlayerParry parry;

    private void Awake()
    {
        key = GetComponent<SOCDkey>();
        parry = GetComponent<PlayerParry>();
    }
    private void OnLimitBreak(InputValue value)
    {
        if (!key.isGageAction) return;
        if (isLB) return;
        AttackLimitBreak();
    }
    void AttackLimitBreak()
    {
        if (GameManager.Instance.GetterGage() >= lBUseGage)
        {
            isLB = true;
            GameManager.Instance.UseGage(lBUseGage);
            StartCoroutine(parry.Parry());
            isLB = false;
        }
    }
}
