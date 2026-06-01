using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextScript : MonoBehaviour
{
    [SerializeField] private GameObject camera;
    [SerializeField] private TextMeshProUGUI graze;
    [SerializeField] private TextMeshProUGUI parry;
    [SerializeField] private TextMeshProUGUI limitBreak;

    public enum EffectType { Graze, Parry, LimitBreak, All }

    // EnumをキーにしたDictionaryでテキストを管理
    private Dictionary<EffectType, TextMeshProUGUI> textMap;

    private void Awake()
    {
        textMap = new Dictionary<EffectType, TextMeshProUGUI>
        {
            { EffectType.Graze,      graze      },
            { EffectType.Parry,      parry      },
            { EffectType.LimitBreak, limitBreak },
        };
    }

    private void Start()
    {
        Removed(EffectType.All);
    }

    private void Update()
    {
        // 全テキストをまとめてビルボード処理
        foreach (var text in textMap.Values)
            BillboardToCamera(text.transform);
    }

    public void Set(EffectType type) => SetAlpha(type, 0.8f);
    public void Removed(EffectType type) => SetAlpha(type, 0f);

    // AlphaをセットするロジックをSetとRemovedで共通化
    private void SetAlpha(EffectType type, float alpha)
    {
        if (type == EffectType.All)
        {
            foreach (var text in textMap.Values)
                text.alpha = alpha;
        }
        else if (textMap.TryGetValue(type, out var text))
        {
            text.alpha = alpha;
        }
    }

    private void BillboardToCamera(Transform t)
    {
        t.LookAt(camera.transform);
        t.Rotate(0, 180, 0);
    }
}