using UnityEngine;

[CreateAssetMenu(fileName = "ClearFlag", menuName = "Scriptable Objects/ClearFlag")]
public class ClearFlag : ScriptableObject
{
    [SerializeField] private string flagName;
    [SerializeField] private bool isGameCleared = false;
    [SerializeField] private bool isPhaseCleared = false;

    public bool IsGameCleared
    {
        get => isGameCleared;
        set => isGameCleared = value;
    }

    public bool IsPhaseCleared
    {
        get => isPhaseCleared;
        set => isPhaseCleared = value;
    }

    public void ResetGameFlag()
    {
        isGameCleared = false;
    }

    public void ResetPhaseFlag()
    {
        isPhaseCleared = false;                 
    }
}
