using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class enemyattack1 : MonoBehaviour
{
    private enemyattack enemymanager;
    //攻撃１
    [Header("攻撃Ⅰ")]
    public float missilespeed = 45f;
    [SerializeField] int attack1missile = 10;//攻撃１のミサイル数　6
    [SerializeField] float rndm = -9;//フィールドごとの範囲指定マイナス
    [SerializeField] float rndp = 9;//フィールドごとの範囲指定プラス
    [SerializeField] int attackf = 5;//攻撃の間隔 5
    [SerializeField] int attackpointx = 10;//攻撃発生の横 10
    [SerializeField] int attackpointz = 10;//攻撃発生の奥行 10
    public Animator animator;

    float groundx;//random値確認用基本使わない
    float groundz;//random値確認用基本使わない
    float attackbunki;//random値確認用基本使わない

    public void Attack1()
    {
        animator.SetTrigger("IsMissile");
        attackbunki = Random.Range(0f, 1f);
        if (attackbunki < 0.5f)
        {
            for (int i = 0; i < attack1missile; i++)
            {
                GameObject objm1 = ObjectPool_Missile.Instance.GetMissile();

                groundx = Random.Range(rndm, rndp);//地面の広さによって変更
                groundz = Random.Range(rndm, rndp);//地面の広さによって変更

                objm1.transform.position = new Vector3((attackf * groundx) - groundx, 0.1f, (attackf * groundz) - groundz);
                //objm1.transform.rotation = Quaternion.Euler(180, 0, 0);
                objm1.transform.rotation = Quaternion.identity;
                //objm1.SetActive(true);
            }
        }
        else
        {
            StartCoroutine(Attack1missileCoroutine());
            
        }
        IEnumerator Attack1missileCoroutine()
        {
            for (int i = 0; i < attack1missile; i++)
            {
                GameObject objm1 = ObjectPool_Missile.Instance.GetMissile();
                groundx = Random.Range(rndm, rndp);//地面の広さによって変更
                groundz = Random.Range(rndm, rndp);//地面の広さによって変更

                objm1.transform.position = new Vector3((attackf * groundx) - groundx, 0.1f, (attackf * groundz) - groundz);
                //objm1.transform.rotation = Quaternion.Euler(180, 0, 0);
                objm1.transform.rotation = Quaternion.identity;
                //objm1.SetActive(true);
                yield return new WaitForSeconds(0.1f);
            }
        }
            //objm1.SetActive(true);

    }
}
