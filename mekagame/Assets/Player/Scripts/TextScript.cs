using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;


public class TextScript : MonoBehaviour
{
    [SerializeField] private GameObject camera;
    [SerializeField] private TextMeshProUGUI graze;
    [SerializeField] private TextMeshProUGUI parry;

    public enum EffectType
    {
        Graze,
        Parry,
        All
    }
    private void Start()
    {
        Removed(EffectType.All);
    }
    private void Update()
    {
        graze.transform.LookAt(camera.transform);
        parry.transform.LookAt(camera.transform);
        parry.transform.Rotate(0, 180, 0);
        graze.transform.Rotate(0, 180, 0);
    }
    public void Set(EffectType type)
    {
        if (type == EffectType.Graze || type == EffectType.All) graze.alpha = 1f;
        if (type == EffectType.Parry || type == EffectType.All) parry.alpha = 1f;
    }
    public void Removed(EffectType type)
    {
        if(type == EffectType.Graze || type == EffectType.All) graze.alpha = 0f;
        if(type == EffectType.Parry || type == EffectType.All) parry.alpha = 0f;
    }
}
