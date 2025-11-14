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
    [SerializeField] private int addGage;
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
        if(nowHP > 0) nowHP --;
    }
    public void ResetHP()
    {
        nowHP = maxHP;
    }
    public void AddGage()
    {
        if (nowGage < maxGage) nowGage += addGage;
    }
    public void ResetGage()
    {
        nowGage = 0;
    }

}
