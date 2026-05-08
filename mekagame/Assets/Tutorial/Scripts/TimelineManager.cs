using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimelineManager : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;
    [SerializeField] private TimelineAsset LV1;
    [SerializeField] private TimelineAsset LV2;

    [SerializeField] private Enemy enemy;

    private void Update()
    {
        //if (enemy.CurrentHP <= 250)
        //{

        //}
        //if (enemy.CurrentHP <= 500)
        //{

        //}
        if (enemy.CurrentHP <= 750)
        {
            director.Stop();
            director.playableAsset = LV2;
            director.Play();
        }
        if (enemy.CurrentHP <= 1000 && enemy.CurrentHP > 750)
        {
            director.Stop();
            director.playableAsset = LV1;
            director.Play();
        }
    }
}
