using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    public void DisableControls()
    {
        playerInput.DeactivateInput();
    }

    public void EnableControls()
    {
        playerInput.ActivateInput();
    }

    private void OnEnable()
    {
        EnableControls();
    }
}
