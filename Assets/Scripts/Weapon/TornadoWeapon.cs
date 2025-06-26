using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 일정시간마다 TornadoBullet을 생성하여 발사한다.
/// </summary>
public class TornadoWeapon : FiringWeapon
{
    // 적 관통 횟수에 제한이 없는게 좋을듯
    //[SerializeField] int _attackCount;      // 총알 공격 횟수(적 관통 얼마나 할지)

    [Header(" ----- 총알 프리펩 ----- ")]
    [SerializeField] TornadoBullet _bulletPrefab; // 발사할 총알 프리팹

    public override WeaponType WeaponType => WeaponType.Tornado;

    // 임시
    // 부모에게 몇몇 스탯들은 이미 있고(base.)
    // 나머지 필요한 스탯들을 생각해서 계산해줘야한다.
    // 하지만 부모 스탯에서 추가될 것이 딱히 없다.(지금은)
    // 여기있는 스탯의 수만큼 ScriptableObject의 WeaponData에 정의되어 있어야 한다.
    protected override void CalculateStats()
    {
        base.CalculateStats();
        // 밑에 있는 6개가 ScriptableObject에서 있어야 할 데이터들임
        //base.CalculateStats(); (damage)
        //_bulletSpeed = _data.GetStat(WeaponStatType.BulletSpeed, _level);
        //_bulletCount = Mathf.RoundToInt(_data.GetStat(WeaponStatType.BulletCount, _level));
        //_coolTime = _data.GetStat(WeaponStatType.CoolTime, _level);
        //_fireDelay = _data.GetStat(WeaponStatType.FireDelay, _level);
        //_bulletDuration = _data.GetStat(WeaponStatType.BulletDuration, _level);
    }

    protected override void SpawnBullet()
    {
        // 일단 총알을 생성하고
        TornadoBullet bullet = Instantiate(_bulletPrefab);

        // bullet의 위치를 이 게임오브젝트 위치로 설정
        bullet.transform.position = transform.position;

        // 총알이 생성될 때 총알의 스텟들을 설정
        bullet.SetDamage(_damage);
        bullet.SetDirection(GetBulletDirection());
        bullet.SetDuration(_bulletDuration);
        bullet.SetSpeed(_bulletSpeed);
    }
}
