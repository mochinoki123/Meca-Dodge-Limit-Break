using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class FadeManager
{
    //フェード対象のアルファ操作用
    private CanvasGroup _canvasGroup;
    //フェード時間
    private float _fadeTime;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="canvasGroup"></param>
    public FadeManager(CanvasGroup canvasGroup)
    {
        _canvasGroup = canvasGroup;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="fadeEndCallback"></param>
    /// <param name="cancellationToken"></param>
    public void FadeIn(System.Action fadeEndCallback,CancellationToken cancellationToken)
    {
        LMotion.Create(_canvasGroup.alpha, 0f,_fadeTime)
        .WithEase(Ease, Linear)
        .WithOnComplate(() =>
        {
            fadeEndCallback?.Invoke();
            _canvasGroup.gameObject.SetActive(false);
        })
        .Bind(x=>_canvasGroup.alpha=x)
        .ToUniTask(cancellationToken);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="fadeEndCallback"></param>
    /// <param name="cancellationToken"></param>
    public void FadeOut(System.Action fadeEndCallback, CancellationToken cancellationToken)
    {
        _canvasGroup.gameObject.SetActive(true);
        LMotion.Create(_canvasGroup.alpha, 1f, _fadeTime).WithEase(Ease, Linear).WithOnComplate(() =>
        {
            fadeEndCallback?.Invoke();
        })
        .Bind(x => _canvasGroup.alpha = x).ToUniTask(cancellationToken);
    }
}
