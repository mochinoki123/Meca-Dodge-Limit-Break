using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimelineManager : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;
    [SerializeField] private Animator animator;
    [SerializeField] private ClearFlag clearFlag;

    [Header("HPフェーズ別タイムライン")]
    [SerializeField] private PlayableAsset phase1Timeline; // HP 60%以上
    [SerializeField] private PlayableAsset phase2Timeline; // HP 30〜60%
    [SerializeField] private PlayableAsset phase3Timeline; // HP 30%以下

    [Header("カウントダウン")]
    [SerializeField] private PlayableAsset phaseTransition_2;
    [SerializeField] private PlayableAsset phaseTransition_3;

    [Header("フェーズ移行時のタイムライン")]
    [SerializeField] private PlayableAsset countdown;

    [Header("フェーズ閾値")]
    [SerializeField] private float phase2Threshold = 0.6f;
    [SerializeField] private float phase3Threshold = 0.3f;

    [SerializeField] private PlayableAsset FinishTimeline;

    private PlayableAsset currentTimeline;

    private int currentPhase = 0;

    private void OnEnable()
    {
        director.stopped += TimelineStopped;
    }

    private void OnDisable()
    {
        director.stopped -= TimelineStopped;
    }

    private void Start()
    {
        // 初期タイムライン再生
        SwitchTimeline(countdown);
    }

    // Enemyのダメージ処理から呼ばれる
    public void OnHpChanged(int currentHP, int maxHP)
    {
        if (currentHP <= 0)
        {
            director.Stop();
            SwitchTimeline(FinishTimeline);
            return;
        }

        float ratio = (float)currentHP / maxHP;
        PlayableAsset targetTimeline = GetTimelineByRatio(ratio);

        // フェーズが変わった時だけ切り替え
        if (targetTimeline != currentTimeline)
        {
            SwitchTimeline(targetTimeline);
        }
    }

    private PlayableAsset GetTimelineByRatio(float ratio)
    {
        if (ratio > phase2Threshold && currentPhase < 1)
        {
            currentPhase = 1;
            return phase1Timeline;
        }
        else if (ratio <= phase2Threshold && ratio > phase3Threshold && currentPhase < 2)
        {
            animator.SetTrigger("IsPhaseChange");
            currentPhase = 2;
            SetWrapModeNone();
            return phaseTransition_2;
        }
        else if (ratio <= phase3Threshold && currentPhase < 3)
        {
            animator.SetTrigger("IsPhaseChange");
            currentPhase = 3;
            SetWrapModeNone();
            return phaseTransition_3;
        }
            return currentTimeline;
    }

    private void SwitchTimeline(PlayableAsset asset)
    {
        if (asset == null) return;

        director.Stop();
        director.playableAsset = asset;
        director.Play();

        currentTimeline = asset;
    }

    private void TimelineStopped(PlayableDirector director)
    {
        if(currentTimeline == countdown)
        {
            SetWrapModeLoop();
            SwitchTimeline(phase1Timeline);
        }
        if(currentTimeline == phaseTransition_2)
        {
            SetWrapModeLoop();
            SwitchTimeline(phase2Timeline);
        }
        if (currentTimeline == phaseTransition_3)
        {
            SetWrapModeLoop();
            SwitchTimeline(phase3Timeline);
        }
    }

    private void SetWrapModeLoop()
    {
        director.extrapolationMode = DirectorWrapMode.Loop;
    }

    private void SetWrapModeNone()
    {
        director.extrapolationMode = DirectorWrapMode.None;
    }
}
