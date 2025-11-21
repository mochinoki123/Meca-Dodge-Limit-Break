using UnityEngine;

public class PlayerInvincible : MonoBehaviour
{
    [SerializeField] private float invincibleTime;
    private float invincibleTimer = 0f;
    public static bool isInvincible = false;

    private void Update()
    {
        if(isInvincible)
        {
            invincibleTimer += Time.deltaTime;

            if(invincibleTimer >= invincibleTime )
            {
                isInvincible = false;
                invincibleTimer = 0f;
            }
        }
    }
    public void OnInvincible()
    {
        isInvincible = true; 

    }
}
