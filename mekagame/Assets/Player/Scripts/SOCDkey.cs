using UnityEngine;
using UnityEngine.InputSystem;

public class SOCDkey : MonoBehaviour
{
    public bool isGageAction = false;

    //‰Ÿ‚µ‚½‚Æ‚«true—£‚µ‚½‚Æ‚«false
    private void OnGageAction(InputValue value)
    {
        isGageAction = value.isPressed;
    }
}
