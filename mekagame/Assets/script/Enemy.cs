using TMPro;
using UnityEngine;
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
    }

    void EnemyHPtime()
    {
        EnemyHP = EnemyHP - 250;
        EnemyHp.text = "HP: " + EnemyHP.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
