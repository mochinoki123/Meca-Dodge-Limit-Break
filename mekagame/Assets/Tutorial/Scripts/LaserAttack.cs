using UnityEngine;
using System.Collections;

public class LaserAttack : MonoBehaviour
{
    [Header("生成位置（Inspectorで各方向にTransformをセット）")]
    [SerializeField] Transform[] northPositions;
    [SerializeField] Transform[] southPositions;
    [SerializeField] Transform[] eastPositions;
    [SerializeField] Transform[] westPositions;

    public enum LaserState { North = 90, South = 270, East = 180, West = 0 }

    /// <summary>
    /// 基本の発射メソッド
    /// </summary>
    public void FireLaser(LaserState state, int positionIndex)
    {
        Transform[] targetArray = GetArrayByState(state);

        if (targetArray == null || positionIndex < 0 || positionIndex >= targetArray.Length)
        {
            Debug.LogWarning($"{state} 方向の Index:{positionIndex} が見つかりません。");
            return;
        }

        Vector3 spawnPos = targetArray[positionIndex].position;

        Quaternion spawnRot = Quaternion.Euler(0, (int)state, 0);

        if (ObjectPool_Lazer.instance != null)
        {
            ObjectPool_Lazer.instance.Spawn(spawnPos, spawnRot);
        }
    }


    /// <summary>
    /// 指定した方向から順番に発射する
    /// </summary>
    public void StartSeriesFire(LaserState state, float interval = 0.5f)
    {
        StartCoroutine(SeriesFireRoutine(state, interval));
    }

    private IEnumerator SeriesFireRoutine(LaserState state, float interval)
    {
        Transform[] targetArray = GetArrayByState(state);
        if (targetArray == null) yield break;

        for (int i = 0; i < targetArray.Length; i++)
        {
            FireLaser(state, i);
            yield return new WaitForSeconds(interval);
        }
    }

    // --- ランダム発射（全方向対応版） ---

    public void FireRandom(LaserState state)
    {
        Transform[] targetArray = GetArrayByState(state);
        if (targetArray == null || targetArray.Length == 0) return;

        int rnd = Random.Range(0, targetArray.Length);
        FireLaser(state, rnd);
    }

    // --- チュートリアル ---

    public void FireTutorial(LaserState state)
    {
        FireLaser(state, 11);
    }

    // --- ヘルパーメソッド（内部処理用） ---

    private Transform[] GetArrayByState(LaserState state)
    {
        return state switch
        {
            LaserState.North => northPositions,
            LaserState.South => southPositions,
            LaserState.East => eastPositions,
            LaserState.West => westPositions,
            _ => null
        };
    }

    // --- Timeline / Inspector 呼び出し用のショートカット ---

    public void FireNorthRandom() => FireRandom(LaserState.North);
    public void FireSouthRandom() => FireRandom(LaserState.South);
    public void FireEastRandom() => FireRandom(LaserState.East);
    public void FireWestRandom() => FireRandom(LaserState.West);

    public void StartNorthSeries() => StartSeriesFire(LaserState.North);
    public void StartSouthSeries() => StartSeriesFire(LaserState.South);
    public void StartEastSeries() => StartSeriesFire(LaserState.East);
    public void StartWestSeries() => StartSeriesFire(LaserState.West);


    public void FireTutorialEast() => FireTutorial(LaserState.East);
}