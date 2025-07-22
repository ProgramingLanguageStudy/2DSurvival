﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 주인공 캐릭터의 데이터 로직을 담당하는 역할
/// </summary>
public class HeroModel : MonoBehaviour
{
    [Header("----- 스탯(인스펙터 뷰에서 보기위해만듬) -----")]
    [SerializeField] float _maxHp;      // 최대 체력
    [SerializeField] float _speed;      // 이동 속력
    [SerializeField] float _currentHp;  // 현재 체력
    [SerializeField] float _currentExp; // 현재 경험치
    [SerializeField] float _maxExp;     // 최대 경험치(필요 경험치)
    [SerializeField] int _level;        // 현재 레벨

    HeroStatData _heroStatData;

    // 체력 변경 이벤트
    public event UnityAction<float, float> OnHpChanged;
    // 이동 속력 변경 이벤트
    public event UnityAction<float> OnSpeedChanged;
    // 사망 이벤트
    public event UnityAction OnDeath;
    // 경험치 변화 이벤트
    public event UnityAction<float, float> OnExpChanged;
    // 레벨 변화 이벤트
    public event UnityAction<int, int> OnLevelChanged;

    // 프로퍼티 (읽기 전용)
    public float MaxHp => _maxHp;
    public float CurrentHp => _currentHp;
    public float Speed => _speed;
    public float MaxExp => _maxExp;
    public float CurrentExp => _currentExp;
    public int Level => _level;

    // 초기화
    public void Initialize(HeroStatData heroStatData)
    {
        // HeroStatData를 받아와서 초기화
        _heroStatData = heroStatData;

        _maxHp = _heroStatData.MaxHp;
        _speed = _heroStatData.Speed;
        _speed = 3f;
        _maxExp = _heroStatData.GetExp(_level);
        _level = 0;

        _currentHp = _maxHp;

        // 초기 체력과 이동 속력 변경 이벤트 발행
        OnSpeedChanged?.Invoke(_speed);
        OnHpChanged?.Invoke(_currentHp, _maxHp);

        // 경험치 변경과 레벨 변경 이벤트 발행
        OnExpChanged?.Invoke(_currentExp, _maxHp);
        OnLevelChanged?.Invoke(_level, _level);
    }

    public void TakeDamage(float amount)
    {
        // 현재 체력이 0 이하인 경우, 더 이상 데미지를 받지 않음
        if (_currentHp <= 0) return;

        // 체력 감소가 마이너스로 들어와서(회복처럼 될 경우)
        // 최대 체력을 초과하지 않도록 제한
        _currentHp = Mathf.Min(_currentHp - amount, _maxHp);

        // 체력 변경 이벤트 발행
        OnHpChanged?.Invoke(_currentHp, _maxHp);

        // 현재 체력이 0 이하가 되면
        if (_currentHp <= 0)
        {
            // 사망 이벤트 발행
            OnDeath?.Invoke();
        }
    }

    /// <summary>
    /// 추가 체력을 적용하는 함수
    /// </summary>
    /// <param name="bounusHp"></param>
    public void SetBonusMaxHp(float bounusHp)
    {
        // 현재 체력 비율을 계산
        float ratio = _currentHp / _maxHp;

        // 최대 체력을 bonusHp만큼 증가
        _maxHp = _heroStatData.MaxHp + bounusHp;

        // 최대 체력 증가 후, 현재 체력을 비율에 맞게 조정
        _currentHp = _maxHp * ratio;

        // 체력 변경 이벤트 발행
        OnHpChanged?.Invoke(_currentHp, _maxHp);
    }

    /// <summary>
    /// 추가 속력 증가율을 적용하는 함수
    /// </summary>
    /// <param name="speedRatio"></param>
    public void SetBonusSpeedRation(float speedRatio)
    {
        // 현재 속력에 speedRatio를 적용하여 새로운 속력을 계산
        _speed = (1 + speedRatio) * _heroStatData.Speed;

        // 이동 속력 변경 이벤트 발행
        OnSpeedChanged?.Invoke(_speed);
    }

    /// <summary>
    /// 경험치를 추가하는 함수
    /// </summary>
    /// <param name="amount"></param>
    public void AddExp(float amount)
    {
        // 경험치가 0 이하인 경우, 추가하지 않음
        if (amount <= 0) return;

        // 현재 경험치에 추가 경험치를 더함
        _currentExp += amount;

        // 경험치가 최대 경험치 이상인 경우 반복
        while (_currentExp >= _maxExp)
        {
            // 레벨업 처리
            LevelUp();
        }

        // 경험치 변경 이벤트 발행
        OnExpChanged?.Invoke(_currentExp, _maxExp);
    }

    /// <summary>
    /// 레벨업을 처리하는 함수
    /// </summary>
    private void LevelUp()
    {
        // 레벨업 전 이전 레벨 변수
        int preLevel = _level;

        // 현재 경험치를 이번 레벨 업 필요치만큼 감소
        _currentExp -= _maxExp;

        // 레벨 1 증가
        _level++;

        // 최대 경험치를 변화된 레벨에 따라 갱신
        _maxExp = _heroStatData.GetExp(_level);

        // 레벨업 이벤트 발행
        OnLevelChanged?.Invoke(preLevel, _level);
    }

}
