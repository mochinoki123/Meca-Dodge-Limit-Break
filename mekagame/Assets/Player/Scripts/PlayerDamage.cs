using UnityEngine;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine.SceneManagement;

public class PlayerDamage : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float mutekiTime = 2.0f;
    [SerializeField] private float parryInterval = 1.0f;
    [SerializeField] private float lbInterval = 4.0f;
    [SerializeField] private float blinkInterval = 0.1f;

    [SerializeField] private ClearFlag clearFlag;

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
    private bool isLB = false;
    private bool isTutorial = false;
    private int damageCount = 0;

    // Awaitableのキャンセル用
    private System.Threading.CancellationTokenSource destroyCts;

    private void OnEnable()
    {
        damageCount = 0;
        ObjectParry.OnParrySuccesState += OnParrySuccesState;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        ObjectParry.OnParrySuccesState -= OnParrySuccesState;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Awake()
    {
        destroyCts = new System.Threading.CancellationTokenSource();

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

    private void OnDestroy()
    {
        // オブジェクト破棄時に非同期処理を安全にキャンセル
        destroyCts?.Cancel();
        destroyCts?.Dispose();
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        isTutorial = (scene.name == "Tutorial");
    }

    private async void OnParrySuccesState(bool parrySuccess)
    {
        if (parrySuccess && !isParry)
        {
            try
            {
                isParry = true;
                // CancellationTokenを渡して、オブジェクト破棄時のエラーを防ぐ
                await Awaitable.WaitForSecondsAsync(parryInterval, destroyCts.Token);
                isParry = false;
            }
            catch (System.OperationCanceledException)
            {
                // キャンセル時は何もしない
            }
        }
    }

    private void Update()
    {
        // 修正：毎フレーム await が走らないよう、フラグの変化の瞬間だけ非同期処理を呼ぶ
        if (lb.isLB && !isLB)
        {
            StartLimitBreakTimer();
        }
    }

    private async void StartLimitBreakTimer()
    {
        try
        {
            isLB = true;
            await Awaitable.WaitForSecondsAsync(lbInterval, destroyCts.Token);
        }
        catch (System.OperationCanceledException)
        {
            // キャンセルハンドリング
        }
        finally
        {
            isLB = false;
        }
    }

    private bool CanTakeDamage()
    {
        if (clearFlag.IsGameCleared.Value) return false;
        if (isMuteki) return false;
        if (playerMove.isRun) return false;
        if (isParry) return false;
        if (playerPulseDiffuser.IsPD.CurrentValue) return false;
        if (isLB) return false;
        return true;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (!CanTakeDamage()) return;

        if (other.CompareTag("FirePoint"))
        {
            ApplyDamage();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // チュートリアルかつ被弾上限なら処理しない
        if (isTutorial && damageCount >= 4) return;
        if (!CanTakeDamage()) return;

        if (other.CompareTag("Missile"))
        {
            ApplyDamage();
            if (other.TryGetComponentInParent<MissileRelease>(out var missile))
            {
                missile.Release();
            }
        }
        else if (other.CompareTag("LaserDamage"))
        {
            ApplyDamage();
            if (other.TryGetComponentInParent<ReleaseLaser>(out var laser))
            {
                laser.Release();
            }
        }
    }

    private void ApplyDamage()
    {
        damageCount++;
        GameManager.Instance.Damage();
        playerImpulseSource.GenerateImpulse();

        if (GameManager.Instance.IsPlayerDead)
        {
            StopAllCoroutines();
            rend.enabled = true;
            isMuteki = false;
            inputController.DisableControls();
            animator.Play("Down");
            return;
        }

        StartCoroutine(MutekiRoutine());
    }

    private IEnumerator MutekiRoutine()
    {
        isMuteki = true;
        yield return StartCoroutine(MutekiMaterial());
        isMuteki = false;
    }

    private IEnumerator MutekiMaterial()
    {
        materialScript.ChangeMaterial(MaterialScript.EffectType.Damage, 2f);

        float elapsed = 0;
        while (elapsed < mutekiTime)
        {
            rend.enabled = !rend.enabled;
            yield return new WaitForSeconds(blinkInterval);
            elapsed += blinkInterval;
        }

        rend.enabled = true;
    }
}

// 拡張メソッド用（GetComponentInParentのNullable対策をスッキリさせる場合）
public static class ComponentExtensions
{
    public static bool TryGetComponentInParent<T>(this Collider collider, out T component) where T : Component
    {
        component = collider.GetComponentInParent<T>();
        return component != null;
    }
}