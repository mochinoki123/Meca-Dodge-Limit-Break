using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerPulseDiffuser : MonoBehaviour
{
    // 効果時間
    [SerializeField] private float pDTime;
    
    public bool isPD { get; private set; }
    private MaterialScript materialScript;

    private Animator animator;

    private void Awake()
    {
        materialScript = GetComponent<MaterialScript>();
        animator = GetComponent<Animator>();
    }

    private void OnPulseDiffuser(InputValue value)
    {
        // 既に使用中なら中断
        if (isPD) return;
        StartCoroutine(PulseDiffuser());
    }

    private IEnumerator PulseDiffuser()
    {
        // ゲージ残量チェック
        if (GameManager.Instance.NowGage >= GameManager.Instance.GetterUseGauge(GameManager.UseGaugeState.PulseDiffuser))
        {
            // ゲージ消費して発動
            GameManager.Instance.UseGaugeStateBranch(GameManager.UseGaugeState.PulseDiffuser);
            isPD = true;

            animator?.SetTrigger("IsPD");

            materialScript.ChangeMaterial(MaterialScript.EffectType.Pulse, pDTime);

            // 効果時間待機
            yield return new WaitForSeconds(pDTime);

            // 終了
            isPD = false;
        }
    }
}