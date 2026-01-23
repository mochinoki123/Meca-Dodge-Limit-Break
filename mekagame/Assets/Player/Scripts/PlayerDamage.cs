using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField] private float mutekiTime;
    private PlayerMove playerMove;
    private PlayerParry playerParry;
    private PlayerPulseDiffuser playerPulseDiffuser;
    private bool isMuteki = false;
    private void Awake()
    {
        playerMove = GetComponent<PlayerMove>();
        playerParry = GetComponent<PlayerParry>();
        playerPulseDiffuser = GetComponent<PlayerPulseDiffuser>();

        GetComponent<Renderer>().material.color = Color.red;
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
        yield return new WaitForSeconds(mutekiTime);
        isMuteki = false;
    }
}
