using UnityEngine;

public class Animation : MonoBehaviour
{
    [SerializeField] private ClearFlag isClear;
    [SerializeField] private Animator animator;

    private void Update()
    {
        if (isClear) animator.SetTrigger("IsClear");
    }
}
