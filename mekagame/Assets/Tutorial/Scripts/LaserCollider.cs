using UnityEngine;
using System.Collections;


public class LaserCollider : MonoBehaviour
{
    [SerializeField] private Transform laser;
    [SerializeField] private float maxPos = 100f;

    private void OnEnable()
    {
        StartCoroutine(GrowCollider());
    }
    public IEnumerator GrowCollider()
    {
        while (transform.position.x < maxPos)
        {
            float grow = laser.transform.localScale.x * 2f;
            transform.position = new Vector3(grow, 2f, 0f);

            yield return null;
        }
        yield return null;
        transform.localScale = new Vector3(maxPos, transform.localScale.y, transform.localScale.z);
    }
    private void OnDisable()
    {
        transform.position = new Vector3(0f, 2f, 0f);
    }
}