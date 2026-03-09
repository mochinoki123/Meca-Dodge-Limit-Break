using UnityEngine;
using UnityEngine.UI;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] private Image fillImage = null;
    private Enemy enemy = null;

    void Start()
    {
        enemy = GetComponentInParent<Enemy>();

        if (fillImage == null)
        {
            fillImage = transform.Find("Canvas/Fill")?.GetComponent<Image>();
        }
    }

    void Update()
    {
        if (enemy == null || fillImage == null) return;

        float hpPercentage = (float)enemy.CurrentHP / (float)enemy.maxHP;

        fillImage.fillAmount = hpPercentage;
    }
}