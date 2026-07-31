//クリアフラグのスクリプダブルオブジェクト
//R3で変数を監視しているコードに値が変わったら通知がいく
using UnityEngine;
using R3;

[CreateAssetMenu(fileName = "ClearFlag", menuName = "Scriptable Objects/ClearFlag")]
public class ClearFlag : ScriptableObject
{
    [SerializeField] private string flagName;  //プラグの名前

    [SerializeField] private SerializableReactiveProperty<bool> isGameCleared = new(false);
    [SerializeField] private SerializableReactiveProperty<bool> isPhaseCleared = new(false);

    public ReactiveProperty<bool> IsGameCleared => isGameCleared;
    public ReactiveProperty<bool> IsPhaseCleared => isPhaseCleared;

    public void ResetGameFlag()
    {
        isGameCleared.Value = false;
    }

    public void ResetPhaseFlag()
    {
        isPhaseCleared.Value = false;
    }
}