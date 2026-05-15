using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimelineManager : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;

    [Header("HPフェーズ別タイムライン")]
    [SerializeField] private PlayableAsset phase1Timeline; // HP 60%以上
    [SerializeField] private PlayableAsset phase2Timeline; // HP 30〜60%
    [SerializeField] private PlayableAsset phase3Timeline; // HP 30%以下

    [Header("フェーズ閾値")]
    [SerializeField] private float phase2Threshold = 0.6f;
    [SerializeField] private float phase3Threshold = 0.3f;

    private PlayableAsset currentTimeline;

    private void Start()
    {
        // 初期タイムライン再生
        SwitchTimeline(phase1Timeline);
    }

    // Enemyのダメージ処理から呼ばれる
    public void OnHpChanged(int currentHP, int maxHP)
    {
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
        if (ratio > phase2Threshold) return phase1Timeline;
        if (ratio > phase3Threshold) return phase2Timeline;
        return phase3Timeline;
    }

    private void SwitchTimeline(PlayableAsset asset)
    {
        if (asset == null) return;

        director.Stop();
        director.playableAsset = asset;
        director.Play();

        currentTimeline = asset;
    }
}
