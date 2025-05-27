using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 총알을 배치하고 스스로 회전하여 배치된 총알들로 적을 공격하는 무기
/// </summary>
public class ShovelWeapon : MonoBehaviour
{
    [Header("----- 스탯 데이터 -----")]
    // 무기 레벨
    [SerializeField] int _level;

    // 데미지
    [SerializeField] float _damage;

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

    /// <summary>
    /// 원형으로 총알들을 배치하는 함수
    /// </summary>
    void SpawnBullets()
    {
        // 총알들 사이 간격 각도 계산
        float angle = 360.0f / _bulletCount;

        // bulletCount만큼 총알 생성
        for (int i = 0; i < _bulletCount; i++)
        {

        }
    }
}
