using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    [Header("敵ステータス")]
    [SerializeField] public int maxHP = 1000;

    public int CurrentHP { get; private set; }

    void Awake()
    {
        CurrentHP = maxHP;
    }

    public void Damage(int damage)
    {
        CurrentHP -= damage;
        CurrentHP = Mathf.Max(CurrentHP, 0);

        CheckIfDead();
    }

    private void CheckIfDead()
    {
        if (CurrentHP <= 0)
        {
            SceneManager.LoadScene("Result");
        }
    }
}