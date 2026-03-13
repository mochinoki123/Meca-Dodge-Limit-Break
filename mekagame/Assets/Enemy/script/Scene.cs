using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene : MonoBehaviour
{
    [SerializeField] private GameObject complete;
    [SerializeField] private GameObject miss;
    [SerializeField] private GameObject skillCustomCanvas;

    private void Start()
    {
        miss.SetActive(GameManager.Instance.IsPlayerDead);
        complete.SetActive(!GameManager.Instance.IsPlayerDead);
    }
    public void OnStartButton()
    {
        SceneManager.LoadScene("Player");
    }
    public void OnTitleButton()
    {
        SceneManager.LoadScene("Title");
    }
    public void OnEndButton()
    {
        Application.Quit();
    }
    public void OnTutorialButton()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void OnSkillCustomButton(bool b)
    {
        skillCustomCanvas.SetActive(b);
    }
}
