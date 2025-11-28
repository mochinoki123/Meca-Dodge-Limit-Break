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
        if (other.gameObject.CompareTag("Missile") && CompareTag("Player") && !PlayerInvincible.isInvincible)
        {
            other.gameObject.GetComponent<enemymissile>().Kill();
            PlayerResource.Instance.Damage();
            PlayerResource.Instance.UpdateText();
            invincible.OnInvincible();
        }
    }
}
