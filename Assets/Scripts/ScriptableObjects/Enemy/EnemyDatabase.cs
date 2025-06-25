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
    
    public EnemyDataBundle GetDataById(int id)
    {
        return enemyDataBundleList.Find(b => b.EnemyId == id);
    }
}
