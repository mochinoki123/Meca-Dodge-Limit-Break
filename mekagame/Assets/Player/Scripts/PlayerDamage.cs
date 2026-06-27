using UnityEngine;
using System.Collections;
using Unity.Cinemachine;

public class PlayerDamage : MonoBehaviour
{
    [Header("Settings")]
    // 無敵時間
    [SerializeField] private float mutekiTime = 2.0f;
    [SerializeField] private float parryInterval = 1.0f;
    // 点滅間隔
    [SerializeField] private float blinkInterval = 0.1f;

    private PlayerMove playerMove;
    private PlayerParry playerParry;
    private PlayerPulseDiffuser playerPulseDiffuser;
    private Renderer rend;
    private MaterialScript materialScript;
    private CinemachineImpulseSource playerImpulseSource;
    private LimitBreak lb;
    private Animator animator;
    private InputController inputController;

    private bool isMuteki = false;
    private bool isParry = false;


    private void OnEnable()
    {
        ObjectParry.OnParrySuccesState += OnParrySuccesState;
    }

    private void OnDisable()
    {
        ObjectParry.OnParrySuccesState -= OnParrySuccesState;
    }
    private void Awake()
    {
        // コンポーネント取得
        playerMove = GetComponent<PlayerMove>();
        playerParry = GetComponent<PlayerParry>();
        playerPulseDiffuser = GetComponent<PlayerPulseDiffuser>();
        rend = GetComponentInChildren<Renderer>();
        materialScript = GetComponent<MaterialScript>();
        playerImpulseSource = GetComponent<CinemachineImpulseSource>();
        lb = GetComponent<LimitBreak>();
        animator = GetComponent<Animator>();
        inputController = GetComponent<InputController>();
    }

    private async void OnParrySuccesState(bool parrySuccess)
    {
        if (parrySuccess)
        {
            isParry = true;
            await Awaitable.WaitForSecondsAsync(parryInterval);
            isParry = false;
        }
    }

    // 被弾可否判定
    private bool CanTakeDamage()
    {
        if (isMuteki) return false;
        if (playerMove.isRun) return false;
        if (isParry) return false;
        if (playerPulseDiffuser.isPD) return false;
        if (lb.isLB) return false;
        return true;
    }

    private void OnParticleCollision(GameObject other)
    {
        // パーティクル被弾処理
        if (!CanTakeDamage()) return;

        if (other.CompareTag("FirePoint"))
        {
            ApplyDamage();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 接触判定
        if (!CanTakeDamage()) return;

        // ミサイル処理
        if (other.CompareTag("Missile"))
        {
            var missile = other.GetComponentInParent<MissileRelease>();
            ApplyDamage();
            missile.Release();
            
        }
        // レーザー処理
        else if (other.CompareTag("LaserDamage"))
        {
            var laser = other.GetComponentInParent<ReleaseLaser>();
            ApplyDamage();
            laser.Release();
        }
    }


    private void ApplyDamage()
    {
        GameManager.Instance.Damage();
        playerImpulseSource.GenerateImpulse();

        if (GameManager.Instance.IsPlayerDead)
        {
            StopAllCoroutines();
            rend.enabled = true;
            isMuteki = false;
            inputController.DisableControls();
            animator.Play("Take 001");
            return;
        }

        StartCoroutine(MutekiRoutine());
    }

    private IEnumerator MutekiRoutine()
    {
        isMuteki = true;
        yield return StartCoroutine(MutekiMaterial()); // ← 修正
        isMuteki = false;
    }

    private IEnumerator MutekiMaterial()
    {
        materialScript.ChangeMaterial(MaterialScript.EffectType.Damage, 2f);

        // 点滅ループ
        float elapsed = 0;
        while (elapsed < mutekiTime)
        {
            rend.enabled = !rend.enabled;
            yield return new WaitForSeconds(blinkInterval);
            elapsed += blinkInterval;
        }

        // 復帰処理
        rend.enabled = true;
    }
}