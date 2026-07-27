using UnityEngine;

public class Scale : MonoBehaviour
{
    public Transform missile;
    private float startHeight;

    void Start()
    {
        missile = transform.parent.Find("Missile");
        startHeight = missile.position.y;
    }
    void Update()
    {
        float t = Mathf.Clamp01(missile.position.y / startHeight);

        float currentScale = Mathf.Lerp(7f, 1f, t);

        transform.localScale = new Vector3(
            currentScale,
            transform.localScale.y,
            currentScale
        );
    }
}
