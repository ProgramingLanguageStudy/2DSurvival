using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 일정 주기로 낫 총알을 발사하는 무기 클래스
/// </summary>
public class ScytheWeapon : FiringWeapon
{
    [SerializeField] float _damageDelay;        // 총알이 데미지를 입히는 시간 간격
    [SerializeField] float _bulletRange;        // 총알의 범위
    [SerializeField] float _rotSpeed;           // 총알 회전 속력

    [Header("----- 총알 프리펩 -----")]
    [SerializeField] ScytheBullet _bulletPrefab;

    protected override void CalculateStats()
    {
        base.CalculateStats();
        _damageDelay = _data.GetStat(WeaponStatType.DamageDelay, _level);
        _bulletRange = _data.GetStat(WeaponStatType.BulletRange, _level);
        _rotSpeed = _data.GetStat(WeaponStatType.RotSpeed, _level);
    }

    protected override void SpawnBullet()
    {
        ScytheBullet bullet = Instantiate(_bulletPrefab);
        bullet.transform.position = transform.position;
        bullet.SetDamage(_damage);
        bullet.SetDuration(_bulletDuration);
        bullet.SetSpeed(_bulletSpeed);
        bullet.SetDirection(GetBulletDirection());
        bullet.SetMaxScale(_bulletRange);
        bullet.SetRotSpeed(_rotSpeed);
        bullet.SetDamageDelay(_damageDelay);

    }
}
