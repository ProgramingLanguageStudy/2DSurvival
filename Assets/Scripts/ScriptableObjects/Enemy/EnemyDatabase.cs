using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 적의 모든 데이터번들을 리스트로 갖고 있는 데이터베이스
/// </summary>
[CreateAssetMenu(fileName = "EnemyDatabase", menuName = "GameSettings/Enemy/EnemyDatabase")]
public class EnemyDatabase : ScriptableObject
{
    public List<EnemyDataBundle> enemyDataBundleList;

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
