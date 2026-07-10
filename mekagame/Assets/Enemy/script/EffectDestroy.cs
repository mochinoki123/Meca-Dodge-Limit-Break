using UnityEngine;
using R3;

public class EffectDestroy : MonoBehaviour
{
    [SerializeField] private ClearFlag clearFlag;
    [SerializeField] private float lifeTime = 1.2f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnEnable()
    {
        clearFlag.IsPhaseCleared
            .Where(cleared => cleared)
            .Subscribe(_ => Destroy(gameObject))
            .AddTo(this);
    }
}