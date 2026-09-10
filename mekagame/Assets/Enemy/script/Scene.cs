using Cysharp.Threading.Tasks.Triggers;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene : MonoBehaviour
{
    [SerializeField] private float FadeTimeLoad;
    [SerializeField] private float FadeTimeTitle;
    [SerializeField] private float FadeTimeTutorial;
    [SerializeField] private GameObject complete;
    [SerializeField] private GameObject miss;
    [SerializeField] private GameObject _continue;
    [SerializeField] private RectTransform title;
    [SerializeField] private ClearFlag clearFlag;
    [SerializeField] private AudioClip titlebuttonclip;
    [SerializeField] private GameObject OptionCanvas;
    [SerializeField] private GameObject HardCanvas;
    [SerializeField] private Slider seSlider;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Button start;
    [SerializeField] private Button tutorial;

    private AudioSource audioSource;
    //private RectTransform titlepos;
    private bool isTransitioning = false;

    // ★ ハードモード中かどうかを保持するフラグ（シーンをまたいでも保持できるように static にするか、マネージャー管理にします）
    public static bool IsHardMode { get; private set; } = false;



    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        //titlepos.anchoredPosition = title.anchoredPosition;
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

    private void HideAllCanvases()
    {
        if (HardCanvas != null) HardCanvas.SetActive(false);
    }


    //リザルト画面
    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Result")
        {
            if (clearFlag.IsGameCleared.Value)
            {
                complete?.SetActive(true);
                _continue?.SetActive(false);
                title.anchoredPosition = new Vector2(0f, -180f);
            }
            else 
            { 
                miss?.SetActive(true);
                /*
                title.anchoredPosition = titlepos.anchoredPosition;
                _continue?.SetActive(true);
                */
            }
        }
    }

    //スタートボタン
    async public void OnStartButton()
    {
        audioSource.PlayOneShot(titlebuttonclip);
        if (!CanTransition()) return;
        IsHardMode = false; // 通常モードとしてフラグをオフ
        await Task.Delay(500);
        FadeManager.Instance.LoadScene("Loading", 1f);
    }

    //タイトルボタン
    async public void OnTitleButton()
    {
        // まずハードモード分岐をチェック
        if (clearFlag.IsGameCleared.Value)
        {
            HardCanvas.SetActive(true);
        }
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

    //ハードモード
    async public void OnHardButton()
    {
        if (!CanTransition()) return;
        HideAllCanvases();
        IsHardMode = true; // ★ ハードモード中なのでフラグをオン
        await Task.Delay(500);
        //FadeManager.Instance.LoadScene("Loading", 1f);
        FadeManager.Instance.LoadScene("hardmode", 1f);
    }

    //ハードモードにいかないボタン
    async public void OnNoButton()
    {
        if (!CanTransition()) return;
        HideAllCanvases();
        await Task.Delay(500);
        FadeManager.Instance.LoadScene("Title", 1f);
    }
}