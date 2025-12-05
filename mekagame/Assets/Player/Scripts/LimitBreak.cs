using UnityEngine;
using UnityEngine.InputSystem;

public class LimitBreak : MonoBehaviour
{
    [SerializeField]private int limitBreakUseGage;

    private bool isLimitBreak = false;

    private void OnLimitBreak(InputValue value)
    {
        if (isLimitBreak) return;
    }
    void OnLimitBreak()
    {

    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
