using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� �ð����� ����� �� ĳ���͸� ���� GunBullet�� �߻��ϴ� ���� Ŭ����
/// </summary>
public class GunWeapon : FiringWeapon
{
    [SerializeField] float _shootingRange;  // ���� �Ÿ�(�� Ž�� ����)
    [SerializeField] int _attackCount;      // �Ѿ� ���� Ƚ��(�� ���� �󸶳� ����)

    [Header(" ----- �Ѿ� ������ ----- ")]
    [SerializeField] GunBullet _bulletPrefab; // �߻��� �Ѿ� ������

    [Header(" ----- Ÿ�� ���� ----- ")]
    [SerializeField] LayerMask _targetLayerMask; // ������ Ÿ�� ����

    /// <summary>
    /// ������ Ÿ�� ������ ������ �迭
    /// </summary>
    Collider2D[] _colliders = new Collider2D[10];

    protected override void CalculateStats()
    {
        // �ϴ� �θ� Ŭ������ ���ݵ��� ����ϰ�
        base.CalculateStats();

        // GunWeapon�� �߰��� �ʿ��� ���ݵ��� ���
        _shootingRange = _data.GetStat(WeaponStatType.ShootingRange, _level);
        _attackCount = Mathf.RoundToInt(_data.GetStat(WeaponStatType.AttackCount, _level));
    }

    // �θ� Ŭ������ SpawnBullet() �Լ��� abstract�� ����Ǿ� �����Ƿ�
    // GunWeapon Ŭ�������� �ݵ�� �����ؾ� �Ѵ�.
    protected override void SpawnBullet()
    {
        // �ϴ� �Ѿ��� �����ϰ�
        GunBullet bullet = Instantiate(_bulletPrefab);

        // bullet�� ��ġ�� �� ���ӿ�����Ʈ ��ġ�� ����
        bullet.transform.position = transform.position;

        // �Ѿ��� ������ �� �Ѿ��� ���ݵ��� ����
        bullet.SetCount(_attackCount);
        bullet.SetDamage(_damage);
        bullet.SetDirection(GetBulletDirection());
        bullet.SetDuration(_bulletDuration);
        bullet.SetSpeed(_bulletSpeed);
    }

    /// <summary>
    /// ���� ����� �� �������� �Ѿ� ������ ���
    /// </summary>
    /// <returns></returns>
    protected override Vector3 GetBulletDirection()
    {
        // ������ ���� ����� �༮�� target�� ����
        Transform target = GetNearestTarget();

        // ���� ������ Ÿ���� null, �� ���ٸ�
        if (target == null)
        {
            // Ÿ���� ���ٸ� ���� �������� �߻�
            return base.GetBulletDirection();
        }
        else
        {
            // Ÿ���� �ִٸ� Ÿ�� �������� �Ѿ��� �߻�
            return (target.position - transform.position).normalized;
        }
    }

    /// <summary>
    /// �����Ÿ� ������ ���� ����� �� Ÿ���� ã�� �Լ�
    /// </summary>
    /// <returns>���� ����� Ÿ���� Transform. ������ null</returns>
    Transform GetNearestTarget()
    {
        // OverlapCircleNonAlloc(Vector2 center, float radius, Collider2D[] results, int layerMask)
        // � ����(transform.position)�� ��������
        // �����Ÿ�(_shootingRange) ���� �ִ�
        // (_targetLayerMask)�� �ش��ϴ� Ÿ�ٵ��� �����Ѵ�.
        // ������ Ÿ�ٵ��� (_colliders) �迭�� ����ȴ�.
        if (Physics2D.OverlapCircleNonAlloc(transform.position, _shootingRange, _colliders, _targetLayerMask.value) > 0)
        {
            // 0�� Ÿ���� ������ Ÿ�� �� ���� ����� Ÿ���̶�� ����
            Transform target = _colliders[0].transform;

            // ���� ����� �Ÿ� ���
            float minDistance = Vector3.Distance(transform.position, target.position);

            foreach(var collider in _colliders)
            {
                // �ϴ� collider �տ������� Ȯ���ϸ鼭 �� ������ �ִ³�鸸 ������ continue
                if (collider == null) continue; // null üũ

                // ���� Ÿ�ٰ��� �Ÿ� ���
                float distance = Vector3.Distance(transform.position, collider.transform.position);

                // ���� Ÿ���� �� ������ ����
                if (distance < minDistance)
                {
                    minDistance = distance;
                    target = collider.transform;
                }
            }
            // ���� ����� Ÿ�� ��ȯ
            return target;
        }

        // ���� �����Ÿ� ���� Ÿ���� ���ٸ� null ��ȯ
        else
        {
            return null;
        }
    }
}
