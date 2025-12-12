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
    [SerializeField] private float parryTime;
    [SerializeField] private float parryCoolTime;
    bool isParry = false;
    bool isParryCoolTime = false;
    public bool notMove = false;
    public bool isParryCommand = false;
    SOCDkey key;
    private void Awake()
    {
        key = GetComponent<SOCDkey>();
    }
    private void OnParry(InputValue value)
    {
        if (key.isGageAction) return;
        if (isParry) return;
        if (isParryCoolTime) return;
        if (Input.GetKeyDown(KeyCode.LeftControl)) return;
        StartCoroutine(Parry());
    }
    private IEnumerator Parry()
    {
        playerParry.SetActive(true);
        yield return new WaitForSeconds(parryTime);
        isParry = true;
        if (!ObjectParry.parrySuccess) notMove = true;
        ObjectParry.parrySuccess = false;
        playerParry.SetActive(false);
        isParryCoolTime = true;
        yield return new WaitForSeconds(parryCoolTime);
        isParry = false;
        isParryCoolTime = false;
        notMove = false;
    }
}
