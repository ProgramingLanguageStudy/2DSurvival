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
}
