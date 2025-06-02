using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 주인공 캐릭터의 데이터 로직을 담당하는 역할
/// </summary>
public class HeroModel : MonoBehaviour
{
    [Header("----- 설정 데이터 -----")]
    [SerializeField] HeroData _data;    // HeroData 연결

    [Header("----- 스탯(인스펙터 뷰에서 보기위해만듬) -----")]
    [SerializeField] float _maxHp;      // 최대 체력
    [SerializeField] float _speed;      // 이동 속력
    [SerializeField] float _maxExp;     // 경험치 통
    [SerializeField] float _currentHp;  // 현재 체력
    [SerializeField] float _currentExp; // 현재 경험치량

    [Header("----- 레벨 -----")]
    [SerializeField] float _level = 1;        // 현재 레벨
    [SerializeField] float _maxLevel = 100;   // 최대 레벨

    // 체력 변경 이벤트
    public event UnityAction<float, float> OnHpChanged;
    // 이동 속력 변경 이벤트
    public event UnityAction<float> OnSpeedChanged;
    // 경험치 변경 이벤트
    public event UnityAction<float> OnExpChanged;
    // 레벨업 이벤트
    public event UnityAction OnLevelUp;
    // 사망 이벤트
    public event UnityAction OnDeath;

    // 프로퍼티 (읽기 전용)
    public float MaxHp => _maxHp;
    public float CurrentHp => _currentHp;
    public float Speed => _speed;
    public float MaxExp => _maxExp;
    public float CurrentExp => _currentExp;

    // 초기화
    public void Initialize()
    {
        _maxHp = _data.MaxHp;
        _speed = _data.Speed;
        _maxExp = _data.MaxExp;

        _currentHp = _maxHp;
        _currentExp = 0;

        // 초기 체력과 이동 속력 변경 이벤트 발행
        OnSpeedChanged?.Invoke(_speed);
        OnHpChanged?.Invoke(_currentHp, _maxHp);
        OnExpChanged?.Invoke(_currentExp);
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

    /// <summary>
    /// 추가 체력을 적용하는 함수
    /// </summary>
    /// <param name="bounusHp"></param>
    public void SetBonusMaxHp(float bounusHp)
    {
        float ratio = _currentHp / _maxHp;
        _maxHp = _data.MaxHp + bounusHp;
        _currentHp = _maxHp * ratio;

        OnHpChanged?.Invoke(_currentHp, _maxHp);
    }

    /// <summary>
    /// 추가 속력 증가율을 적용하는 함수
    /// </summary>
    /// <param name="speedRatio"></param>
    public void SetBonusSpeedRation(float speedRatio)
    {
        _speed = (1 + speedRatio) * _data.Speed;
        OnSpeedChanged?.Invoke(_speed);
    }

    // 임시
    /// <summary>
    /// 레벨업시 필요 경험치량을 늘리는 함수
    /// </summary>
    /// <param name="amount"></param>
    public void SetMaxExp(float amount)
    {
        _maxExp += amount;
        OnExpChanged?.Invoke(MaxExp);
    }

    /// <summary>
    /// 경험치 획득 함수
    /// </summary>
    /// <param name="amount"></param>
    public void AddCurrentExp(float amount)     //매개변수로 float amount 받아야하는데 일단 임시로
    {
        _currentExp += amount;
        if (CurrentExp >= _maxExp)
        {
            LevelUp();
        }
        OnExpChanged?.Invoke(_currentExp);
    }

    /// <summary>
    /// 현재경험치량이 최대 경험치량보다 크거나 같을 때 레벨업을 하는 함수
    /// </summary>
    public void LevelUp()
    {
        // 현재 경험치량이 최대 경험치량보다 작으면 레벨업하지 않음
        if (_currentExp < _maxExp) return;

        // 레벨이 최대 레벨보다 크거나 같으면 레벨업하지 않음
        if (_level >= _maxLevel)
        {
            _currentExp = _maxExp; // 최대 레벨에 도달하면 경험치 초기화
            return;
        }

        _currentExp -= _maxExp;
        // 경험치통 늘리는 함수
        // SetMaxExp(_maxExp * 0.1f); // 레벨업시 최대 경험치량을 10% 증가
        _level++;

        OnExpChanged?.Invoke(_currentExp);
        OnLevelUp?.Invoke();
    }
}
