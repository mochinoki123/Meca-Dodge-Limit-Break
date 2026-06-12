using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Loading : MonoBehaviour
{
    [SerializeField] private Slider slider;

    IEnumerator Start()
    {

        slider.value = 0f;

        while (slider.value < 1f)
        {
            // 徐々にスライダーを増やす（演出）
            slider.value += Time.deltaTime * 0.5f; // 速度調整OK
            yield return null;
        }
        FadeManager.Instance.LoadScene("MissileDebugScene", 1f);
    }
}
