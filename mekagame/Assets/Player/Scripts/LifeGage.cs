using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeGage : MonoBehaviour
{
    [SerializeField] private Slider[] lifeSlider;
    [SerializeField] private int maxHP = 5;
    public float nowLife;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void Start()
    {
        foreach(var slider in lifeSlider)
        {
            slider.minValue = 0f;
            slider.maxValue = 1f;
        }
    }
    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        nowLife = maxHP;
        UIReset();
    }
    public void Damage()
    {
        if (nowLife == 1)
        {
            GameManager.Instance.Die();
            return;
        }

        nowLife--;

        UIReset();
    }
    private void UIReset()
    {
        // スライダー1つあたりの担当量を計算
        float valuePerSlider = maxHP / lifeSlider.Length;

        for (int i = 0; i < lifeSlider.Length; i++)
        {
            float rangeStart = i * valuePerSlider;

            // 担当範囲に対する進捗率(0~1)を計算
            float progress = Mathf.InverseLerp(rangeStart, rangeStart + valuePerSlider, nowLife);

            lifeSlider[i].value = progress;

            // 値がほぼ0なら画像を非表示にする（見た目の調整）
            Image fillImage = lifeSlider[i].fillRect.GetComponent<Image>();
            fillImage.enabled = (progress > 0.001f);
        }
    }
}
