using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ݷ��� ������ �����ϴ� ������� �������̽�
/// </summary>
public interface IAttackable
{
    float Damage { get; }
    /// <summary>
    /// ������� ���ݷ��� ������ �� ����ϴ� �Լ�
    /// </summary>
    /// <param name="additionalDamage">��ȭ�� ����������(ex:20% ������ 0.2f, ���Ҹ� -0.2f)</param>
    public void SetDamage(float additionalDamage);
}
