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

    private bool isTransitioning = false;

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
            miss?.SetActive(GameManager.Instance.IsPlayerDead);
            complete?.SetActive(!GameManager.Instance.IsPlayerDead);
        }
    }
    async public void OnStartButton()
    {
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
