using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    [Header("敵ステータス")]
    [SerializeField] public int maxHP = 1000;
    private AudioSource audioSource;
    [SerializeField] private AudioClip EnemyFinish;
    public Animator animator;
    public float finishfade;
    public int CurrentHP { get; private set; }

    // タイムライン制御の参照を追加
    [SerializeField] private TimelineManager timelineManager;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponentInChildren<Animator>();
        CurrentHP = maxHP;
    }

    public void Damage(int damage)
    {
        CurrentHP -= damage;
        CurrentHP = Mathf.Max(CurrentHP, 0);
        animator.SetTrigger("isDamage");

        // HP変化をタイムラインに通知
        timelineManager?.OnHpChanged(CurrentHP, maxHP);

        CheckIfDead();
    }

    private async void CheckIfDead()
    {
        if (CurrentHP <= 0)
        {
            animator.SetTrigger("IsFinish");
            AudioSource.PlayClipAtPoint(EnemyFinish, transform.position);
            await Task.Delay(4000);
            FadeManager.Instance.LoadScene("Result", finishfade);
        }
    }
}