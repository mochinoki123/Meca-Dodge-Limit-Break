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
    [SerializeField] private GameObject skillCustomCanvas;

    private void Start()
    {
        miss?.SetActive(GameManager.Instance.IsPlayerDead);
        complete?.SetActive(!GameManager.Instance.IsPlayerDead);
    }
    async public void OnStartButton()
    {
        await Task.Delay(500);
        FadeManager.Instance.LoadScene("Loading", FadeTimeLoad);
    }
    async public void OnTitleButton()
    {
        await Task.Delay(500);
        FadeManager.Instance.LoadScene("Title", FadeTimeTitle);
    }
    async public void OnEndButton()
    {
        await Task.Delay(500);
        Application.Quit();
    }
    async public void OnTutorialButton()
    {
        await Task.Delay(500);
        FadeManager.Instance.LoadScene("Tutorial", FadeTimeTutorial);
    }
    async public void OnSkillCustomButton()
    {
        await Task.Delay(500);
        SceneManager.LoadScene("SkillCustom");
    }
}
