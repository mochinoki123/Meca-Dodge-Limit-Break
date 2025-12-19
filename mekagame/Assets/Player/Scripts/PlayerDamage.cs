using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    private PlayerInvincible invincible;

    private void Start()
    {
        invincible = GetComponent<PlayerInvincible>();
    }
    private void OnTriggerEnter(Collider other)
    { 
        if (PlayerInvincible.isInvincible) return;

        bool isHit = false; 

        if (other.CompareTag("Missile"))
        {
            var missile = other.GetComponentInParent<enemymissile>();
            if (missile != null)
            {
                missile.Kill();
                isHit = true;
            }
        }
        else if (other.CompareTag("Lazer"))
        {
            var lazer = other.GetComponentInParent<enemylazer>();
            if (lazer != null)
            {
                lazer.Kill();
                isHit = true;
            }
        }

        if (isHit)
        {
            GameManager.Instance.Damage();
            invincible.OnInvincible();
        }
    }
}
