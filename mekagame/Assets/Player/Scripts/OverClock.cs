using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class OverClock : MonoBehaviour
{
    [SerializeField] private int oCUseGage;
    [SerializeField] private float oCTime;
    [SerializeField] public float oCSpeed;
    [SerializeField] public float oCCoolTime;
    [SerializeField] public float oCGrazeRange;
    public bool isOC = false;
    SOCDkey key;
    PlayerGraze pg;

    private void Awake()
    {
        key = GetComponent<SOCDkey>();
        pg = GetComponentInChildren<PlayerGraze>();
    }
    private void OnOverClock(InputValue value)
    {
        if (!key.isGageAction) return;
        if (isOC) return;
        StartCoroutine(PlayOverClock());
    }
    private IEnumerator PlayOverClock()
    {
        if(GameManager.Instance.GetterGage() >= oCUseGage)
        {
            GameManager.Instance.UseGage(oCUseGage);
            isOC = true;
            pg.OCRange(oCGrazeRange);
            yield return new WaitForSeconds(oCTime);
            isOC = false;
            pg.Range();
        }
    }
}
