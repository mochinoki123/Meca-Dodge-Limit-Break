using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene : MonoBehaviour
{
    [SerializeField] private GameObject complete;
    [SerializeField] private GameObject miss;

    private void Start()
    {
        bool isDead = GameManager.Instance.Died();

        miss.SetActive(isDead);
        complete.SetActive(!isDead);
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
}
