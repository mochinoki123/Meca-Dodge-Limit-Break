using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad
{
    private FadeManager _fadeManager;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fadeManager"></param>
    public SceneLoad(FadeManager fadeManager) 
    { 
        _fadeManager = fadeManager;
    }

    public void LoadNextScene(int sceneIndex,
        System.Action sceneChangeEndCallback,
        CancellationToken cancellationToken)
    {
        _fadeManager.FadeOut(() =>
        {
            LoadingScene(sceneIndex, sceneChangeEndCallback, cancellationToken).Foget();
        },cancellationToken);
    }

    private async UniTask LoadingScene(int sceneIndex,
        System.Action sceneChangeEndCallback,
        CancellationToken cancellationToken)
    {
        await SceneManager.LoadSceneAsync(sceneIndex);
        _fadeManager.FadeIn(sceneChangeEndCallback, cancellationToken);
    }
}
