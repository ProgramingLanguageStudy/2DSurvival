using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 적 캐릭터의 데이터 로직을 담당하는 역할
/// </summary>
public class EnemyModel : MonoBehaviour
{
    [Header("----- 기본 스탯 -----")]
    [SerializeField] float _baseDamage;
    [SerializeField] float _baseHp;
    [SerializeField] float _baseExpReward;

    [Header("----- 실제 스탯 -----")]
    // 공격력
    [SerializeField] float _damage;
    // 보상 경험치
    [SerializeField] float _expReward;
    // 최대 체력
    [SerializeField] float _maxHp;

    // 임시
    // 현재 체력
    [SerializeField] float _currentHp;

    // 체력 변경 이벤트
    public event UnityAction<float, float> OnHpChanged;
    // 사망 이벤트
    public event Action OnDeath;

    public float Damage => _damage;
    public float MaxHp => _maxHp;
    public float CurrentHp => _currentHp;
    public float ExpReward => _expReward;

    //public void Initialize(WaveData waveData)
    //{
    //    _damage = _baseDamage * waveData.DamageRate;
    //    _maxHp = _baseHp * waveData.HpRate;
    //    _expReward = _baseExpReward * waveData.ExpRate;

    //    _currentHp = _maxHp;
    //}

    public void Initialize(WaveData waveData)
    {
        _damage = _baseDamage * waveData.DamageRate;
        _maxHp = _baseHp * waveData.HpRate;
        _expReward = _baseExpReward * waveData.ExpRate;

        _currentHp = _maxHp;
    }


    public void TakeDamage(float amount)
    {
        if (_currentHp <= 0) return;
        
        _currentHp = Mathf.Min(_currentHp - amount, _maxHp);

        // 체력 변경 이벤트 발행
        OnHpChanged?.Invoke(_currentHp, _maxHp);

        if (_currentHp <= 0)
        {
            // 사망 이벤트 발행
            OnDeath?.Invoke();
        }
    }
}
