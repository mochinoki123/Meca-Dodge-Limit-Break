using UnityEngine;
using R3;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private ClearFlag isClear;
    [SerializeField] private Animator animator;

    private void OnEnable()
    {
        isClear.IsGameCleared
            .Where(cleared => cleared)
            .Subscribe(_ => animator.SetTrigger("IsClear"))
            .AddTo(this);
    }
}