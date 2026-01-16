using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI EnemyHp;
  //[SerializeField] public int EnemyMAXHP = 1000;
    [SerializeField] public int EnemyHP = 1000;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnemyHp.text = "HP: " + EnemyHP.ToString();
        Invoke("EnemyHPtime",30f);
        Invoke("EnemyHPtime",60f);
        Invoke("EnemyHPtime",90f);
    }

    void EnemyHPtime()
    {
        EnemyHP = EnemyHP - 250;
        EnemyHp.text = "HP: " + EnemyHP.ToString();
    }

    public void Damage(int damage)
    {
        EnemyHP -= damage;
        EnemyHp.text = "HP: " + EnemyHP.ToString();
        Enemydown();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Enemydown()
    {
        if (EnemyHP<=0)
        {
            SceneManager.LoadScene("Result");//シーン名を入れる
        }
    }
    //シーン移動使う場合
    /*
    SceneManager.LoadScene("Scene");//シーン名を入れる
    */
}
