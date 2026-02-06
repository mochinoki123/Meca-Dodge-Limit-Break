using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField] private float mutekiTime;
    [SerializeField] private int loopCount;
    private PlayerMove playerMove;
    private PlayerParry playerParry;
    private PlayerPulseDiffuser playerPulseDiffuser;
    private Renderer rend;
    private MaterialScript materialScript;

    private bool isMuteki = false;
    private float alpha_Sin;

    private void Awake()
    {
        // 各コンポーネント取得
        playerMove = GetComponent<PlayerMove>();
        playerParry = GetComponent<PlayerParry>();
        playerPulseDiffuser = GetComponent<PlayerPulseDiffuser>();
        rend = GetComponentInChildren<Renderer>();
        materialScript = GetComponent<MaterialScript>();
    }

    private void Update()
    {
        // 明滅計算
        alpha_Sin = Mathf.Sin(Time.time) / 2 + 0.5f;
    }

    private void OnTriggerEnter(Collider other)
    {
        // ダメージ無効状態なら処理しない
        if (playerMove.isRun) return;
        if (playerParry.isParry) return;
        if (playerPulseDiffuser.isPD) return;
        if (isMuteki) return;

        bool isDamage = false;

        // ミサイル接触判定
        if (other.CompareTag("Missile"))
        {
            var missile = other.GetComponentInParent<enemymissile>();
            if (missile != null)
            {
                missile.Kill();
                GameManager.Instance.Damage();
                isDamage = true;
            }
        }

        // レーザー接触判定
        if (other.CompareTag("Lazer"))
        {
            var lazer = other.GetComponentInParent<enemylazer>();
            if (lazer != null)
            {
                lazer.Kill();
                GameManager.Instance.Damage();
                isDamage = true;
            }
        }

        // 炎接触判定
        if (other.CompareTag("FirePoint"))
        {
            GameManager.Instance.Damage();
            isDamage = true;
        }

        // ダメージ発生時の無敵処理開始
        if (isDamage)
        {
            StartCoroutine(MutekiTime());
        }
    }

    private IEnumerator MutekiTime()
    {
        // 無敵開始
        isMuteki = true;
        //色変更
        materialScript.ChangeMaterial(MaterialScript.EffectType.Damage, 2f);
        // 点滅ループ
        for (int i = 0; i < loopCount; i++)
        {
            rend.enabled = false;
            yield return new WaitForSeconds(0.1f);

            rend.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }

        // 無敵終了
        rend.enabled = true;
        isMuteki = false;
    }
}