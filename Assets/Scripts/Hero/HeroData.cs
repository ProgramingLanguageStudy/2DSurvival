using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 주인공 기본 능력치를 포함하는 설정 데이터 클래스
/// </summary>
[CreateAssetMenu(fileName = "HeroData", menuName = "GameSettings/HeroData")]
public class HeroData : ScriptableObject
{
    [SerializeField] float _maxHp;      // 기본 최대 체력
    [SerializeField] float _speed;      // 기본 이동 속력
    [SerializeField] float _maxExp;        // 경험치통

    public float MaxHp => _maxHp;
    public float Speed => _speed;
    public float MaxExp => _maxExp;
}
