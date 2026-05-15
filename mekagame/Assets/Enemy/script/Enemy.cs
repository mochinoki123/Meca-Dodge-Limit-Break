using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    [Header("敵ステータス")]
    [SerializeField] public int maxHP = 1000;
    public int CurrentHP { get; private set; }

    // タイムライン制御の参照を追加
    [SerializeField] private TimelineManager timelineManager;

    void Awake()
    {
        CurrentHP = maxHP;
    }

    public void Damage(int damage)
    {
        Debug.Log("enemydamage");
        CurrentHP -= damage;
        CurrentHP = Mathf.Max(CurrentHP, 0);

        // HP変化をタイムラインに通知
        timelineManager?.OnHpChanged(CurrentHP, maxHP);

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