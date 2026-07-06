using System;
using UnityEngine;
using System.Threading.Tasks;

public class StopAttack : MonoBehaviour
{
    [SerializeField] private GameObject laserAttack;
    [SerializeField] private GameObject missileAttack;
    [SerializeField] private ClearFlag clearFlag;

    public static event Action OnPhaseClear;

    private void Update()
    {
        if (clearFlag.IsPhaseCleared)
        {
            ReleaseAttack();
            OnStopAttack();
        }
    }

    private async void OnStopAttack()
    {
        OnStopAttack(false);
        if (!clearFlag.IsGameCleared)
        {
            await Task.Delay(3000);
            clearFlag.IsPhaseCleared = false;
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
        OnPhaseClear.Invoke();
    }
}
