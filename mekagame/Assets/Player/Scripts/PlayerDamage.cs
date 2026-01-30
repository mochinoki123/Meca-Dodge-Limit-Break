using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField] private float mutekiTime;
    [SerializeField] private int loopCount;
    private PlayerMove playerMove;
    private PlayerParry playerParry;
    private PlayerPulseDiffuser playerPulseDiffuser;
    private Renderer rend;

    private bool isMuteki = false;
    private float alpha_Sin;
    private void Awake()
    {
        playerMove = GetComponent<PlayerMove>();
        playerParry = GetComponent<PlayerParry>();
        playerPulseDiffuser = GetComponent<PlayerPulseDiffuser>();
        rend = GetComponentInChildren<Renderer>();
    }
    private void Update()
    {
        alpha_Sin = Mathf.Sin(Time.time) / 2 + 0.5f;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (playerMove.isRun) return;
        if (playerParry.isParry) return;
        if (playerPulseDiffuser.isPD) return;
        if (isMuteki) return;
        bool isDamage = false;
        if (other.CompareTag("Missile"))
        {
            var missile = other.GetComponentInParent<enemymissile>();
            if (missile != null)
            {
                missile.Kill();
                GameManager.Instance.Damage();
                isDamage = true;
            }
        }
        if (other.CompareTag("Lazer"))
        {
            var lazer = other.GetComponentInParent<enemylazer>();
            if (lazer != null)
            {
                lazer.Kill();
                GameManager.Instance.Damage();
                isDamage = true;
            }
        }
        if(isDamage)
        {
            StartCoroutine(MutekiTime());
        }
    }
    private IEnumerator MutekiTime()
    {
        isMuteki = true;

        for (int i = 0; i < loopCount; i++)
        {
            rend.enabled = false; 
            yield return new WaitForSeconds(0.1f);

            rend.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }

        rend.enabled = true;
        isMuteki = false;
    }
}
