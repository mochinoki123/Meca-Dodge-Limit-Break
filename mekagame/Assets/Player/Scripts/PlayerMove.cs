using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Diagnostics;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float walkSpeed;   // 歩くスピード
    [SerializeField] private float sprintSpeed; // 走るスピード
    [SerializeField] private float jumpForce;   // ジャンプ力
    [SerializeField] private int HP;            // プレイヤーのHP
    Rigidbody rb;
    public Vector2 inputVec;
    public Vector3 movVec;
    bool goJump = false, isRun = false, onGround = false;
    Stopwatch stopwatch = new Stopwatch();


    void Awake() => rb = GetComponent<Rigidbody>();  // Rigidbodyを取得
    private void OnMove(InputValue value) => inputVec = value.Get<Vector2>(); // 移動入力
    void OnSprint(InputValue value) => isRun = value.isPressed;  // 走る入力
    void OnJump(InputValue value) => goJump = true;  // ジャンプ入力

    void Update()
    {
        float moveSpeed = walkSpeed * inputVec.magnitude;  // 歩くスピード
        stopwatch.Start();
        if (isRun) moveSpeed = sprintSpeed * inputVec.magnitude;  // 走るスピード
        stopwatch.Stop();
        movVec = new Vector3(inputVec.x * moveSpeed, rb.linearVelocity.y, inputVec.y * moveSpeed);  // 移動ベクトル

        // 地面との接触判定
        RaycastHit hit;
        onGround = Physics.Linecast(transform.position + (Vector3.up * 0.01f), transform.position - (Vector3.up * 0.2f), out hit);
    }

    private void FixedUpdate()
    {
        if (goJump)
        {
            if (onGround)  // 地面に接触していればジャンプ
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);  // ジャンプ力を加える
            }
            goJump = false;

        }

        rb.linearVelocity = movVec;  // Rigidbodyの速度更新
    }
}
