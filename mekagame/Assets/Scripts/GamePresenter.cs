using UnityEngine;

public class GamePresenter : Singleton<GamePresenter>
{
    #region InspectorÝ’è—p

    [SerializeField] private CanvasGroup _canvasGroup;

    #endregion

    #region ƒ[ƒJƒ‹•Ï”

    private FadeManager _fadeManager;
    private SceneLoad _sceneLoad;

    #endregion
    protected override void Awake()
    {
       base.Awake();
        _fadeManager = new FadeManager(_canvasGroup);
        _sceneLoad=new SceneLoad(_fadeManager);
    }
    private void Start()
    {
        _fadeManager.FadeIn(null,this.destroyCancellationToken);
    }
    public void LoadScene(int sceneIndex,System.Action sceneChangeCallback=null)=>
        _sceneLoad.LoadNextScene(sceneIndex,sceneChangeCallback,this.destroyCancellationToken);
}
