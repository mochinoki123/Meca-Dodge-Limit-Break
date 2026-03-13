using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Gage Settings")]
    [SerializeField] private int maxGage = 100;
    [SerializeField] private float nowGage;
    [SerializeField] private LifeGage lifeGage;
    [SerializeField] private GrazeGage grazeGage;

    [Header("Combo Settings")]
    [SerializeField] private TextMeshProUGUI comboText;
    [SerializeField] private float comboTime = 2.0f;
    [SerializeField] private float[] comboMultiple = { 1.0f, 1.2f, 1.5f, 2.0f };

    public bool IsPlayerDead { get; private set; } = false;
    public float NowGage => nowGage;

    private int combo;
    private int maxCombo;
    private float lastComboTime = 0;
    SkillIcon skillIcon;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        FindUIElements();
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void Start()
    {
        ResetGage();
    }

    private void Update()
    {
        CheckCombo();
    }
   
    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        FindUIElements();
    }

    private void FindUIElements()
    {
        lifeGage = GameObject.Find("HP")?.GetComponent<LifeGage>();
        grazeGage = GameObject.Find("GrazeGage")?.GetComponent<GrazeGage>();
        comboText = GameObject.Find("Combo")?.GetComponent<TextMeshProUGUI>();
        skillIcon = GameObject.Find("SkillIcon")?.GetComponent<SkillIcon>();
    }


    public void AddGage(float amount)
    {
        UpdateCombo();

        nowGage += amount * GetComboMultiple();
        nowGage = Mathf.Clamp(nowGage, 0, maxGage);

        grazeGage?.SetValue(nowGage);
        UpdateText();
    }

    public void UseGage(float amount)
    {
        nowGage = Mathf.Max(nowGage - amount, 0f);
        grazeGage?.SetValue(nowGage);
    }

    private void UpdateCombo()
    {
        combo++;
        lastComboTime = Time.time;
        if (combo > maxCombo) maxCombo = combo;
    }

    private void CheckCombo()
    {
        if (combo > 0 && Time.time - lastComboTime > comboTime)
        {
            combo = 0;
            UpdateText();
        }
    }

    private float GetComboMultiple()
    {
        if (comboMultiple == null || comboMultiple.Length == 0) return 1f;

        if (combo >= 40) return GetSafeMultiple(3);
        if (combo >= 30) return GetSafeMultiple(2);
        if (combo >= 10) return GetSafeMultiple(1);

        return GetSafeMultiple(0);
    }

    private float GetSafeMultiple(int index)
    {
        if (index >= comboMultiple.Length) return comboMultiple[comboMultiple.Length - 1];
        return comboMultiple[index];
    }

    public void UpdateText()
    {
        if (comboText != null)
        {
            comboText.text = combo > 0 ? $"Combo : {combo}" : "";
        }
    }

    public void ResetGage()
    {
        nowGage = 0;
        combo = 0; 
        UpdateText();
    }

    public void Die()
    {
        if (IsPlayerDead) return;
        IsPlayerDead = true;
        skillIcon.ResetSkill();

        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) Destroy(p);

        SceneManager.LoadScene("Result");
    }

    public void Damage()
    {
        lifeGage?.Damage();
    }
}