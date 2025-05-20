using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ���ΰ� ĳ������ ������ ������ ����ϴ� ����
/// </summary>
public class HeroModel : MonoBehaviour
{
    // �ִ� ü��
    [SerializeField] float _maxHp;

    // �ӽ�
    // ���� ü��
    [SerializeField] float _currentHp;

    // ü�� ���� �̺�Ʈ
    public event UnityAction<float, float> OnHpChanged;
    // ��� �̺�Ʈ
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

        // ü�� ���� �̺�Ʈ ����
        OnHpChanged?.Invoke(_currentHp, _maxHp);

        if (_currentHp <= 0)
        {
            // ��� �̺�Ʈ ����
            OnDeath?.Invoke();
        }
    }
}
