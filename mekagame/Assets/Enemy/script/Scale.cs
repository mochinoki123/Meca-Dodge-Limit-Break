using UnityEngine;

public class Scale : MonoBehaviour
{
    public Transform missile;
    private float startHeight;

    void Start()
    {
        missile = transform.parent.Find("Missile");//ミサイルの位置取得
        startHeight = missile.position.y;
    }
    void Update()
    {
        float t = Mathf.Clamp01(missile.position.y / startHeight);

        float currentScale = Mathf.Lerp(7f, 1f, t);//ミサイル発生時点のポイントのスケール

        //ミサイルの距離でポイントのスケールが変更する
        transform.localScale = new Vector3(
            currentScale,
            transform.localScale.y,
            currentScale
        );
    }
}
