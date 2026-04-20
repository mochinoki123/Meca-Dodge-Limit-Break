using UnityEngine;
using UnityEngine.SceneManagement;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
}
