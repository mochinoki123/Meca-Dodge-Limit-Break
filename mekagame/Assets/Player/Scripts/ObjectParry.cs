using System.Collections.Generic;
using UnityEngine;

public class ObjectParry : MonoBehaviour
{
    public static bool parrySuccess;
    private HashSet<GameObject> parriedMissiles = new HashSet<GameObject>();

    private void Start()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Missile"))
        {
            if (!parriedMissiles.Contains(other.gameObject))
            {
                parriedMissiles.Add(other.gameObject);
                other.gameObject.GetComponent<enemymissile>().Kill();
                parrySuccess = true;
                GameManager.Instance.AddGage(50);
            }
        }
    }
}
