using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class PlayerResource : MonoBehaviour
{
    public static PlayerResource Instance { get; private set; }
    [SerializeField] private int maxHP;
    [SerializeField] private int maxGage;
    [SerializeField] private Text hpText;
    [SerializeField] private Text gageText;
    [SerializeField] private int nowHP;
    [SerializeField] private int nowGage;
    [SerializeField] private GameObject player;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }
    private void Start()
    {
        ResetHP();
        ResetGage();
        UpdateText();
    }
    public void UpdateText()
    {
        hpText.text = "HP : " + nowHP;
        gageText.text = "Gage : " + nowGage; 
    }
    public void Damage()
    {
        if(nowHP > 1)
        {
            nowHP--;
        }
        else if(nowHP == 1)
        {
            nowHP = 0;
            Die(player);
        }
        UpdateText();
    }
    public void ResetHP()
    {
        nowHP = maxHP;
        UpdateText();
    }
    public void AddGage(int n)
    {
        if (nowGage < maxGage)
        {
            nowGage += n;
            UpdateText();
        }
    }
    public void ResetGage()
    {
        nowGage = 0;
        UpdateText();
    }
    public void UseGage(int n)
    {
        nowGage -= n;
        UpdateText();
    }
    public int GetterGage()
    {
        return nowGage;
    }
    public void Die(GameObject obj)
    {
        Destroy(obj);
    }
}
