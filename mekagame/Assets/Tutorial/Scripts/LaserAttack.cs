using System.Runtime.ConstrainedExecution;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using System.Collections;

public class LaserAttack : MonoBehaviour
{
    [SerializeField] Transform [] verPosition;
    [SerializeField] Transform [] horPosition;

    Vector3 spawnPos;

    public enum LaserState { ver = 0, hor = 270 }

    public void FireLaser(LaserState state, int positionIndex)
    {
        Vector3 spawnPos = state == LaserState.ver ? verPosition[positionIndex].position : horPosition[positionIndex].position;
        Quaternion spawnRot = Quaternion.Euler(0, (int)state, 0);
        ObjectPool_Lazer.instance.Spawn(spawnPos, spawnRot);
    }

    IEnumerator SeriesFireVer()
    {
        for (int i = 0; i < verPosition.Length; i++)
        {
            FireLaser(LaserState.ver, i);

            yield return new WaitForSeconds(2f);
        }
    }

    IEnumerator SeriesFireHor()
    {
        for (int i = 0; i < horPosition.Length; i++)
        {
            FireLaser(LaserState.hor, i);

            yield return new WaitForSeconds(2f);
        }
    }

    //  Timeline—p
    public void FireVer(int pos)
    {
        FireLaser(LaserState.ver, pos);
    }

    public void FireHor(int pos)
    {
        FireLaser(LaserState.hor, pos);
    }

    public void RandomFireVer()
    {
        int rnd = Random.Range(0, verPosition.Length);
        FireLaser(LaserState.ver, rnd);
    }

    public void RandomFireHor()
    {
        int rnd = Random.Range(0, horPosition.Length);
        FireLaser(LaserState.hor, rnd);
    }

    public void CoroutineLaserVer()
    {
        StartCoroutine(SeriesFireVer());
    }

    public void CoroutineLaserHor()
    {
        StartCoroutine(SeriesFireHor());
    }
}
