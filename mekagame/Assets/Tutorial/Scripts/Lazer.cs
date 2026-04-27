using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class Lazer : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxScale;
    [SerializeField] private AudioClip charge;
    [SerializeField] private AudioClip grow;
    [SerializeField] private GameObject effect;

    AudioSource audioSource;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        audioSource.PlayOneShot(charge);
        StartCoroutine(Grow());
    }

    private IEnumerator Grow()
    {
        audioSource.PlayOneShot(grow);
        while (transform.localScale.x < maxScale)
        {
            float newX = transform.localScale.x + (speed * Time.deltaTime);

            transform.localScale = new Vector3(newX, transform.localScale.y, transform.localScale.z);

            yield return null;
        }

        transform.localScale = new Vector3(maxScale, transform.localScale.y, transform.localScale.z);

        ObjectPool_Lazer.instance.ReleaseLaser(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            ObjectPool_Lazer.instance.ReleaseLaser(gameObject);
        }
    }

    private void OnDisable()
    {
        transform.localScale = new Vector3(0.01f, 2f, 2f);
    }
}
