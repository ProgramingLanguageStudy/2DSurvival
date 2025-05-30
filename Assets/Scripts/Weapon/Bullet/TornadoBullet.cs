using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoBullet : ProjectileBullet, IRotatable, IGrowable
{
    [Header("----- 회전 스텟 -----")]
    // 일단은 기본적으로 한 방향으로 회전한다고 생각해서 회전방향을 따로 설정하지 않음
    [SerializeField] float _rotationSpeed;  // 회전 속력

    [Header("----- 거대화 스텟 -----")]
    // Lerp를 이용해 거대화하는데 필요한 변수들
    [SerializeField] float _growTimer;          // 최대 크기까지 도달하기 위한 타이머
    [SerializeField] float _growDuration;   // 최대 크기까지 도달하는 시간(초 단위)
    [SerializeField] float _maxSize;        // 최대 크기 배수(최대 몇배로 커질지)

    public void Update()
    {
        Rotate();
        Grow();
    }

    public void Rotate()
    {

        transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime, Space.World);
    }

    public void Grow()
    {
        // 타이머가 최대 크기까지 도달하는 시간보다 작으면
        if (_growTimer < _growDuration)
        {
            // 타이머를 증가시킨다.
            _growTimer += Time.deltaTime;
            // 현재 크기를 Lerp를 이용해 계산한다.
            float currentSize = Mathf.Lerp(1f, _maxSize, _growTimer / _growDuration);
            // 현재 크기에 맞춰서 스케일을 조정한다.
            transform.localScale = new Vector3(currentSize, currentSize, currentSize);
        }
    }

    // 사실 생략해도 되는데 Attack()함수를 굳이써주고 싶었음
    protected override void Attack(Enemy enemy)
    {
        base.Attack(enemy);
    }
}
