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
    [SerializeField] float _maxExp;     // ����ġ ��
    [SerializeField] float _currentHp;  // ���� ü��
    [SerializeField] float _currentExp; // ���� ����ġ��

    [Header("----- ���� -----")]
    [SerializeField] float _level = 1;        // ���� ����
    [SerializeField] float _maxLevel = 100;   // �ִ� ����

    // ü�� ���� �̺�Ʈ
    public event UnityAction<float, float> OnHpChanged;
    // �̵� �ӷ� ���� �̺�Ʈ
    public event UnityAction<float> OnSpeedChanged;
    // ����ġ ���� �̺�Ʈ
    public event UnityAction<float> OnExpChanged;
    // ������ �̺�Ʈ
    public event UnityAction OnLevelUp;
    // ��� �̺�Ʈ
    public event UnityAction OnDeath;

    // ������Ƽ (�б� ����)
    public float MaxHp => _maxHp;
    public float CurrentHp => _currentHp;
    public float Speed => _speed;
    public float MaxExp => _maxExp;
    public float CurrentExp => _currentExp;

    // �ʱ�ȭ
    public void Initialize()
    {
        _maxHp = _data.MaxHp;
        _speed = _data.Speed;
        _maxExp = _data.MaxExp;

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

    // �ӽ�
    /// <summary>
    /// �������� �ʿ� ����ġ���� �ø��� �Լ�
    /// </summary>
    /// <param name="amount"></param>
    public void SetMaxExp(float amount)
    {
        _maxExp += amount;
        OnExpChanged?.Invoke(MaxExp);
    }

    /// <summary>
    /// ����ġ ȹ�� �Լ�
    /// </summary>
    /// <param name="amount"></param>
    public void AddCurrentExp(float amount)     //�Ű������� float amount �޾ƾ��ϴµ� �ϴ� �ӽ÷�
    {
        _currentExp += amount;
        if (CurrentExp >= _maxExp)
        {
            LevelUp();
        }
        OnExpChanged?.Invoke(_currentExp);
    }

    /// <summary>
    /// �������ġ���� �ִ� ����ġ������ ũ�ų� ���� �� �������� �ϴ� �Լ�
    /// </summary>
    public void LevelUp()
    {
        // ���� ����ġ���� �ִ� ����ġ������ ������ ���������� ����
        if (_currentExp < _maxExp) return;

        // ������ �ִ� �������� ũ�ų� ������ ���������� ����
        if (_level >= _maxLevel)
        {
            _currentExp = _maxExp; // �ִ� ������ �����ϸ� ����ġ �ʱ�ȭ
            return;
        }

        _currentExp -= _maxExp;
        // ����ġ�� �ø��� �Լ�
        // SetMaxExp(_maxExp * 0.1f); // �������� �ִ� ����ġ���� 10% ����
        _level++;

        OnExpChanged?.Invoke(_currentExp);
        OnLevelUp?.Invoke();
    }
}
