using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ���ΰ� ĳ������ ������ ������ ����ϴ� ����
/// </summary>
public class HeroModel : MonoBehaviour
{
    [Header("----- ���� ������ -----")]
    [SerializeField] HeroData _data;    // HeroData ����

    [Header("----- ����(�ν����� �信�� �������ظ���) -----")]
    [SerializeField] float _maxHp;      // �ִ� ü��
    [SerializeField] float _speed;      // �̵� �ӷ�
    [SerializeField] float _exp;        // ����ġ ��
    [SerializeField] float _currentHp;  // ���� ü��
    [SerializeField] float _currentExp; // ���� ����ġ��

    // ü�� ���� �̺�Ʈ
    public event UnityAction<float, float> OnHpChanged;
    // �̵� �ӷ� ���� �̺�Ʈ
    public event UnityAction<float> OnSpeedChanged;
    // ����ġ ���� �̺�Ʈ
    public event UnityAction<float> OnExpChanged;
    // ��� �̺�Ʈ
    public event UnityAction OnDeath;

    // ������Ƽ (�б� ����)
    public float MaxHp => _maxHp;
    public float CurrentHp => _currentHp;
    public float Speed => _speed;
    public float Exp => _exp;
    public float CurrentExp => _currentExp;

    // �ʱ�ȭ
    public void Initialize()
    {
        _maxHp = _data.MaxHp;
        _speed = _data.Speed;
        _exp = _data.Exp;

        _currentHp = _maxHp;
        _currentExp = 0;

        // �ʱ� ü�°� �̵� �ӷ� ���� �̺�Ʈ ����
        OnSpeedChanged?.Invoke(_speed);
        OnHpChanged?.Invoke(_currentHp, _maxHp);
        OnExpChanged?.Invoke(_currentExp);
    }

    public void TakeDamage(float amount)
    {
        if (_currentHp <= 0) return;

        _currentHp = Mathf.Min(_currentHp - amount, _maxHp);

        // ü�� ���� �̺�Ʈ ����
        OnHpChanged?.Invoke(_currentHp, _maxHp);

        if (_currentHp <= 0)
        {
            // ��� �̺�Ʈ ����
            OnDeath?.Invoke();
        }
    }

    /// <summary>
    /// �߰� ü���� �����ϴ� �Լ�
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
    /// �߰� �ӷ� �������� �����ϴ� �Լ�
    /// </summary>
    /// <param name="speedRatio"></param>
    public void SetBonusSpeedRation(float speedRatio)
    {
        _speed = (1 + speedRatio) * _data.Speed;
        OnSpeedChanged?.Invoke(_speed);
    }

    /// <summary>
    /// �������� �ʿ� ����ġ���� �ø��� �Լ�
    /// </summary>
    /// <param name="amount"></param>
    public void SetExp(float amount)
    {
        _exp += amount;
    }

    public void AddCurrentExp(float amount)
    {
        _currentExp += amount;
    }
}
