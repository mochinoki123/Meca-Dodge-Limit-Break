using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using R3;

public class PlayerPulseDiffuser : MonoBehaviour
{
    // 効果時間
    [SerializeField] private float pDTime;
    
    private MaterialScript materialScript;

    private Animator animator;
    private OverClock overclock;

    private readonly ReactiveProperty<bool> isPD = new ReactiveProperty<bool>(false);

    public ReadOnlyReactiveProperty<bool> IsPD => isPD;

    private void Awake()
    {
        materialScript = GetComponent<MaterialScript>();
        animator = GetComponent<Animator>();
        overclock = GetComponent<OverClock>();

        overclock.IsOC
            .Where(isOC => isOC == true)
            .Subscribe(isOC =>
            {
                StopAllCoroutines();
                isPD.Value = false;
            })
            .AddTo(this);
    }

    private void OnPulseDiffuser(InputValue value)
    {
        // 既に使用中なら中断
        if (isPD.Value) return;
        StartCoroutine(PulseDiffuser());
    }

    private IEnumerator PulseDiffuser()
    {
        // ゲージ残量チェック
        if (GameManager.Instance.NowGage >= GameManager.Instance.GetterUseGauge(GameManager.UseGaugeState.PulseDiffuser))
        {
            // ゲージ消費して発動
            GameManager.Instance.UseGaugeStateBranch(GameManager.UseGaugeState.PulseDiffuser);
            isPD.Value = true;

            animator?.SetTrigger("IsPD");

            materialScript.ChangeMaterial(MaterialScript.EffectType.Pulse, pDTime);

            // 効果時間待機
            yield return new WaitForSeconds(pDTime);

            // 終了
            isPD.Value = false;
        }
    }
}