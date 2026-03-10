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

    private GameObject player; // 実行時に探すか、プロパティで管理
    private int combo;
    private int maxCombo;
    private float lastComboTime = 0;
    private bool isPlayerDead = false;

    private void Awake()
    {
        // シングルトンの重複チェック
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
    }

    // --- コンボ・ゲージ関連 ---

    public void AddGage(int n)
    {
        UpdateCombo();
        nowGage += n * GetComboMultiple();
        nowGage = Mathf.Min(nowGage, maxGage);

        grazeGage.SetValue(nowGage);
        UpdateText();
    }

    public void UseGage(int n)
    {
        nowGage = Mathf.Max(nowGage - n, 0f);
        grazeGage.SetValue(nowGage);
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

        // 配列の範囲外参照を防ぎつつ倍率を適用
        if (combo >= 40 && comboMultiple.Length > 3) return comboMultiple[3];
        if (combo >= 30 && comboMultiple.Length > 2) return comboMultiple[2];
        if (combo >= 10 && comboMultiple.Length > 1) return comboMultiple[1];

        return comboMultiple[0];
    }

    public void UpdateText()
    {
        if (comboText != null)
            comboText.text = $"Combo : {combo}";
    }

    public void ResetGage()
    {
        nowGage = 0;
        UpdateText();
    }

    public float GetterGage()

    {

        // 現在値の取得

        return nowGage;

    }

    // --- 状態管理 ---

    public bool Died() => isPlayerDead; // 前回のSceneクラスとの整合性用

    public void Die()
    {
        isPlayerDead = true;

        // プレイヤーオブジェクトの削除（タグで探すのが一般的）
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) Destroy(p);

        SceneManager.LoadScene("Result");
    }

    public void Damage()
    {
        if (lifeGage != null) lifeGage.Damage();
    }
}