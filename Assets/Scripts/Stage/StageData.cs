using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 스테이지 설정 데이터(전체 플레이 시간, 웨이브 데이터)
/// </summary>
[CreateAssetMenu(fileName = "StageData", menuName = "GameSettings/StageData")]
public class StageData : ScriptableObject
{
    [SerializeField] WaveData[] _waveDatas;     // 시작 시간 순서대로 정렬된 웨이브 데이터 배열
    [SerializeField] float _playTime;           // 스테이지의 전체 플레이 시간

    public float PlayTime => _playTime;

    /// <summary>
    /// 현재 플레이 시간에 해당하는 웨이브 데이터를 반환
    /// </summary>
    /// <param name="time">현재 플레이 시간</param>
    /// <returns></returns>
    public WaveData GetWaveData(float time)
    {
        for (int i = _waveDatas.Length - 1; i >= 0; i--)
        {
            if (time > _waveDatas[i].InitialTime)
            {
                return _waveDatas[i];
            }
        }
        return _waveDatas[0];
    }
}
