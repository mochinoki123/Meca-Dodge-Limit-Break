using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGraze : MonoBehaviour
{
    private Dictionary<GameObject, float> grazeCooldowns = new Dictionary<GameObject, float>();
    private List<GameObject> removeCache = new List<GameObject>();

    [Header("Settings")]
    [SerializeField] private float grazeRange;
    [SerializeField] private int ocAddGage;
    [SerializeField] private int addGage;
    [SerializeField] private float reGrazeTime = 0.2f;

    [Header("Audio & Visuals")]
    [SerializeField] private AudioClip grazeSound;
    [SerializeField] private GameObject grazeEffectPrefab;

    private PlayerMove playerMove;
    private OverClock oc;
    private TextScript textScript;
    private SphereCollider myCollider;
    private AudioSource audioSource;

    private void Awake()
    {
        myCollider = GetComponent<SphereCollider>();
        oc = GetComponentInParent<OverClock>();
        audioSource = GetComponentInParent<AudioSource>();
        playerMove = GetComponentInParent<PlayerMove>();
        textScript = GetComponentInParent<TextScript>();
        ResetRange();
    }

    private void Update()
    {
        if (Time.frameCount % 60 == 0)
        {
            CleanUpCooldowns();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!playerMove.isRun) return;

        if (other.CompareTag("Missile") || other.CompareTag("Lazer") || other.CompareTag("FirePoint"))
        {
            TryGraze(other.gameObject);
        }
    }

    private void TryGraze(GameObject target)
    {
        if (target == null) return;

        float nextTime;
        if (!grazeCooldowns.TryGetValue(target, out nextTime) || Time.time >= nextTime)
        {
            grazeCooldowns[target] = Time.time + reGrazeTime;
            ExecuteGraze();
        }
    }

    private void ExecuteGraze()
    {
        if (audioSource != null && grazeSound != null)
        {
            audioSource.PlayOneShot(grazeSound);
        }

        if (grazeEffectPrefab != null)
        {
            GameObject effect = Instantiate(grazeEffectPrefab, transform.position, Quaternion.identity);
            effect.transform.SetParent(transform);
            Destroy(effect, 1.0f);
        }

        int gageAmount = (oc != null && oc.isOC) ? ocAddGage : addGage;
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddGage(gageAmount);
        }

        if (textScript != null)
        {
            StartCoroutine(ShowGrazeText());
        }
    }

    private void CleanUpCooldowns()
    {
        removeCache.Clear();
        foreach (var key in grazeCooldowns.Keys)
        {
            if (key == null)
            {
                removeCache.Add(key);
            }
        }

        for (int i = 0; i < removeCache.Count; i++)
        {
            grazeCooldowns.Remove(removeCache[i]);
        }
    }

    public void SetOCRange(float range)
    {
        myCollider.radius = range;
    }

    public void ResetRange()
    {
        myCollider.radius = grazeRange;
    }

    private IEnumerator ShowGrazeText()
    {
        textScript.Set(TextScript.EffectType.Graze);
        yield return new WaitForSeconds(1.0f);
        textScript.Removed(TextScript.EffectType.Graze);
    }
}