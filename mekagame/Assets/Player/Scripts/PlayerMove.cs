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

    bool goJump = false;
    public static bool isRun = false;        
    bool isRunCoolTime = false; 
    bool isRunKey = false;    

    void Awake() => rb = GetComponent<Rigidbody>();

    private void OnMove(InputValue value) => inputVec = value.Get<Vector2>();
    private void OnJump(InputValue value) => goJump = true;

    private void OnSprint(InputValue value)
    {
        isRunKey = value.isPressed;

        if (isRunKey)
        {
            Run();
        }
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
        float speed = (isRun ? runSpeed : walkSpeed) * inputVec.magnitude;

        movVec = new Vector3(
            inputVec.x * speed,
            rb.linearVelocity.y,
            inputVec.y * speed
        );
    }

    private void Run()
    {
        if (isRun) return;         
        if (isRunCoolTime) return;  
        StartCoroutine(RunRoutine());
    }

    private IEnumerator RunRoutine()
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
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Missile") && CompareTag("Player"))
        {
            PlayerResource.Instance.Damage();
            PlayerResource.Instance.UpdateText();
        }
    }
}
