using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene : MonoBehaviour
{
    [SerializeField] private float FadeTimeLoad;
    [SerializeField] private float FadeTimeTitle;
    [SerializeField] private float FadeTimeTutorial;
    [SerializeField] private GameObject complete;
    [SerializeField] private GameObject miss;

    [SerializeField] private ClearFlag clearFlag;

    [SerializeField] private AudioClip titlebuttonclip;
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


    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Result")
        {
            if (clearFlag.IsCleared) complete?.SetActive(true);
            else miss?.SetActive(true);      
        }
    }
    async public void OnStartButton()
    {
        audioSource.PlayOneShot(titlebuttonclip);
        if (!CanTransition()) return;
        await Task.Delay(500);
        FadeManager.Instance.LoadScene("Loading", FadeTimeLoad);
    }
    async public void OnTitleButton()
    {
        if (!CanTransition()) return;
        await Task.Delay(500);
        FadeManager.Instance.LoadScene("Title", FadeTimeTitle);
    }
    async public void OnEndButton()
    {
        if (!CanTransition()) return;
        await Task.Delay(500);
        Application.Quit();
    }
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
}
