using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeGage : MonoBehaviour
{
    [SerializeField] private GameObject[] life;
    private int damageCount = 0;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        damageCount = 0;
        UIReset();
    }
    public void Damage()
    {
        if (damageCount >= life.Length)
        {
            return;
        }

        life[life.Length - 1 - damageCount].gameObject.SetActive(false);

        damageCount++;

        if (damageCount == life.Length)
        {
            GameManager.Instance.Die();
        }
    }
    private void UIReset()
    {
        for(int i = 0; i < life.Length; i++)
        {
            life[i].gameObject.SetActive(true);
        }
    }
}
