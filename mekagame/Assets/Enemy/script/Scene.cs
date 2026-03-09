using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
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
