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
    [SerializeField] float _damageRate;         // 적 공격력 배율
    [SerializeField] float _hpRate;             // 적 체력 배율
    [SerializeField] float _expRate;            // 적 보상 경험치 배율

    [System.Serializable]
    public class EnemySpawnRate
    {
        public int EnemyId;
        public float Rate;
    }

    [SerializeField]
    List<EnemySpawnRate> _spawnRates;

    public float InitialTime => _initialTime;
    public float SpawnSpan => _spawnSpan;
    public float DamageRate => _damageRate;
    public float HpRate => _hpRate;
    public float ExpRate => _expRate;

    /// <summary>
    /// 스폰 확률에 따라 랜덤하게 적 인덱스를 선택해 반환하는 함수
    /// </summary>
    public int GetRandomEnemyId()
    {
        float total = 0f;
        foreach (var spawn in _spawnRates)
            total += spawn.Rate;

        float rand = Random.value * total;

        foreach (var spawn in _spawnRates)
        {
            if (rand < spawn.Rate)
                return spawn.EnemyId;
            rand -= spawn.Rate;
        }

        return _spawnRates[_spawnRates.Count - 1].EnemyId;
    }

}
