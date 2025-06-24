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
}
