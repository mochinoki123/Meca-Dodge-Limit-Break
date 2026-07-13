using UnityEngine;
using R3;

public class EffectDestroy : MonoBehaviour
{
    [SerializeField] private ClearFlag clearFlag;
    [SerializeField] private float lifeTime = 1.2f;

    private readonly CompositeDisposable disableDisposable = new CompositeDisposable();

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnEnable()
    {
        disableDisposable.Clear();

        clearFlag.IsPhaseCleared
            .Where(cleared => cleared)
            .Subscribe(_ =>
            {
                if (gameObject != null)
                {
                    Destroy(gameObject);
                }
            })
            .AddTo(disableDisposable); 
    }

    private void OnDisable()
    {
        disableDisposable.Dispose();
    }

    private void OnDestroy()
    {
        disableDisposable.Dispose();
    }
}