using UnityEngine;
using System.Collections;

public class Lazer : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxScale;

    private void OnEnable()
    {
        StartCoroutine(Grow());
    }

    private IEnumerator Grow()
    {
        while(transform.localScale.x < maxScale)
        {
            transform.localScale = new Vector3(speed, 0, 0);
        }
        yield return null;
    }

    private void OnDisable()
    {
        transform.localScale = new Vector3(0.01f, 2f, 2f);
    }
}
