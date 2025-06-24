using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ��� �����͹����� ����Ʈ�� ���� �ִ� �����ͺ��̽�
/// </summary>
[CreateAssetMenu(fileName = "EnemyDatabase", menuName = "GameSettings/Enemy/EnemyDatabase")]
public class EnemyDatabase : ScriptableObject
{
    public List<EnemyDataBundle> enemyDataBundleList;

    /// <summary>
    /// ���� Ȯ���� ���� �����ϰ� �� �ε����� ������ ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <returns>���õ� �� �ε���</returns>
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
