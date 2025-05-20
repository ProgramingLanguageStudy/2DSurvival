using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 주인공 캐릭터의 데이터 로직을 담당하는 역할
/// </summary>
public class HeroModel : MonoBehaviour
{
    // 최대 체력
    [SerializeField] float _maxHp;

    // 임시
    // 현재 체력
    [SerializeField] float _currentHp;

    // 체력 변경 이벤트
    public event UnityAction<float, float> OnHpChanged;
    // 사망 이벤트
    public event UnityAction OnDeath;

    public float MaxHp => _maxHp;
    public float CurrentHp => _currentHp;

    public void Initialize()
    {
        _currentHp = _maxHp;
    }

    public void TakeDamage(float amount)
    {
        if (_currentHp <= 0) return;

        //_currentHp = -amount;

        //if(_currentHp > _maxHp)
        //{
        //    _currentHp = _maxHp;
        //}
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
