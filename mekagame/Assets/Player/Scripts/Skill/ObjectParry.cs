using System.Collections.Generic;
using UnityEngine;

public class ObjectParry : MonoBehaviour
{
    [SerializeField] private GameObject parryEffect;
    public static bool parrySuccess;
    private HashSet<GameObject> parriedMissiles = new HashSet<GameObject>();
    private void OnTriggerEnter(Collider other)
    {
        GameObject targetObj = null;
        bool isHitEnemy = false;

        if (other.CompareTag("Missile"))
        {
            var missileScript = other.GetComponentInParent<enemymissile>();

            if (missileScript != null)
            {
                targetObj = missileScript.gameObject;

                if (!parriedMissiles.Contains(targetObj))
                {
                    isHitEnemy = true; 
                }
            }
        }
        else if (other.CompareTag("Lazer"))
        {
            var lazerScript = other.GetComponentInParent<enemylazer>();

            if (lazerScript != null)
            {
                targetObj = lazerScript.gameObject;

                if (!parriedMissiles.Contains(targetObj))
                {
                    isHitEnemy = true; 
                }
            }
        }

        if (isHitEnemy && targetObj != null)
        {
            parriedMissiles.Add(targetObj);

            GameObject effect = Instantiate(parryEffect, new Vector3(transform.position.x, 1.0f, transform.position.z), Quaternion.identity);
            Destroy(effect, 1.0f);
            parrySuccess = true;
            GameManager.Instance.AddGage(50);
        }
    }

    private void OnDisable()
    {
        parriedMissiles.Clear();
    }
}
