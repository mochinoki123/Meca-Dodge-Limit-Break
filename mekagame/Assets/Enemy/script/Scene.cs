using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    [SerializeField] private float FadeTimeLoad;
    [SerializeField] private float FadeTimeTitle;
    [SerializeField] private float FadeTimeTutorial;
    [SerializeField] private GameObject complete;
    [SerializeField] private GameObject miss;
    [SerializeField] private ClearFlag clearFlag;
    [SerializeField] private AudioClip titlebuttonclip;
    [SerializeField] private GameObject OptionCanvas;

    private AudioSource audioSource;
    private bool isTransitioning = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private bool CanTransition()
    {
        if (isTransitioning) return false;
        isTransitioning = true;
        return true;
    }

    //リザルト画面
    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Result")
        {
            if (clearFlag.IsGameCleared.Value) complete?.SetActive(true);
            else miss?.SetActive(true);
        }
    }

    //スタートボタン
    async public void OnStartButton()
    {
        audioSource.PlayOneShot(titlebuttonclip);
        if (!CanTransition()) return;
        await Task.Delay(500);
        FadeManager.Instance.LoadScene("Loading", 1f);
    }

    //タイトルボタン
    async public void OnTitleButton()
    {
        if (!CanTransition()) return;
        await Task.Delay(500);
        FadeManager.Instance.LoadScene("Title", 1f);
    }

    //終了ボタン
    async public void OnEndButton()
    {
        if (!CanTransition()) return;
        await Task.Delay(500);
        Application.Quit();
    }

    //チュートリアルボタン
    async public void OnTutorialButton()
    {
        if (!CanTransition()) return;
        await Task.Delay(500);
        FadeManager.Instance.LoadScene("Tutorial", FadeTimeTutorial);
    }

    async public void OnSkillCustomButton()
    {
        await Task.Delay(500);
        SceneManager.LoadScene("SkillCustom");
    }

    async public void OnOptionButton()
    {
        await Task.Delay(500);
        OptionCanvas.SetActive(true);
    }

    async public void OnOptionOffButton()
    {
        await Task.Delay(500);
        OptionCanvas.SetActive(false);
    }

    //コンテニューボタン
    async public void OnContinueButton()
    {
        if (!CanTransition()) return;
        await Task.Delay(500);
        FadeManager.Instance.LoadScene("Loading", 1f);
    }
}