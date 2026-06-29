using UnityEngine;

[CreateAssetMenu(fileName = "ClearFlag", menuName = "Scriptable Objects/ClearFlag")]
public class ClearFlag : ScriptableObject
{
    [SerializeField] private string flagName;
    [SerializeField] private bool isCleared = false;

    public bool IsCleared
    {
        get => isCleared;
        set => isCleared = value;
    }

    public void ResetFlag()
    {
        isCleared = false;
    }
}
