using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ΰ� �⺻ �ɷ�ġ�� �����ϴ� ���� ������ Ŭ����
/// </summary>
[CreateAssetMenu(fileName = "HeroStatData", menuName = "GameSettings/HeroStatData")]
public class HeroStatData : ScriptableObject
{
    [SerializeField] float _maxHp;      // �⺻ �ִ� ü��
    [SerializeField] float _speed;      // �⺻ �̵� �ӷ�
    [SerializeField] float _baseExp;    // �⺻ ����ġ
    [SerializeField] float _expIncrementRate; // ����ġ ���

    [Header("----- �ر� ���� -----")]
    [SerializeField] int _unlockCost;

    public int UnlockCost => _unlockCost;

    public float MaxHp => _maxHp;
    public float Speed => _speed;

    /// <summary>
    /// ������ ���� �ʿ� ����ġ�� ��ȯ�� �ִ� �Լ�
    /// </summary>
    /// <param name="level">����</param>
    /// <returns></returns>
    public float GetExp(int level)
    {
        if (level <= 0)
            return _baseExp;

        return _baseExp * Mathf.Pow(_expIncrementRate, level - 1);
    }
}
