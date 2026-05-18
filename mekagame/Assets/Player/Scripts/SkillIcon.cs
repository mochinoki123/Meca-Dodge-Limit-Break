using UnityEngine;
using UnityEngine.SceneManagement;

public class SkillIcon : MonoBehaviour
{
    [SerializeField] private GameObject[] useSkill;
    [SerializeField] private GameObject[] skill;
    [SerializeField] private Transform[] iconTransform;
    GameObject[] s = new GameObject[3];

    PlayerParry parry;
    PlayerPulseDiffuser pulseDiffuser;
    OverClock overClock;
    LimitBreak limitBreak;
    Heal heal;
    GrazeAttack grazeAttack;
    CounterHeal counterHeal;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Awake()
    {
        UpdateSkill();
    }

    private void UpdateSkill()
    {
        int[] skillNumbers = { 0, 1, 2 ,3};

        for (int i = 0; i < useSkill.Length; i++)
        {
            useSkill[i] = skill[skillNumbers[i]];

            Vector3 spawnPos = iconTransform[i].position + new Vector3(0, -75, 0);

            //s[i] = Instantiate(useSkill[i], spawnPos, Quaternion.identity, transform);
        }
    }

    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        UpdateSkill();
    }
}