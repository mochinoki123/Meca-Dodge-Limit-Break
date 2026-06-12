using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PressedButton : MonoBehaviour
{
    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnSubmit(BaseEventData eventData)
    {
        animator?.Play("Pressed");
    }
}
