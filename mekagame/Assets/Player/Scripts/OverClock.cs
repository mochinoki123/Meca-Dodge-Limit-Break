using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class OverClock : MonoBehaviour
{
    [SerializeField] private int oCUseGage;
    [SerializeField] private float oCTime;
    [SerializeField] public float oCSpeed;
    [SerializeField] public float oCCoolTime;
    [SerializeField] public float oCGrazeRange;
    [SerializeField] private AudioClip overClock;
    [SerializeField] private Material ocColor;
    [SerializeField] private Material originalColor;

    public bool isOC = false;
    PlayerGraze pg;
    AudioSource audioSource;
    Renderer rend;

    private void Awake()
    {
        pg = GetComponentInChildren<PlayerGraze>();
        audioSource = GetComponent<AudioSource>();
        rend = GetComponentInChildren<Renderer>();
    }
    private void OnOverClock(InputValue value)
    {
        if (isOC) return;
        StartCoroutine(PlayOverClock());
    }
    private IEnumerator PlayOverClock()
    {
        if(GameManager.Instance.GetterGage() >= oCUseGage)
        {
            audioSource.PlayOneShot(overClock);
            GameManager.Instance.UseGage(oCUseGage);
            isOC = true;
            rend.material.color = ocColor.color;
            pg.OCRange(oCGrazeRange);
            yield return new WaitForSeconds(oCTime);
            isOC = false;
            rend.material.color = originalColor.color;
            pg.Range();
        }
    }
}
