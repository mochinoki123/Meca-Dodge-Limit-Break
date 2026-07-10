using System;
using System.Threading.Tasks;
using UnityEngine;
using R3;

public class StopAttack : MonoBehaviour
{
    [SerializeField] private GameObject laserAttack;
    [SerializeField] private GameObject missileAttack;
    [SerializeField] private ClearFlag clearFlag;

    public static event Action OnPhaseClear;

    private void OnEnable()
    {
        clearFlag.IsPhaseCleared
            .Where(cleared => cleared)
            .Subscribe(_ =>
            {
                ReleaseAttack();
                OnStopAttack();
            })
            .AddTo(this);
    }

    private async void OnStopAttack()
    {
        OnStopAttack(false);
        if (!clearFlag.IsGameCleared.Value)
        {
            await Task.Delay(3000);
            clearFlag.IsPhaseCleared.Value = false;
            OnStopAttack(true);
        }
    }

    private void OnStopAttack(bool i)
    {
        missileAttack.SetActive(i);
        laserAttack.SetActive(i);
    }

    private void ReleaseAttack()
    {
        OnPhaseClear?.Invoke();
    }
}