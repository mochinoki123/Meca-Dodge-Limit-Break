using System.Collections.Generic;
using UnityEngine;

public class PlayerGraze : MonoBehaviour
{
    private HashSet<GameObject> grazedMissiles = new HashSet<GameObject>();
    [SerializeField] private float grazeRange;
    private float range;
    OverClock oc;
    SphereCollider myCollider;
    private void Awake()
    {
        myCollider = GetComponent<SphereCollider>();
        Range();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Missile") && PlayerMove.isRun)
        {
            if (!grazedMissiles.Contains(other.gameObject))
            {
                AddGraze(other.gameObject);
            }
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
    private void AddGraze(GameObject missile)
    {
        grazedMissiles.Add(missile);
        PlayerResource.Instance.AddGage(10);
    }
}
