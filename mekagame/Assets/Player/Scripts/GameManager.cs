using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Splines.ExtrusionShapes;
using System;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private ClearFlag clearFlag;

    [Header("Gage Settings")]
    [SerializeField] private int maxGage = 100;
    [SerializeField] private float nowGage;
    [SerializeField] private LifeGage lifeGage;
    [SerializeField] private GrazeGage grazeGage;

    [Header("ゲージ増加量")]
    [SerializeField] private int grazeGauge;
    [SerializeField] private int parryGauge;
    [SerializeField] private int LBfailedGauge;

    [Header("チュートリアルゲージ増加量")]
    [SerializeField] private int tGrazeGauge;
    [SerializeField] private int tParryGauge;

    [Header("ゲージ使用量")]
    [SerializeField] private int useLB;
    [SerializeField] private int useOC;
    [SerializeField] private int usePD;

    [Header("Combo Settings")]
    [SerializeField] private TextMeshProUGUI comboText;
    [SerializeField] private float comboTime = 2.0f;
    [SerializeField] private float[] comboMultiple = { 1.0f, 1.2f, 1.5f, 2.0f };

    public enum AddGaugeState
    {
        Graze,
        Parry,
        LBfailed
    }

    public enum UseGaugeState
    {
        LimitBreak,
        OverClock,
        PulseDiffuser
    }

    public bool IsPlayerDead { get; private set; } = false;
    public float NowGage => nowGage;

    private int combo;
    private int maxCombo;
    private float lastComboTime = 0;

    private bool isTutorial;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

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

        FindUIElements();
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
        ResetGage();
        FindUIElements();
        clearFlag.ResetGameFlag();

        isTutorial = false;

        if (scene.name == "Player") IsPlayerDead = false;
        if (scene.name == "Tutorial")
        {
            isTutorial = true;
            IsPlayerDead = false;
        }
    }

    private void FindUIElements()
    {
        lifeGage = FindAnyObjectByType<LifeGage>();
        grazeGage = FindAnyObjectByType<GrazeGage>();
        comboText = GameObject.Find("Combo")?.GetComponent<TextMeshProUGUI>();
    }

    public void AddGaugeStateBranch(AddGaugeState state)
    {
        if (isTutorial)
        {
            if (state == AddGaugeState.Graze) AddGage(tGrazeGauge);
            if (state == AddGaugeState.Parry) AddGage(tParryGauge);
        }
        if (state == AddGaugeState.Graze) AddGage(grazeGauge);
        if (state == AddGaugeState.Parry) AddGage(parryGauge);
        if (state == AddGaugeState.LBfailed) AddGage(LBfailedGauge);
    }
    private void AddGage(float amount)
    {
        float multiple = GetComboMultiple();
        nowGage += amount * multiple;
        nowGage = Mathf.Clamp(nowGage, 0, maxGage);
        UpdateCombo();

        grazeGage?.SetValue(nowGage);
        UpdateText();
    }
    public void UseGaugeStateBranch(UseGaugeState state)
    {
        if (state == UseGaugeState.LimitBreak) UseGage(useLB);
        if (state == UseGaugeState.OverClock) UseGage(useOC);
        if (state == UseGaugeState.PulseDiffuser) UseGage(usePD);
    }
    private void UseGage(float amount)
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
            comboText.text = combo > 0 ? $"{combo}" : "";
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
        FadeManager.Instance.LoadScene("Result",3f);
    }

    public void Damage()
    {
        lifeGage.Damage();
    }

    public int GetterUseGauge(UseGaugeState state)
    {
        if (state == UseGaugeState.LimitBreak) return useLB;
        if (state == UseGaugeState.OverClock) return useOC;
        if (state == UseGaugeState.PulseDiffuser) return usePD;
        return 0;
    }
}