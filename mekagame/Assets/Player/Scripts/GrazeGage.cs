using UnityEngine;
using UnityEngine.UI;

public class GrazeGage : MonoBehaviour
{
    [SerializeField] private Slider [] gageSlider;
    [SerializeField] private float maxGage = 300f;
    [Range(0f, 300f)]
    [SerializeField] float nowGage;

    private void Start()
    {
        foreach (var slider in gageSlider)
        {
            slider.minValue = 0f;
            slider.maxValue = 1f;
        }
    }

    private void Update()
    {
        UpdateGage();
    }

    public void UpdateGage()
    {
        float valuePerSlider = maxGage / gageSlider.Length;

        for(int i = 0; i < gageSlider.Length; i++)
        {
            float rangeStart = i * valuePerSlider;

            float progress = Mathf.InverseLerp(rangeStart, rangeStart + valuePerSlider, nowGage);
            
            gageSlider[i].value = progress;

            Image fillImage = gageSlider[i].fillRect.GetComponent<Image>();

            fillImage.enabled = (progress > 0.001f);
        }
    }

    public void SetValue(float value)
    {
        nowGage = Mathf.Clamp(value, 0f, maxGage);
        UpdateGage();
    }
}
