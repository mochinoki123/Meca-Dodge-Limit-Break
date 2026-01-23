using System.Collections.Generic;
using UnityEngine;

public class PlayerGraze : MonoBehaviour
{
    private HashSet<GameObject> grazedMissiles = new HashSet<GameObject>();
    [SerializeField] private float grazeRange;
    [SerializeField] private int ocAddGage;
    [SerializeField] private int addGage;
    [SerializeField] private AudioClip graze;

    private float range;
    private PlayerMove playerMove;
    OverClock oc;
    SphereCollider myCollider;
    AudioSource audioSource;

    private void Awake()
    {
        myCollider = GetComponent<SphereCollider>();
        oc = GetComponentInParent<OverClock>();
        audioSource = GetComponentInParent<AudioSource>();
        playerMove = GetComponentInParent<PlayerMove>();
        Range();
    }
    private void OnTriggerStay(Collider other)
    {
        if ((other.CompareTag("Missile") || other.CompareTag("Lazer")) && playerMove.isRun)
        {
            if (!grazedMissiles.Contains(other.gameObject))
            {
                audioSource.PlayOneShot(graze);
                AddGraze(other.gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (grazedMissiles.Contains(other.gameObject))
        {
            grazedMissiles.Remove(other.gameObject);
        }
    }
    public void OCRange(float range)
    {
        myCollider.radius = range;
    }
    public void Range()
    {
        myCollider.radius = grazeRange;
    }
    private void AddGraze(GameObject obj)
    {
        grazedMissiles.Add(obj);
        if (oc.isOC) GameManager.Instance.AddGage(ocAddGage);
        else GameManager.Instance.AddGage(addGage);
    }
}
