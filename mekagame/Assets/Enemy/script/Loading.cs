using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Loading : MonoBehaviour
{
    [Header("SceneNameSet")]
    [SerializeField] private string scenename;
    [SerializeField] private Slider slider;
    IEnumerator Start()
    {
        float waitTime = Random.Range(20.0f, 30.0f);
        slider.value = 0f;

        while (slider.value < 100f)
        {
            // 徐々にスライダーを増やす（演出）
            slider.value += Time.deltaTime * waitTime; // 速度調整OK
            yield return null;
        }
        FadeManager.Instance.LoadScene(scenename, 1.5f);
    }
}
