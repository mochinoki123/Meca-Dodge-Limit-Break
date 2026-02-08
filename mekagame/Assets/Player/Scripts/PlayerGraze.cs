using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGraze : MonoBehaviour
{
    private Dictionary<int, float> grazeCooldowns = new Dictionary<int, float>();
    private List<int> removeCache = new List<int>();
    private readonly HashSet<string> targetTags = new HashSet<string> { "Missile", "Lazer", "FirePoint" };

    [Header("Settings")]
    [SerializeField] private float grazeRange;
    [SerializeField] private int ocAddGage;
    [SerializeField] private int addGage;
    [SerializeField] private float reGrazeTime = 1.0f;

    [Header("Audio & Visuals")]
    [SerializeField] private AudioClip grazeSound;
    [SerializeField] private GameObject grazeEffectPrefab;

    private PlayerMove playerMove;
    private OverClock oc;
    private TextScript textScript;
    private SphereCollider myCollider;
    private AudioSource audioSource;
    private Coroutine uiCoroutine;

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
        if (playerMove != null && !playerMove.isRun) return;

        if (targetTags.Contains(other.tag))
        {
            TryGraze(other.gameObject);
        }
    }

    private void TryGraze(GameObject target)
    {
        if (target == null) return;

        int targetID = target.GetInstanceID();
        float currentTime = Time.time;

        if (!grazeCooldowns.TryGetValue(targetID, out float nextTime) || currentTime >= nextTime)
        {
            grazeCooldowns[targetID] = currentTime + reGrazeTime;
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
            if (uiCoroutine != null) StopCoroutine(uiCoroutine);
            uiCoroutine = StartCoroutine(ShowGrazeText());
        }
    }

    private void CleanUpCooldowns()
    {
        removeCache.Clear();
        float currentTime = Time.time;

        foreach (var kvp in grazeCooldowns)
        {
            if (currentTime > kvp.Value + 1.0f)
            {
                removeCache.Add(kvp.Key);
            }
        }

        for (int i = 0; i < removeCache.Count; i++)
        {
            grazeCooldowns.Remove(removeCache[i]);
        }
    }

    public void SetOCRange(float range)
    {
        if (myCollider != null) myCollider.radius = range;
    }

    public void ResetRange()
    {
        if (myCollider != null) myCollider.radius = grazeRange;
    }

    private IEnumerator ShowGrazeText()
    {
        textScript.Set(TextScript.EffectType.Graze);
        yield return new WaitForSeconds(1.0f);
        textScript.Removed(TextScript.EffectType.Graze);
        uiCoroutine = null;
    }
}