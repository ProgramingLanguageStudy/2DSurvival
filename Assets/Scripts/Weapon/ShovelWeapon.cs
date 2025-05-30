using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 총알을 원형으로 배치하고 스스로 회전하여 배치된 총알들로 적을 공격하는 무기
/// </summary>
public class ShovelWeapon : Weapon
{
    // 사정거리(총알 위치 반지름)
    [SerializeField] float _shootingRange;

    // 총알 속력(총알이 회전하는 속력)
    [SerializeField] float _bulletSpeed;

    // 총알 수(총알이 동시에 몇 개 배치되어 있을지)
    [SerializeField] int _bulletCount;

    [Header("----- 총알 프리펩 -----")]
    [SerializeField] Bullet _bulletPrefab;

    // 생성된 총알 리스트
    List<Bullet> _bullets = new List<Bullet>();

    // 임시로 Start()에서 초기화
    private void Start()
    {
        Initialize();
    }

    public override void Initialize()
    {
        CalculateStats();
        SpawnBullets();
    }

    /// <summary>
    /// 레벨에 따른 무기의 현재 스텟(런타임 데이터)을 계산하는 함수
    /// </summary>
    protected override void CalculateStats()
    {
        base.CalculateStats();  // _damage = _data.GetStat(WeaponStatType.Damage, _level);
        _bulletSpeed = _data.GetStat(WeaponStatType.BulletSpeed, _level);
        _shootingRange = _data.GetStat(WeaponStatType.ShootingRange, _level);
        float bulletCount = (int)_data.GetStat(WeaponStatType.BulletCount, _level);

        // float 값을 반올림해서 int로 전환
        _bulletCount = Mathf.RoundToInt(bulletCount);
    }

    private void FixedUpdate()
    {
        HandleRotation();
    }

    /// <summary>
    /// FixedUpdate()마다 호출
    /// 총알이 캐릭터 주위를 일정속력으로 돌기때문에(행성의 공전같은 느낌) 
    /// 무기의 총알이 계속 존재해야 하므로 필요한 함수이다.
    /// </summary>
    void HandleRotation()
    {
        transform.Rotate(0, 0, _bulletSpeed*Time.fixedDeltaTime);
    }

    /// <summary>
    /// 원형으로 총알들을 배치하는 함수.
    /// 각도 기준으로 일정간격 총알을 배치하는 역할.
    /// </summary>
    void SpawnBullets()
    {
        // 기존에 있던 총알들 제거
        RemoveBullets();

        // 총알들 사이 간격 각도 계산
        float angle = 360.0f / _bulletCount;

        // bulletCount만큼 총알 생성
        for (int i = 0; i < _bulletCount; i++)
        {
            // ShovelWeapon 컴포넌트가 붙어 있는 게임오브젝트의
            // 자식 게임오브젝트로 _bulletPrefab의 복제본을
            // 씬에 생성하고 bullet 변수로 그 복제본 게임오브젝트의
            // Bulelt 컴포넌트를 가리킨다.
            Bullet bullet = Instantiate(_bulletPrefab, transform);

            // 각 Bullet 게임오브젝트가 배치될 방향
            // Mathf.Cos(): 코사인(각도) -> x좌표
            // Mathf.Sin(): 사인(각도) -> y좌표
            // 각도 단위: degree(0도 ~ 360도) 라디안(2 * Pi = 360도)
            Vector3 dir = new Vector3(Mathf.Cos(i * angle * Mathf.Deg2Rad), Mathf.Sin(i * angle * Mathf.Deg2Rad), 0);

            // localPosition: 부모 게임오브젝트에 대한 상대 위치
            // 인스펙터뷰에 표시되는 Position은 사실 localPosition
            bullet.transform.localPosition = dir * _shootingRange;

            // 생성된 bullet 게임오브젝트의 위쪽 방향을
            // dir 방향으로 설정
            bullet.transform.up = dir;

            // 생성한 bullet의 데미지를 설정
            bullet.SetDamage(_damage);

            // 생성된 bullet 게임오브젝트를 리스트에 저장
            _bullets.Add(bullet);
        }
    }

    /// <summary>
    /// 기존에 생성되어 있던 총알들을 제거하는 함수
    /// </summary>
    void RemoveBullets()
    {
        foreach(Bullet bullet in _bullets)
        {
            // 총알 게임오브젝트를 파괴
            Destroy(bullet.gameObject);
        }

        // 리스트 비우기
        _bullets.Clear();
    }
}
