using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    private PlayerInvincible invincible;
    public bool isHit;

    private void Start()
    {
        invincible = GetComponent<PlayerInvincible>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("“–‚½‚Á‚½");
        if (PlayerInvincible.isInvincible) return;

        isHit = false; 

        if (other.CompareTag("Missile"))
        {
            var missile = other.GetComponentInParent<enemymissile>();
            if (missile != null)
            {
                missile.Kill();
                isHit = true;
            }
        }
        if (other.CompareTag("Lazer"))
        {
            var lazer = other.GetComponentInParent<enemylazer>();
            if (lazer != null)
            {
                lazer.Kill();
            }
            isHit = true;
        }

        if (isHit)
        {
            GameManager.Instance.Damage();
            invincible.OnInvincible();
        }
    }
}
