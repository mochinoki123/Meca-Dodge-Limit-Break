using UnityEngine;
using UnityEngine.InputSystem;

public abstract class InputTutorialTask : ITutorialTask
{
    public abstract string Title { get; }
    public abstract string Description { get; }
    public virtual float TransitionTime => 2.0f;

    protected PlayerInput playerInput;

    protected InputTutorialTask(PlayerInput playerInput)
    {
        this.playerInput = playerInput;
    }

    public virtual void OnTaskSet() { }
    public virtual void OnTaskEnd() { }
    public virtual void Tick() { }
    public abstract bool IsCompleted();

    public virtual string GetProgress() => "";

    protected InputAction GetAction(string actionName)
    {
        if (playerInput == null)
        {
            Debug.LogWarning("[TutorialTask] PlayerInput‚ª–¢İ’è‚Å‚·");
            return null;
        }

        var action = playerInput.actions.FindAction(actionName);
        if (action == null)
            Debug.LogWarning($"[TutorialTask] ƒAƒNƒVƒ‡ƒ“ '{actionName}' ‚ªŒ©‚Â‚©‚è‚Ü‚¹‚ñ");

        return action;
    }
}