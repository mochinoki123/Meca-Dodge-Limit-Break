using UnityEngine;
using System.Collections;


public class LaserCollider : MonoBehaviour
{
    [SerializeField] private Transform laser;

    private void OnEnable()
    {
    }
    private void Update()
    {
        float grow = laser.localScale.x * 2f;
        transform.localPosition = new Vector3(-grow, 0f, 0f);
    }
    private void OnDisable()
    {
        transform.localPosition = new Vector3(0f, 2f, 0f);
    }
}