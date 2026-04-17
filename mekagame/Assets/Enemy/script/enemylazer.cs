using UnityEngine;
using System.Collections;

public class enemylazer : MonoBehaviour
{
    [SerializeField] float maxLengthx = 50f;
    [SerializeField] float extendSpeed = 80f;
    enemyattack enemyAttack;

    public enum LazerAngle
    {
        //縦
        Vertical,
        //横
        Horizontal
    }
    private void Start()
    {
        enemyAttack = FindAnyObjectByType<enemyattack>();
    }
    public void AngleLazer(LazerAngle state)
    {
        switch(state)
        {
            case LazerAngle.Vertical:
                transform.rotation = Quaternion.identity;
                StartCoroutine(ExtendLazer5(gameObject));
                break;
            case LazerAngle.Horizontal:
                transform.rotation = Quaternion.Euler(0, 90, 0);
                StartCoroutine(ExtendLazer5(gameObject));
                break;
        }
    }
    public void Kill()
    {
        enemyAttack.Return(gameObject);
    }

    IEnumerator ExtendLazer5(GameObject gameObject)
    {
        Vector3 scale = transform.localScale;
        scale.x = 0; // 最初は長さ0
        transform.localScale = scale;
        while (scale.x < maxLengthx)
        {
            scale.x += extendSpeed * Time.deltaTime;
            transform.localScale = scale;
            yield return null; // 次のフレームへ
        }
        yield return new WaitForSeconds(1f);
        // 最終値を保証
        //scale.x = maxLength;
        enemyAttack.Returnlx(gameObject);
    }
}
