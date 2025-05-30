using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 일정 방향을 향해 커지며 회전하면서 날아가는 낫 형태의 총알 클래스
/// 적에게 닿으면 주기적으로 데미지를 입힌다.
/// </summary>
public class ScytheBullet : ProjectileBullet
{
    [SerializeField] float _maxScale;       // 최대 크기
    [SerializeField] float _rotSpeed;       // 회전 속력
    [SerializeField] float _damageDelay;    // 데미지 시간 간격

    Dictionary<Collider2D, Enemy> _touchingEnemyMap = new();

    protected

    private void Start()
    {
        StartCoroutine(AttackRoutine());
    }

    /// <summary>
    /// 총알의 최대 크기 설정
    /// </summary>
    /// <param name="maxScale"></param>
    public void SetMaxScale(float maxScale)
    {
        _maxScale = maxScale;
    }

    /// <summary>
    /// 총알의 회전 속력 설정
    /// </summary>
    /// <param name="rotSpeed"></param>
    public void SetRotSpeed(float rotSpeed)
    {
        _rotSpeed = rotSpeed;
    }

    /// <summary>
    /// 적에게 주는 데미지 간격 시간 설정
    /// </summary>
    /// <param name="damageDelay"></param>
    public void SetDamageDelay(float damageDelay)
    {
        _damageDelay = damageDelay;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        // 사라지기 전까지 크기가 최대 크기까지 점차 커진다.
        // Mathf.Lerp(a, b, t): a 값에서 시작해서 b 값으로 갈 때 t만큼 비율로 간 값을 반환한다.
        // a와 b 사이를 t로 선형 보간한다.
        float scale = Mathf.Lerp(1.0f,_maxScale, _timer / _duration);
        transform.localScale = Vector3.one * scale;

        // 회전
        transform.Rotate(0, 0, _rotSpeed * Time.fixedDeltaTime);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (_targetLayerMask.Contains(collision.gameObject.layer))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                Attack(enemy);

                // 충돌한 Enemy 컴포넌트를 딕셔너리에 저장
                // [collision] ----- [enemy] 딕셔너리임
                _touchingEnemyMap[collision] = enemy;
            }
        }
    }

    /// <summary>
    /// 적과 충돌이 끝났을 때 딕셔너리에서 제거
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_targetLayerMask.Contains(collision.gameObject.layer))
        {
            // 콜라이더로 딕셔너리에서 Enemy 컴포넌트를 찾아서 제거
            _touchingEnemyMap.Remove(collision);
        }
    }

    /// <summary>
    /// 일정 시간 간격마다 접촉 중인 적들에게 데미지를 입히는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator AttackRoutine()
    {
        while (true)
        {
            List<Enemy> enemies = new List<Enemy>(_touchingEnemyMap.Values);
            foreach (var enemy in enemies)
            {
                // 접촉 중인 적에게 데미지를 입힌다.
                Attack(enemy);
            }
            yield return new WaitForSeconds(_damageDelay);
        }
    }
}
