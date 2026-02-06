using UnityEngine;
using UnityEngine.UI;

public class GrazeGage : MonoBehaviour
{
    [SerializeField] private Slider[] gageSlider;
    [SerializeField] private float maxGage = 300f;
    [Range(0f, 300f)]
    [SerializeField] float nowGage;

    private void Start()
    {
        // スライダーの正規化範囲を設定
        foreach (var slider in gageSlider)
        {
            slider.minValue = 0f;
            slider.maxValue = 1f;
        }
    }

    private void Update()
    {
        // 常時描画更新
        UpdateGage();
    }

    public void UpdateGage()
    {
        // スライダー1つあたりの担当量を計算
        float valuePerSlider = maxGage / gageSlider.Length;

        for (int i = 0; i < gageSlider.Length; i++)
        {
            float rangeStart = i * valuePerSlider;

            // 担当範囲に対する進捗率(0~1)を計算
            float progress = Mathf.InverseLerp(rangeStart, rangeStart + valuePerSlider, nowGage);

            gageSlider[i].value = progress;

            // 値がほぼ0なら画像を非表示にする（見た目の調整）
            Image fillImage = gageSlider[i].fillRect.GetComponent<Image>();
            fillImage.enabled = (progress > 0.001f);
        }
    }

    public void SetValue(float value)
    {
        // 値を範囲内に制限して更新
        nowGage = Mathf.Clamp(value, 0f, maxGage);
        UpdateGage();
    }
}