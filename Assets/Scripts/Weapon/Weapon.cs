using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 무기의 공통 기능을 포함하는 추상 클래스
/// abstract를 붙여 이 클래스로는 객체를 생성하지 않겠다.
/// </summary>
public abstract class Weapon : MonoBehaviour
{
    [Header("----- 스탯 데이터 -----")]
    // 무기 데이터
    [SerializeField] protected WeaponData _data;

    // 현재 무기 레벨
    [SerializeField] protected int _level;

    // 데미지
    [SerializeField] protected float _damage;

    /// <summary>
    /// 초기화 함수
    /// </summary>
    public abstract void Initialize();

    /// <summary>
    /// 레벨에 따른 무기 스텟을 계산하는 함수
    /// </summary>
    protected virtual void CalculateStats()
    {
        // 무기 레벨에 따른 데미지 계산
        _damage = _data.GetStat(WeaponStatType.Damage, _level);
    }
}
