using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 장비 공통 기능을 담을 추상 클래스
/// </summary>
public abstract class Gear : MonoBehaviour
{
    [Header("----- 대상 모델 -----")]
    [SerializeField] protected HeroModel _heroModel;

    [Header("----- 설정 데이터 -----")]
    [SerializeField] protected GearData _data;

    [Header("----- 스탯 -----")]
    [SerializeField] protected int _level;        // 기어 레벨
    [SerializeField] protected float _bonusValue; // 레벨에 따른 보너스 수치

    /// <summary>
    /// 현재 레벨에 따른 스탯 갱신
    /// </summary>
    void CalculateStats()
    {
        _bonusValue = _data.GetValue(_level);
    }
    
    /// <summary>
    /// 장비를 업그레이드 하는 함수
    /// </summary>
    public void Upgrade()
    {
        _level++;           // 기어의 레벨 하나 올려주고
        CalculateStats();   // 다시 스탯 갱신하고
        Apply();            // 장비 효과 적용
    }

    /// <summary>
    /// 장비 효과를 적용하는 함수
    /// </summary>
    protected abstract void Apply();
}
