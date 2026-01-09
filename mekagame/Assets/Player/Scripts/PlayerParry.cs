using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.UIElements;


public class PlayerParry : MonoBehaviour
{
    [SerializeField] private GameObject playerParry;
    [SerializeField] private  Enemy enemy;
    [SerializeField] private float parryTime;
    [SerializeField] private float parryCoolTime;
    bool isParry = false;
    bool isParryCoolTime = false;
    public bool notMove = false;
    public bool isParryCommand = false;
    SOCDkey key;
    LimitBreak lb;
    PlayerPulseDiffuser pd;
    Animator animator;

    private void Awake()
    {
        key = GetComponent<SOCDkey>();
        lb = GetComponent<LimitBreak>();
        pd = GetComponent<PlayerPulseDiffuser>();
        animator = GetComponent<Animator>();
    }
    private void OnParry(InputValue value)
    {
        if (pd.isPD) return;
        if (key.isGageAction) return;
        if (isParry) return;
        if (isParryCoolTime) return;
        StartCoroutine(Parry());
    }
    public IEnumerator Parry()
    {
        bool isLBMode = lb.isLB;
        isParry = true;
        playerParry.SetActive(true);
        float currentDuration = (isLBMode) ? lb.lBTime : parryTime;
        yield return new WaitForSeconds(currentDuration);
        playerParry.SetActive(false);
        if (ObjectParry.parrySuccess)
        {
            if (lb != null && lb.isLB)
            {
                enemy.Damage(lb.lBDamage);
            }
        }
        else
        {
            notMove = true;
        }
        ObjectParry.parrySuccess = false;
        isParryCoolTime = true;
        isParry = false;
        float currentCoolTime = (isLBMode) ? lb.lBCoolTime : parryCoolTime;
        yield return new WaitForSeconds(currentCoolTime);
        isParryCoolTime = false;
        notMove = false;
    }
    private void Update()
    {
        animator.SetBool("LimitBreakSuccess", ObjectParry.parrySuccess);
    }
}
