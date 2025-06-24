using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EnemyStatData", menuName = "GameSettings/Enemy/EnemyStatData")]
public class EnemyStatData : ScriptableObject
{
    [Header("----- 기본 스탯 -----")]
    [SerializeField] float _baseDamage;
    [SerializeField] float _baseMoveSpeed;
    [SerializeField] float _baseHp;
    [SerializeField] float _baseExpReward;

    public float BaseDamage => _baseDamage;
    public float BaseMoveSpeed => _baseMoveSpeed;
    public float BaseHp => _baseHp;
    public float BaseExpReward => _baseExpReward;
}
