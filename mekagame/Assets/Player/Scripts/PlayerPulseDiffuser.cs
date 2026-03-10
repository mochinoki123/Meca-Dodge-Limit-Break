using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerPulseDiffuser : MonoBehaviour
{
    // 消費ゲージ量
    [SerializeField] private int pDUseGage;
    // 効果時間
    [SerializeField] private float pDTime;

    public bool isPD = false;
    private MaterialScript materialScript;

    private void Awake()
    {
        materialScript = GetComponent<MaterialScript>();
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
        if (GameManager.Instance.NowGage >= pDUseGage)
        {
            // ゲージ消費して発動
            GameManager.Instance.UseGage(pDUseGage);
            isPD = true;

            materialScript.ChangeMaterial(MaterialScript.EffectType.Pulse, pDTime);

            // 効果時間待機
            yield return new WaitForSeconds(pDTime);

            // 終了
            isPD = false;
        }
    }
}