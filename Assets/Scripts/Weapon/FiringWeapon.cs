using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� �ð����� �Ѿ��� �߻��ϴ� ���� �߻� Ŭ����
/// �ܵ����� FiringWeapon Ŭ�����δ� ��ü�� �������� �ʰڴ�.
/// </summary>
public abstract class FiringWeapon : Weapon
{
    [SerializeField] protected float _bulletSpeed;  // �Ѿ� �ӷ�
    [SerializeField] protected int _bulletCount;    // �� ���� �߻��ϴ� �Ѿ� ��
    [SerializeField] protected float _coolTime;     // �Ѿ� �߻� ����(��Ÿ��)
    [SerializeField] protected float _fireDelay;    // ���� �� �Ѿ� ���� ���� �ð�
    [SerializeField] protected float _bulletDuration; // �Ѿ� ���� �ð�

    // �Ѿ��� �ֱ������� �߻��ϱ� ���� �ڷ�ƾ ����
    Coroutine _attackRoutine;

    private void Start()
    {
        Initialize();
    }

    /// <summary>
    /// ���� �ʱ�ȭ �� ���� ��ƾ ����
    /// </summary>
    public override void Initialize()
    {
        // ���� �ʱ�ȭ �ѹ� ���ֱ�
        CalculateStats();

        // ���� ��ƾ ����
        _attackRoutine = StartCoroutine(AttackRoutine());
    }

    protected override void CalculateStats()
    {
        base.CalculateStats();
        _bulletSpeed=_data.GetStat(WeaponStatType.BulletSpeed, _level);
        _bulletCount = Mathf.RoundToInt(_data.GetStat(WeaponStatType.BulletCount, _level));
        _coolTime = _data.GetStat(WeaponStatType.CoolTime, _level);
        _fireDelay = _data.GetStat(WeaponStatType.FireDelay, _level);
        _bulletDuration = _data.GetStat(WeaponStatType.BulletDuration, _level);
    }

    /// <summary>
    /// ���� �ֱ�� �߻� ��ƾ�� ȣ���ϴ� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator AttackRoutine()
    {
        while (true)
        {
            // ��Ÿ�� ���
            yield return new WaitForSeconds(_coolTime);

            // �߻� ��ƾ ����
            StartCoroutine(FireRoutine());
        }
    }

    /// <summary>
    /// ������ ����ŭ �Ѿ��� ���߷� �߻��ϴ� �ڷ�ƾ
    /// �Ѿ��� ���ư��ٴ� ���� Bullet���� �̹� �����Ǿ�����
    /// </summary>
    /// <returns></returns>
    IEnumerator FireRoutine()
    {
        for (int i = 0; i < _bulletCount; i++)
        {
            // �Ѿ� �ϳ� ����
            SpawnBullet();

            // ���� ���� �ð� ���ݸ�ŭ ���
            yield return new WaitForSeconds(_fireDelay);
        }
    }

    /// <summary>
    /// �Ѿ��� �� �� or �� �� �����ϴ� �Լ�
    /// </summary>
    protected abstract void SpawnBullet();

    /// <summary>
    /// �Ѿ��� ���ư� ������ ����ϴ� �Լ�
    /// </summary>
    /// <returns></returns>
    protected virtual Vector3 GetBulletDirection()
    {
        // 2�������� ������������ �߻�
        return Random.insideUnitCircle.normalized;
    }
}
