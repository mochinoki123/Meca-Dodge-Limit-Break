using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private int maxHP;
    [SerializeField] private int maxGage;
    [SerializeField] private Text hpText;
    [SerializeField] private Text gageText;
    [SerializeField] private Text comboText;
    [SerializeField] private int nowHP;
    [SerializeField] private float nowGage;
    [SerializeField] private GameObject player;
    [SerializeField] private float comboTime;
    [SerializeField] private float comboMultiple1;
    [SerializeField] private float comboMultiple2;
    [SerializeField] private float comboMultiple3;

    private float comboMultiple;
    private int combo;
    private  int maxCombo;
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
    private void Update()
    {
        //if(combo > 0)
        //{
            
        //}
    }
    public void UpdateText()
    {
        hpText.text = "HP : " + nowHP;
        gageText.text = "Gage : " + nowGage;
        //comboText.text = "Combo : " + combo;
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
            nowGage += comboMultiple * n;
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
    public float GetterGage()
    {
        return nowGage;
    }
    public void Die(GameObject obj)
    {
        Destroy(obj);
    }
}
