using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Loading : MonoBehaviour
{
    [SerializeField] private Slider slider;

    IEnumerator Start()
    {
        float waitTime = Random.Range(3.0f, 7.0f);
        slider.value = 0f;

        while (slider.value < 100f)
        {
            // 徐々にスライダーを増やす（演出）
            slider.value += Time.deltaTime * waitTime; // 速度調整OK
            yield return null;
        }
        FadeManager.Instance.LoadScene("", 1f);
    }
}
