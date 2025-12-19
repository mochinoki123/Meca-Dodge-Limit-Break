using System.Collections.Generic;
using UnityEngine;

public class ObjectParry : MonoBehaviour
{
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
                    missileScript.Kill();
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
                    lazerScript.Kill();
                    isHitEnemy = true; 
                }
            }
        }

        if (isHitEnemy && targetObj != null)
        {
            parriedMissiles.Add(targetObj);

            parrySuccess = true;
            GameManager.Instance.AddGage(50);
        }
    }

    private void OnDisable()
    {
        parriedMissiles.Clear();
    }
}
