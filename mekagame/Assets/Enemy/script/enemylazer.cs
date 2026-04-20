using UnityEngine;
using System.Collections;

public class enemylazer : MonoBehaviour
{
    [SerializeField] float maxLengthx = 50f;
    [SerializeField] float extendSpeed = 80f;
    enemyattack enemyAttack;

    public enum LazerAngle
    {
        //èc
        Ver,
        //â°
        Hor
    }
    private void Start()
    {
        enemyAttack = FindAnyObjectByType<enemyattack>();
        AngleLazer(LazerAngle.Hor);
    }
    public void AngleLazer(LazerAngle state)
    {
        switch(state)
        {
            case LazerAngle.Ver:
                transform.rotation = Quaternion.identity;
                StartCoroutine(ExtendLazer5());
                break;
            case LazerAngle.Hor:
                transform.rotation = Quaternion.Euler(0, 90, 0);
                StartCoroutine(ExtendLazer5());
                break;
        }
    }
    public void Kill()
    {
        enemyAttack.Return(gameObject);
    }

    IEnumerator ExtendLazer5()
    {
        Vector3 scale = transform.localScale;
        scale.z = 0; // ç≈èâÇÕí∑Ç≥0
        transform.localScale = scale;
        while (scale.z < maxLengthx)
        {
            scale.z += extendSpeed * Time.deltaTime;
            transform.localScale = scale;
            yield return null; 
        }
        yield return new WaitForSeconds(1f);
        enemyAttack.Returnlx(gameObject);
    }
}
