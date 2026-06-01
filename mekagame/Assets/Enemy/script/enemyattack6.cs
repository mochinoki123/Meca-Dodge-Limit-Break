using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;


public class enemyattack6 : MonoBehaviour
{
    private enemyattack enemymanager;
    enemylazer enemyLazer;
    //攻撃６
    [Header("攻撃Ⅵ")]
    [SerializeField] int Attack6ms = 5;//攻撃６のミサイル数
    Vector3 play;
    //プレイヤー座標取得
    float x;
    float z;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //-----攻撃Ⅵ-----
    public void Attack6()
    {
        StartCoroutine(Attack6missileCoroutine());//追尾攻撃スタート
        

        //Debug.Log("攻撃Ⅵ");
    }

    //攻撃Ⅵ連続追尾ミサイル
    IEnumerator Attack6missileCoroutine()
    {
        int i = 0;
        while (i < Attack6ms)//追尾ミサイル上限数分繰り返す
        {
            Attack6missile();//攻撃Ⅵミサイル
            i++;
            yield return new WaitForSeconds(1.2f);
        }
        //Debug.Log("攻撃Ⅵ missile");
    }

    //攻撃Ⅵ追尾ミサイル
    void Attack6missile()
    {
        GameObject objm6 = ObjectPool_Missile.Instance.GetMissile();
        
        // transform.position で現在のワールド座標を取得
        Vector3 currentPosition = transform.position;
        //Debug.Log("プレイヤーの座標: " + currentPosition);

        // x, y, z 座標を個別に取得
        float x = currentPosition.x;
        float z = currentPosition.z;
        //Debug.Log("X座標: " + x + ", Z座標: " + z);

        Vector3 play = GameObject.Find("Player").transform.position;//プレイヤーの座標取得
        objm6.transform.position = new Vector3(play.x, 0.1f, play.z);
        objm6.transform.rotation = Quaternion.identity;//Quaternion.Euler(180, 0, 0);

    }

}
