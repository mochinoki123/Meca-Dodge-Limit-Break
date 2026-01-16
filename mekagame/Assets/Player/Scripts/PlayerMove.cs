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

    Rigidbody rb;
    Vector2 inputVec;
    Vector3 movVec;
    private float notSpeed = 0;
    PlayerParry parry;
    OverClock oc;
    Animator animator;

    bool goJump = false;
    public static bool isRun = false;
    bool isRunCoolTime = false;

    void Awake()
    {
        isRun = false;
        rb = GetComponent<Rigidbody>();
        parry = GetComponent<PlayerParry>();
        oc = GetComponent<OverClock>();
        animator = GetComponent<Animator>();
    }
    private void OnMove(InputValue value) => inputVec = value.Get<Vector2>();
    private void OnJump(InputValue value) => goJump = true;
    private void OnSprint(InputValue value)
    {
        if (isRun) return;
        if (isRunCoolTime) return;
        StartCoroutine(Run());
    }
    void Update()
    {
        Move();
        Rotate();
        CheckGround();

    }
    private void FixedUpdate()
    {
        Jump();
        rb.linearVelocity = movVec;
    }
    private void Move()
    {
        float baseSpeed = (parry.notMove, isRun, oc.isOC) switch
        {
            (true, _, _) => notSpeed,    // 1. “®‚¯‚È‚¢ (Å—Dæ)
            (_, true, true) => oc.oCSpeed,  // 3. ‘–‚Á‚Ä‚¢‚ÄA‚©‚ÂOC’† 
            (_, true, _) => runSpeed,    // 2. (OC‚Å‚Í‚È‚­) ‚½‚¾‘–‚Á‚Ä‚¢‚é
            _ => walkSpeed    // ‚»‚Ì‘¼ (•à‚«A‚Ü‚½‚Í—§‚¿Ž~‚Ü‚Á‚ÄOC’†‚È‚Ç)
        };

        float speed = baseSpeed * inputVec.magnitude;
        movVec = new Vector3(
            inputVec.x * speed,
            rb.linearVelocity.y,
            inputVec.y * speed
        );
        float animSpeed = Mathf.Abs( speed );
        animator.SetFloat("AnimSpeed", animSpeed);
    }
    private IEnumerator Run()
    {
        isRun = true;

        yield return new WaitForSeconds(runTime);

        isRun = false;

        isRunCoolTime = true;

        float coolTime = oc.isOC ? oc.oCCoolTime : runCoolTime;
        yield return new WaitForSeconds(coolTime);
        isRunCoolTime = false;
    }
    private void Rotate()
    {
        Vector3 lookDir = new Vector3(movVec.x, 0, movVec.z);
        if (lookDir.sqrMagnitude > 0.001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(lookDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 10f);
        }
    }
    private bool CheckGround()
    {
        return Physics.Linecast(
            transform.position + (Vector3.up * 0.01f),
            transform.position - (Vector3.up * 0.2f)
        );
    }
    private void Jump()
    {
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
