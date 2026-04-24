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
        while (transform.localScale.x < maxScale)
        {
            float newX = transform.localScale.x + (speed * Time.deltaTime);

            transform.localScale = new Vector3(newX, transform.localScale.y, transform.localScale.z);

            yield return null;
        }

        transform.localScale = new Vector3(maxScale, transform.localScale.y, transform.localScale.z);
    }

    private void OnDisable()
    {
        transform.localScale = new Vector3(0.01f, 2f, 2f);
    }
}
