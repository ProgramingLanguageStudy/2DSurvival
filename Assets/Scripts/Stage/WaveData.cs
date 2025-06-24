using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 특정 웨이브(시간 구간)의 적 스폰 및 능력치 배율 설정 데이터
/// </summary>
[System.Serializable]
public class WaveData
{
    [SerializeField] float _initialTime;        // 웨이브 시작 시간
    [SerializeField] float _spawnSpan;          // 적 스폰 간격(초)
    [SerializeField] float[] _spawnRates;       // 적 종류별 스폰 확률
    [SerializeField] float _damageRate;         // 적 공격력 배율
    [SerializeField] float _hpRate;             // 적 체력 배율
    [SerializeField] float _expRate;            // 적 보상 경험치 배율

    public float InitialTime => _initialTime;
    public float SpawnSpan => _spawnSpan;
    public float DamageRate => _damageRate;
    public float HpRate => _hpRate;
    public float ExpRate => _expRate;

    /// <summary>
    /// 스폰 확률에 따라 랜덤하게 적 인덱스를 선택해 반환하는 함수
    /// </summary>
    /// <returns>선택된 적 인덱스</returns>
    public int GetRandomEnemyIndex()
    {
        float total = 0;
        foreach (float rate in _spawnRates)
        {
            total += rate;
        }

        float randomPoint = Random.value * total;
        for (int i = 0; i < _spawnRates.Length; i++)
        {
            if (randomPoint < _spawnRates[i])
            {
                return i;
            }
            else
            {
                randomPoint -= _spawnRates[i];
            }
        }
        return _spawnRates.Length - 1;
    }
}
