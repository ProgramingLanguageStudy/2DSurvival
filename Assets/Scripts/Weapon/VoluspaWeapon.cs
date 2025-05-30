using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 생성할 위치가 달라지지만 어쨌든 FiringWeapon을 상속받는다.
// 적을 어떻게 감지할지 잘 생각해야함
// 메커니즘이 상당히 중요한대 먼저 쿨타임이 찼을때 대기하고
// 그 상태에서 적이 감지되었으면 발사
// 감지가 안되었으면 쿨타임 다시 돌지말고 기다려야함

// Update문에서 쿨타임이 되면 적을 감지함
// 감지된 적이 있으면 총알을 발사하고
// 감지된 적이 없으면 쿨타임을 다시 돌지 않고 대기한다.
// 감지는 뷰포트좌표를 활용해서
// 화면내에 보이는 적을 감지한다. -> Physics2D.OverlapAreaNonAlloc()를 사용
// 가 원래 목표지만 Camaera.ViewportToWorldPoint()를 사용해서 하는 방법은 나중에 생각해보기로 하고
// 지금당장은 transform.position을 기준으로
// 화면의 가로길이 세로길이를 구해서 감지할 예정이다.
// 생성위치는 x와 y가 다른대
// x는 뷰포트좌표를 활용해서 화면 끝에서 살짝 벗어나는 범위에서 생성하고
// y는 감지된 적들 중에서 random으로 count만큼 뽑아 당첨된 놈들의 y 값을 사용한다.

/// <summary>
/// 볼루스파 무기를 구현할 클래스
/// </summary>
public class VoluspaWeapon : FiringWeapon
{
    [SerializeField] float _senseDistanceX = 10f; // 감지할 x축 거리(일단 화면 기준으로 Hero부터 화면끝까지의 거리로 잡음)
    [SerializeField] float _senseDistanceY = 5f; // 감지할 y축 거리(일단 화면 기준으로 Hero부터 화면끝까지의 거리로 잡음)
    // 이거 여기서 이렇게하면 안됨
    //Vector2 _senseSize = new Vector2(_senseDistanceX, _senseDistanceY); // 감지할 영역의 크기(OverlapAreaNonAlloc()에서 사용)

    [Header(" ----- 총알 프리펩 ----- ")]
    [SerializeField] VoluspaBullet _bulletPrefab; // 발사할 총알 프리팹

    // 기존 FiringWeapon에서 날아가는 방향은 랜덤이었고
    // VoluspaWeapon에서는 타겟을 감지해서 날아가는 방향을 설정해주기 위해 필요함
    // 나중에 IDetectable 인터페이스를 사용하면 좋을듯?
    [Header(" ----- 타겟 감지 ----- ")]
    [SerializeField] LayerMask _targetLayerMask; // 감지할 타겟 설정

    /// <summary>
    /// 감지된 타겟 정보를 저장할 배열
    /// </summary>
    Collider2D[] _colliders = new Collider2D[10];
    
    List<Enemy> _detectedEnemies = new List<Enemy>();


    protected override void CalculateStats()
    {
        // 일단 부모 클래스의 스텟들은 계산하고
        base.CalculateStats();
    }

    // 부모 클래스의 SpawnBullet() 함수가 abstract로 선언되어 있으므로
    // GunWeapon 클래스에서 반드시 구현해야 한다.
    protected override void SpawnBullet()
    {
        // 일단 총알을 생성하고
        VoluspaBullet bullet = Instantiate(_bulletPrefab);

        // 여기가 중요한 부분인데 detectedEnemies 중 랜덤으로 뽑아서
        // 생성하려는 위치의 y값은 그 랜덤으로 뽑힌 randEnemies의 position.y로 설정하고
        // 생성하려는 위치의 x값은 Hero의 Local???포지션의 x값으로 설정한다.
        bullet.transform.position = transform.position;

        // 총알이 생성될 때 총알의 스텟들을 설정
        bullet.SetDamage(_damage);
        bullet.SetDirection(GetBulletDirection());
        bullet.SetDuration(_bulletDuration);
        bullet.SetSpeed(_bulletSpeed);
    }

    /// <summary>
    /// 가장 가까운 적 방향으로 총알 방향을 계산
    /// </summary>
    /// <returns></returns>
    protected override Vector3 GetBulletDirection()
    {
        // 일단 임시(컴파일러떄문에)
        return new Vector3(0,0,0);
    }

    /// <summary>
    /// 사정거리 내에서 가장 가까운 적 타겟을 찾는 함수
    /// </summary>
    /// <returns>가장 가까운 타겟의 Transform. 없으면 null</returns>
    Transform GetNearestTarget()
    {
        Vector2 _senseSize = new Vector2(_senseDistanceX, _senseDistanceY); // 감지할 영역의 크기
        if (Physics2D.OverlapBoxNonAlloc(transform.position, _senseSize, 0, _colliders, _targetLayerMask.value) > 0)
        {
            // 0번 타겟이 감지된 타겟 중 가장 가까운 타겟이라고 가정
            Transform target = _colliders[0].transform;
            // 가장 가까운 거리 계산
            float minDistance = Vector3.Distance(transform.position, target.position);
            foreach (var collider in _colliders)
            {
                // 일단 collider 앞에서부터 확인하면서 더 없으면 있는놈들만 데리고 continue
                if (collider == null) continue; // null 체크
                // 현재 타겟과의 거리 계산
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                // 현재 타겟이 더 가까우면 갱신
                if (distance < minDistance)
                {
                    minDistance = distance;
                    target = collider.transform;
                }
            }
            // 가장 가까운 타겟 반환
            return target;
        }

        // 만약 사정거리 내에 타겟이 없다면 null 반환
        else
        {
            return null;
        }
    }
}
