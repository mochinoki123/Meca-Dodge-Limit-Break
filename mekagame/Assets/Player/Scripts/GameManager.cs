using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private int maxGage;
    [SerializeField] private TextMeshProUGUI comboText;
    [SerializeField] private float nowGage;
    [SerializeField] private GameObject player;
    [SerializeField] private float comboTime;
    [SerializeField] private float[] comboMultiple;
    [SerializeField] private LifeGage lifeGage;
    [SerializeField] private GrazeGage grazeGage;

    private int combo;
    private  int maxCombo;
    private float lastComboTime = 0;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }
    private void Start()
    {
        ResetGage();
        UpdateText();
    }
    private void Update()
    {
        CheckCombo();
    }
    public void UpdateText()
    {
        comboText.text = "Combo : " + combo;
    }
    public void Damage()
    {
        lifeGage.Damage();
    }
    public void AddGage(int n)
    {
        UpdateCombo();
        float multiple = GetComboMultiple();
        nowGage += n * multiple;
        grazeGage.SetValue(nowGage);

        UpdateText();
    }
    public void ResetGage()
    {
        nowGage = 0;
        UpdateText();
    }
    public void UseGage(int n)
    {
        nowGage -= n;
        grazeGage.SetValue(nowGage);
    }
    public float GetterGage()
    {
        return nowGage;
    }
    public void Die()
    {
        Destroy(player);
        SceneManager.LoadScene("Result");
    }
    private void CheckCombo()
    {
        if (combo == 0) return;
        if (Time.time - lastComboTime > comboTime)
        {
            combo = 0;
            UpdateText();
        }
    }
    private void UpdateCombo()
    {
        combo++;
        lastComboTime = Time.time;

        if (combo > maxCombo)
        {
            maxCombo = combo;
        }
    }
    private float GetComboMultiple()
    {
        if (combo >= 40) return comboMultiple[3];
        if (combo >= 30) return comboMultiple[2];
        if (combo >= 10) return comboMultiple[1];

        return comboMultiple[0];
    }
}

