using UnityEngine;
using System.Collections;

public class AoE : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxScale;

    private void OnEnable()
    {
        StartCoroutine(Grow());
    }

    private IEnumerator Grow()
    {
        yield return new WaitForSeconds(0.1f);
        while (transform.localScale.z < maxScale)
        {
            float newX = transform.localScale.x + (speed * Time.deltaTime);

            transform.localScale = new Vector3(newX, transform.localScale.y, transform.localScale.z);

            yield return null;
        }

        transform.localScale = new Vector3(maxScale, transform.localScale.y, transform.localScale.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            StopAllCoroutines();
        }
    }

    private void OnDisable()
    {
        transform.localScale = new Vector3(0f, 0.1f, 3f);
    }
}
