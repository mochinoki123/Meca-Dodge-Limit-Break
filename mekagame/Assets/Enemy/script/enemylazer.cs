using UnityEngine;

public class enemylazer : MonoBehaviour
{
    enemyattack enemyAttack;
    private void Start()
    {
        enemyAttack = FindAnyObjectByType<enemyattack>();
    }
    public void Kill()
    {
        enemyAttack.Return(gameObject);
    }
}
