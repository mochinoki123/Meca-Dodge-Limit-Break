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
    [SerializeField] private GameObject playerParry;
    [SerializeField] private float parryTime;
    [SerializeField] private float parryCoolTime;

    Rigidbody rb;
    Vector2 inputVec;
    Vector3 movVec;
    private float notSpeed = 0;

    bool goJump = false;
    public static bool isRun = false;
    bool isRunCoolTime = false;
    bool isParry = false;
    bool isParryCoolTime = false;
    bool notMove = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnMove(InputValue value) => inputVec = value.Get<Vector2>();
    private void OnJump(InputValue value) => goJump = true;
    private void OnParry(InputValue value)
    {
        if (isParry) return;
        if (isParryCoolTime) return;
        playerParry.SetActive(true);
        StartCoroutine(Parry());
    }
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
        float baseSpeed = notMove ? notSpeed : (isRun ? runSpeed : walkSpeed);

        float speed = baseSpeed * inputVec.magnitude;
        movVec = new Vector3(
            inputVec.x * speed,
            rb.linearVelocity.y,
            inputVec.y * speed
        );
    }
    private IEnumerator Run()
    {
        isRun = true;

        yield return new WaitForSeconds(runTime);

        isRun = false;

        isRunCoolTime = true;
        yield return new WaitForSeconds(runCoolTime);
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
    private IEnumerator Parry()
    {
        yield return new WaitForSeconds(parryTime);

        isParry = false;

        if(!PlayerParry.parrySuccess) notMove = true;
        PlayerParry.parrySuccess = false;
        playerParry.SetActive(false);
        isParryCoolTime = true;
        yield return new WaitForSeconds(parryCoolTime);
        isParryCoolTime = false;
        notMove = false;
    }
}
