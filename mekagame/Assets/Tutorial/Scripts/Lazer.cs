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

    public bool isWall;


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
        yield return new WaitForSeconds(0.5f);
        audioSource.PlayOneShot(grow);
        while (!isWall)
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
            isWall = true;
        }
    }

    private void OnDisable()
    {
        transform.localScale = new Vector3(0.01f, 2f, 2f);
        isWall = false;
    }
}
