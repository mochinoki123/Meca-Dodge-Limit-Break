using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyManager : MonoBehaviour
{//EnemyƒXƒNƒŠƒvƒg
    private Enemy enemyhpscripts;
    enemyattack1 a1;
    enemyattack2 a2;
    enemyattack3 a3;
    enemyattack4 a4;
    enemyattack5 a5;
    enemyattack6 a6;
    int attack12345;//random’lŠm”F—pŠî–{g‚í‚È‚¢
    int attack123456;//random’lŠm”F—pŠî–{g‚í‚È‚¢
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyhpscripts = GetComponent<Enemy>();//“Gƒf[ƒ^ŒÄ‚Ño‚µ
        a1 = FindAnyObjectByType<enemyattack1>();
        a2 = FindAnyObjectByType<enemyattack2>();
        a3 = FindAnyObjectByType<enemyattack3>();
        a4 = FindAnyObjectByType<enemyattack4>();
        a5 = FindAnyObjectByType<enemyattack5>();
        a6 = FindAnyObjectByType<enemyattack6>();
        EnemyAttackController1();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EnemyAttackController1()
    {
        Invoke("Attack1", 3f);
        Invoke("Attack2", 5f);
        Invoke("Attack3", 8f);
        //Invoke("AttackLoop", 10f);
        Invoke("Attack4", 11f);
        Invoke("Attack5", 16f);
        Invoke("AttackLoop", 33f);
    }

    //-----UŒ‚ƒpƒ^[ƒ“‡U-----
    void EnemyAttackController2()
    {
        CancelInvoke("AttackLoop");
        /*
        Invoke("Attack4", 3f);
        Invoke("Attack5", 7f);
        Invoke("AttackLoop2", 22f);
        */
        Invoke("Attack6", 3f);
        Invoke("AttackLoop2", 8f);
    }

    //-----UŒ‚ƒpƒ^[ƒ“‡V-----
    void EnemyAttackController3()
    {
        CancelInvoke("AttackLoop2");
        Invoke("Attack6", 3f);
        Invoke("AttackLoop3", 8f);
    }

    //-----UŒ‚ƒpƒ^[ƒ“‡Tƒ‹[ƒv-----
    void AttackLoop()
    {
        StartCoroutine(AttackLoopCoroutine());//ƒ‹[ƒv“Ë“ü
    }

    //-----UŒ‚ƒpƒ^[ƒ“‡Tƒ‹[ƒv’EoğŒ-----
    IEnumerator AttackLoopCoroutine()
    {
        //ƒ‹[ƒv’EoğŒ
        while (enemyhpscripts.CurrentHP > 750)//“G‚ÌHPğŒ
        {
            //attack123 = Random.Range(0, 99);//ƒ‰ƒ“ƒ_ƒ€‚ÅUŒ‚•ªŠò
            attack12345 = Random.Range(0, 99);//ƒ‰ƒ“ƒ_ƒ€‚ÅUŒ‚•ªŠò
            Attackrnd();//UŒ‚ƒpƒ^[ƒ“‡T

            yield return new WaitForSeconds(2f);//2•b‚²‚Æ‚Éƒ‹[ƒv‚·‚é
        }

        //Debug.Log("UŒ‚’Ç‰Á");
        EnemyAttackController2();//UŒ‚ƒpƒ^[ƒ“‡U“Ë“ü
    }

    //-----UŒ‚ƒpƒ^[ƒ“‡T•ªŠò-----
    void Attackrnd()
    {
        /*
        if (attack123 <=33)
        {
            Attack1();//UŒ‚‡T
        }
        else if (attack123 <=66)
        {
            Attack2();//UŒ‚‡U
        }
        else
        {
            Attack3();//UŒ‚‡V
        }
        */
        if (attack12345 <= 20)
        {
            a1.Attack1();//UŒ‚‡T
        }
        else if (attack12345 <= 40)
        {
            a2.Attack2();//UŒ‚‡U
        }
        else if (attack12345 <= 60)
        {
            a3.Attack3();//UŒ‚‡V
        }
        else if (attack12345 <= 80)
        {
            a4.Attack4();//UŒ‚‡W
        }
        else
        {
            a5.Attack5();//UŒ‚‡X
        }
    }

    //-----UŒ‚ƒpƒ^[ƒ“‡Uƒ‹[ƒv-----
    void AttackLoop2()
    {
        StartCoroutine(AttackLoop2Coroutine());//ƒ‹[ƒv“Ë“ü
    }

    //-----UŒ‚ƒpƒ^[ƒ“‡Uƒ‹[ƒv’EoğŒ-----
    IEnumerator AttackLoop2Coroutine()
    {
        while (enemyhpscripts.CurrentHP > 500)//“G‚ÌHPğŒ
        {
            attack123456 = Random.Range(0, 99);//ƒ‰ƒ“ƒ_ƒ€‚ÅUŒ‚•ªŠò
            Attackrndv2();//UŒ‚ƒpƒ^[ƒ“‡U

            yield return new WaitForSeconds(1.5f);//2•b‚²‚Æ‚Éƒ‹[ƒv‚·‚é
        }

        //Debug.Log("UŒ‚’Ç‰Á‡U");
        EnemyAttackController3();//UŒ‚ƒpƒ^[ƒ“‡V“Ë“ü
    }

    //-----UŒ‚ƒpƒ^[ƒ“‡U•ªŠò-----
    void Attackrndv2()
    {
        /*
        if (attack12345 <= 20)
        {
            Attack1();//UŒ‚‡T
        }
        else if (attack12345 <= 40)
        {
            Attack2();//UŒ‚‡U
        }
        else if (attack12345 <= 60)
        {
            Attack3();//UŒ‚‡V
        }
        else if (attack12345 <= 80)
        {
            Attack4();//UŒ‚‡W
        }
        else
        {
            Attack5();//UŒ‚‡X
        }
        */
        if (attack123456 <= 16)
        {
            a1.Attack1();//UŒ‚‡T
        }
        else if (attack123456 <= 32)
        {
            a2.Attack2();//UŒ‚‡U
        }
        else if (attack123456 <= 48)
        {
            a3.Attack3();//UŒ‚‡V
        }
        else if (attack123456 <= 64)
        {
            a4.Attack4();//UŒ‚‡W
        }
        else if (attack123456 <= 80)
        {
            a5.Attack5();//UŒ‚‡X
        }
        else
        {
            a6.Attack6();//UŒ‚‡Y
        }
    }

    //-----UŒ‚ƒpƒ^[ƒ“‡Vƒ‹[ƒv-----
    void AttackLoop3()
    {
        StartCoroutine(AttackLoop3Coroutine());//ƒ‹[ƒv“Ë“ü
    }

    //-----UŒ‚ƒpƒ^[ƒ“‡Vƒ‹[ƒv’EoğŒ-----
    IEnumerator AttackLoop3Coroutine()
    {
        while (enemyhpscripts.CurrentHP > 250)//“G‚ÌHPğŒ
        {
            attack123456 = Random.Range(0, 99);//ƒ‰ƒ“ƒ_ƒ€‚ÅUŒ‚•ªŠò
            Attackrndv3();//UŒ‚ƒpƒ^[ƒ“‡V

            yield return new WaitForSeconds(1f);//2•b‚²‚Æ‚Éƒ‹[ƒv‚·‚é
        }

        //Debug.Log("UŒ‚’Ç‰Á");
        EnemyAttackController3();//UŒ‚ƒpƒ^[ƒ“@“Ë“ü
    }

    //-----UŒ‚ƒpƒ^[ƒ“‡V•ªŠò-----
    void Attackrndv3()
    {
        if (attack123456 <= 16)
        {
            a1.Attack1();//UŒ‚‡T
        }
        else if (attack123456 <= 32)
        {
            a2.Attack2();//UŒ‚‡U
        }
        else if (attack123456 <= 48)
        {
            a3.Attack3();//UŒ‚‡V
        }
        else if (attack123456 <= 64)
        {
            a4.Attack4();//UŒ‚‡W
        }
        else if (attack123456 <= 80)
        {
            a5.Attack5();//UŒ‚‡X
        }
        else
        {
            a6.Attack6();//UŒ‚‡Y
        }
    }
}
