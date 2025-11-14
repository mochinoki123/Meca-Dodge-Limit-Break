using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    private int combo;
    [SerializeField] private Text comboText;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }
    private void Start()
    {
        UpdateText();
        ResetCombo();
    }
    public void UpdateText()
    {
        comboText.text = "COMBO : " + combo;
    }
    public void AddCombo()
    {
        combo++;
        UpdateText();
    }
    public void ResetCombo()
    {
        combo = 0;
        UpdateText();
    }
}
