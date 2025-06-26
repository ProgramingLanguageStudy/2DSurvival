using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 적의 모든 데이터들을 묶음으로 관리하는 SO 클래스
/// 각 데이터들을 묶어 한번에 보기 편하게 만들어 놓은 것으로
/// 이 번들을 거쳐야만 Data들에 접근이 가능하다.
/// </summary>
[CreateAssetMenu(fileName = "EnemyDataBundle", menuName = "GameSettings/Enemy/EnemyDataBundle")]
public class EnemyDataBundle : ScriptableObject
{
    [SerializeField] int _enemyId;
    [SerializeField] EnemyStatData _enemyStatData;

    [SerializeField] GameObject _enemyPrefab;

    public int EnemyId => _enemyId;
    public EnemyStatData EnemyStatData => _enemyStatData;
    public GameObject EnemyPrefab => _enemyPrefab;
}
