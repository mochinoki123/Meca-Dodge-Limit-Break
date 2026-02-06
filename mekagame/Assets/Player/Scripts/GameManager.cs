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
    private int maxCombo;
    private float lastComboTime = 0;

    private void Awake()
    {
        // シングルトン設定
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    private void Start()
    {
        // 初期化
        ResetGage();
        UpdateText();
    }

    private void Update()
    {
        // コンボ継続判定
        CheckCombo();
    }

    public void UpdateText()
    {
        // UI表示更新
        comboText.text = "Combo : " + combo;
    }

    public void Damage()
    {
        // ダメージ処理
        lifeGage.Damage();
    }

    public void AddGage(int n)
    {
        // コンボ更新とゲージ加算（倍率適用）
        UpdateCombo();
        float multiple = GetComboMultiple();
        nowGage += n * multiple;
        grazeGage.SetValue(nowGage);

        UpdateText();
    }

    public void ResetGage()
    {
        // ゲージリセット
        nowGage = 0;
        UpdateText();
    }

    public void UseGage(int n)
    {
        // ゲージ消費
        nowGage -= n;
        grazeGage.SetValue(nowGage);
    }

    public float GetterGage()
    {
        // 現在値の取得
        return nowGage;
    }

    public void Die()
    {
        // プレイヤー破壊とシーン遷移
        Destroy(player);
        SceneManager.LoadScene("Result");
    }

    private void CheckCombo()
    {
        if (combo == 0) return;

        // 時間経過でコンボリセット
        if (Time.time - lastComboTime > comboTime)
        {
            combo = 0;
            UpdateText();
        }
    }

    private void UpdateCombo()
    {
        // コンボ加算と時間記録
        combo++;
        lastComboTime = Time.time;

        if (combo > maxCombo)
        {
            maxCombo = combo;
        }
    }

    private float GetComboMultiple()
    {
        // コンボ数に応じた倍率を返す
        if (combo >= 40) return comboMultiple[3];
        if (combo >= 30) return comboMultiple[2];
        if (combo >= 10) return comboMultiple[1];

        return comboMultiple[0];
    }
}