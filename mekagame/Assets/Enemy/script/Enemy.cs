using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Enemy : MonoBehaviour
{
    [Header("敵ステータス")]
    [SerializeField] public int maxHP = 1000;
    private AudioSource audioSource;
    [SerializeField] private AudioClip EnemyFinish;
    [SerializeField] private ClearFlag clearFlag;
    public Animator animator;
    public float finishfade;
    public int CurrentHP { get; private set; }

    // タイムライン制御の参照を追加
    [SerializeField] private TimelineManager timelineManager;


    private bool isTutorial = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponentInChildren<Animator>();
        CurrentHP = maxHP;
    }

    private void OnEnable()
    {
        clearFlag.ResetGameFlag();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        isTutorial = (scene.name == "Tutorial");
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
        if (isTutorial) return;
        if (CurrentHP <= 0)
        {
            if (clearFlag != null) clearFlag.IsGameCleared = true;
            animator.SetTrigger("IsFinish");
            AudioSource.PlayClipAtPoint(EnemyFinish, transform.position);
            await Task.Delay(4000);
            FadeManager.Instance.LoadScene("Result", finishfade);
        }
    }
}