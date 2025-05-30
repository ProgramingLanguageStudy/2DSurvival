using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ĳ���� ������ �������� 3f���� ���������� ū ����� �߻�ǰ� 2������ �����ϸ鼭 ������ �����ϴٰ� ������� ����
/// </summary>
public class MeteoWeapon : FiringWeapon
{
    // �� ���� Ƚ���� ������ ���°� ������
    //[SerializeField] int _attackCount;      // �Ѿ� ���� Ƚ��(�� ���� �󸶳� ����)

    [Header(" ----- �Ѿ� ������ ----- ")]
    [SerializeField] MeteoBullet _bulletPrefab; // �߻��� �Ѿ� ������

    // �ӽ�
    // �θ𿡰� ��� ���ȵ��� �̹� �ְ�(base.)
    // ������ �ʿ��� ���ȵ��� �����ؼ� ���������Ѵ�.
    // ������ �θ� ���ȿ��� �߰��� ���� ���� ����.(������)
    // �����ִ� ������ ����ŭ ScriptableObject�� WeaponData�� ���ǵǾ� �־�� �Ѵ�.
    protected override void CalculateStats()
    {
        base.CalculateStats();
        // �ؿ� �ִ� 6���� ScriptableObject���� �־�� �� �����͵���
        //base.CalculateStats(); (damage)
        //_bulletSpeed = _data.GetStat(WeaponStatType.BulletSpeed, _level);
        //_bulletCount = Mathf.RoundToInt(_data.GetStat(WeaponStatType.BulletCount, _level));
        //_coolTime = _data.GetStat(WeaponStatType.CoolTime, _level);
        //_fireDelay = _data.GetStat(WeaponStatType.FireDelay, _level);
        //_bulletDuration = _data.GetStat(WeaponStatType.BulletDuration, _level);
    }

    protected override void SpawnBullet()
    {
        // �ϴ� �Ѿ��� �����ϰ�
        MeteoBullet bullet = Instantiate(_bulletPrefab);

        // bullet�� ���� ��ġ�� �� ���ӿ�����Ʈ ��ġ�� ����
        bullet.transform.position = transform.position;

        // �Ѿ��� ������ �� �Ѿ��� ���ݵ��� ����
        bullet.SetDamage(_damage);
        bullet.SetDirection(GetBulletDirection());
        bullet.SetDuration(_bulletDuration);
        bullet.SetSpeed(_bulletSpeed);
    }
}
