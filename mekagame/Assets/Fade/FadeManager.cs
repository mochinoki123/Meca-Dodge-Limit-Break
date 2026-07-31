using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// シーン遷移時のフェードイン・アウトを制御するためのクラス .
/// </summary>
public class FadeManager : MonoBehaviour
{
	#region Singleton

	private static FadeManager instance;

	public static FadeManager Instance {
		get {
			if (instance == null) {
				instance = (FadeManager)FindObjectOfType (typeof(FadeManager));

				if (instance == null) {
					Debug.LogError (typeof(FadeManager) + "is nothing");
				}
			}

			return instance;
		}
	}

    #endregion Singleton

    public float FadeTime;

	/// <summary>フェード中の透明度</summary>
	private float fadeAlpha = 0;
	/// <summary>フェード中かどうか</summary>
	private bool isFading = false;
	/// <summary>デフォルトフェード色</summary>
	public Color fadeColor = Color.black;

    // シーンごとのフェード色設定
    [Serializable]
    public class SceneFadeColor
    {
        public string sceneName;
        public Color color = Color.black;//デフォルトカラー
    }

    public List<SceneFadeColor> sceneFadeColors = new List<SceneFadeColor>();

    public void Awake ()
	{
		if (this != Instance) {
			Destroy (this.gameObject);
			return;
		}

		DontDestroyOnLoad (this.gameObject);
	}

	public void OnGUI ()
	{
		// Fade .
		if (this.isFading) {
			//色と透明度を更新して白テクスチャを描画 .
			this.fadeColor.a = this.fadeAlpha;
			GUI.color = this.fadeColor;
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
		}

	}

	/// <summary>
	/// 画面遷移 .
	/// </summary>
	/// <param name='scene'>シーン名</param>
	/// <param name='interval'>暗転にかかる時間(秒)</param>
	public void LoadScene (string scene, float interval)
	{
		StartCoroutine (TransScene (scene, interval));
	}

	/// <summary>
	/// シーン遷移用コルーチン .
	/// </summary>
	/// <param name='scene'>シーン名</param>
	/// <param name='interval'>暗転にかかる時間(秒)</param>
	private IEnumerator TransScene (string scene, float interval)
	{
        // 遷移先シーンの色を取得
        fadeColor = GetSceneFadeColor(scene);

        //だんだん暗く .暗転 フェードイン
        this.isFading = true;
		float time = 0;
		while (time <= interval) {
			this.fadeAlpha = Mathf.Lerp (0f, 1f, time / interval);
			time += Time.deltaTime;
			yield return 0;
		}

		//シーン切替 .
		SceneManager.LoadScene (scene);

		//だんだん明るく .暗転 フェードアウト
		time = 0;
		while (time <= interval) {
			this.fadeAlpha = Mathf.Lerp (1f, 0f, time / interval);
			time += Time.deltaTime;
			yield return 0;
		}

		this.isFading = false;
	}

    // シーン名から色を探す
    private Color GetSceneFadeColor(string sceneName)
    {
        foreach (SceneFadeColor data in sceneFadeColors)
        {
            if (data.sceneName == sceneName)
            {
                return data.color;
            }
        }

        // 設定がない場合はデフォルトカラー
        return Color.black;
    }
}
