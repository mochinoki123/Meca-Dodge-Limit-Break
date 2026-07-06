using System.IO;
using UnityEngine;

public class EffectDestroy : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject,1.2f);
    }

    private void OnEnable()
    {
        StopAttack.OnPhaseClear += StopEffect;
    }

    private void OnDisable()
    {
        StopAttack.OnPhaseClear -= StopEffect;
    }

    private void StopEffect()
    {
        Destroy(gameObject);
    }
}
