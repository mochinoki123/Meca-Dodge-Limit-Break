using UnityEngine;
using UnityEngine.InputSystem;

public class OverClock : MonoBehaviour
{
    [SerializeField] private int overClockUseGage;
    private bool isOverClock = false;

    private void OnOverClock(InputValue value)
    {
        if (isOverClock) return;
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
