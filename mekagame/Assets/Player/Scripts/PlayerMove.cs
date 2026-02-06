using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float runTime;
    [SerializeField] private float runCoolTime;
    [SerializeField] private float jumpForce;
    [SerializeField] private AudioClip dash;

    Rigidbody rb;
    Vector2 inputVec;
    Vector3 movVec;
    private float notSpeed = 0;
    PlayerParry parry;
    OverClock oc;
    Animator animator;
    AudioSource audioSource;

    bool goJump = false;
    public bool isRun = false;
    bool isRunCoolTime = false;

    void Awake()
    {
        // 初期化
        isRun = false;
        rb = GetComponent<Rigidbody>();
        parry = GetComponent<PlayerParry>();
        oc = GetComponent<OverClock>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Input System コールバック
    private void OnMove(InputValue value) => inputVec = value.Get<Vector2>();
    private void OnJump(InputValue value) => goJump = true;
    private void OnSprint(InputValue value)
    {
        // ダッシュ実行判定
        if (isRun) return;
        if (isRunCoolTime) return;
        StartCoroutine(Run());
    }

    void Update()
    {
        // 毎フレームの移動計算・回転・接地確認
        Move();
        Rotate();
        CheckGround();
    }

    private void FixedUpdate()
    {
        // 物理挙動の適用
        Jump();
        rb.linearVelocity = movVec;
    }

    private void Move()
    {
        // 状態に応じた速度決定
        float baseSpeed = (parry.notMove, isRun, oc.isOC) switch
        {
            (true, _, _) => notSpeed,    // 硬直中
            (_, true, true) => oc.oCSpeed,  // OC中のダッシュ
            (_, true, _) => runSpeed,    // 通常ダッシュ
            _ => walkSpeed    // 歩き
        };

        // ベクトル計算
        float speed = baseSpeed * inputVec.magnitude;
        movVec = new Vector3(
            inputVec.x * speed,
            rb.linearVelocity.y,
            inputVec.y * speed
        );

        // アニメーション速度更新
        float animSpeed = Mathf.Abs(speed);
        animator.SetFloat("AnimSpeed", animSpeed);
    }

    private IEnumerator Run()
    {
        // ダッシュ開始
        audioSource.PlayOneShot(dash);
        isRun = true;

        yield return new WaitForSeconds(runTime);

        // ダッシュ終了・クールタイム開始
        isRun = false;
        isRunCoolTime = true;

        // OC中か通常かでクールタイム変化
        float coolTime = oc.isOC ? oc.oCCoolTime : runCoolTime;
        yield return new WaitForSeconds(coolTime);
        isRunCoolTime = false;
    }

    private void Rotate()
    {
        // 移動方向へ回転
        Vector3 lookDir = new Vector3(movVec.x, 0, movVec.z);
        if (lookDir.sqrMagnitude > 0.001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(lookDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 10f);
        }
    }

    private bool CheckGround()
    {
        // 接地判定
        return Physics.Linecast(
            transform.position + (Vector3.up * 0.01f),
            transform.position - (Vector3.up * 0.2f)
        );
    }

    private void Jump()
    {
        // ジャンプ実行
        if (goJump)
        {
            if (CheckGround())
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
            goJump = false;
        }
    }
}