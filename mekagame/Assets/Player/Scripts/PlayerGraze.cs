using System.Collections.Generic;
using UnityEngine;

public class PlayerGraze : MonoBehaviour
{
    private HashSet<GameObject> grazedMissiles = new HashSet<GameObject>();

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

    private void AddGraze(GameObject missile)
    {
        grazedMissiles.Add(missile); 
        PlayerResource.Instance.AddGage(10);
    }
}
