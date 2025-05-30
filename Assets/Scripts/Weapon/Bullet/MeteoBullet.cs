using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/*
처음에는 단순히 시간만 조절하고 크기 조절하면 끝이라 생각했는데
적을 지속적으로 공격하는 기능추가가 필요해짐
*/

/// <summary>
/// 일정시간 날아가다가 멈추고 그 자리에서 커지면서 적을 공격하는 메테오 총알 클래스
/// 지속시간은 원래 지속시간인 _duration에서 _meteoStopDuration을 뺀 값이 된다.
/// 즉 ScriptableObject에서 설정한 지속시간에서 날아가는 시간을 뺀 값이 총알의 지속시간이 된다.
/// </summary>
public class MeteoBullet : ProjectileBullet, IGrowable
{
    // 원래는 이런식으로 하려했는데 그냥 Update에서 시간을 체크해서
    // 일정시간 뒤에 속도를 0으로 바꾸는 방식으로 하기로 함
    // 그러기 위해서는 몇초 날아갈다 멈출지 시간만 하나 있으면됨
    //[Header("----- 이동 후 멈추기위해 필요한 스텟 -----")]
    //[SerializeField] float _meteoSpeed; // 메테오가 날아가는 속력
    //[SerializeField] float _meteoMoveDuration;   // 메테오가 날아가는 시간(초 단위)
    [SerializeField] float _meteoStopDuration;   // 메테오가 멈추는 시간(초 단위)

    [Header("----- 거대화 스텟 -----")]
    // Lerp를 이용해 거대화하는데 필요한 변수들
    bool isGrowing = false; // 거대화 시작할까?(메테오가 멈춘 후에 거대화 시작)
    [SerializeField] float _growTimer;          // 최대 크기까지 도달하기 위한 타이머
    [SerializeField] float _growDuration;   // 최대 크기까지 도달하는 시간(초 단위)
    [SerializeField] float _maxSize;        // 최대 크기 배수(최대 몇배로 커질지)

    [Header("----- 장판 스텟 -----")]
    [SerializeField] float _attackSpan; // 장판 공격 주기

    [Header("----- 장판이 감지한 적을 담아둘 리스트 -----")]
    List<Enemy> _detectedEnemies = new List<Enemy>(); // 장판이 감지한 적들을 담아둘 리스트

    // 단순히 코루틴을 시작해버리면 적이 들어올때마다 실행되서
    // 장판이 마치 여러개 있는 것처럼 작동한다.
    // 그래서 중복실행을 방지하기 위한 장치가 필요하다
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    // 충돌한 상대 게임오브젝트의 레이어가
    //    // _targetLayerMask에 포함되면
    //    if (_targetLayerMask.Contains(collision.gameObject.layer))
    //    {
    //        // 충돌한 상대 게임오브젝트의 Enemy 컴포넌트를 가져온다.
    //        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
    //        // Enemy 컴포넌트가 실제로 있으면
    //        if (enemy != null)
    //        {
    //            // 적을 리스트에 추가한다.
    //            _detectedEnemies.Add(enemy);
    //        }
    //    }
    //    StartCoroutine(AttackCoroutine()); // 적을 감지하면 공격 코루틴 시작
    //}

    // 코루틴 중복 실행 방지용 플래그
    private bool _isAttackCoroutineRunning = false;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 상대 게임오브젝트의 레이어가
        // _targetLayerMask에 포함되면
        if (_targetLayerMask.Contains(collision.gameObject.layer))
        {
            // 충돌한 상대 게임오브젝트의 Enemy 컴포넌트를 가져온다.
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            // Enemy 컴포넌트가 실제로 있고
            // 리스트에 있는놈이 아니라면(버그나 물리엔진 이상으로 적 한놈이 두번 트리거하는거 방지)
            if (enemy != null && !_detectedEnemies.Contains(enemy))
            {
                // 적을 리스트에 추가한다.
                _detectedEnemies.Add(enemy);
                if(_isAttackCoroutineRunning == false)
                {
                    // 공격 코루틴이 실행 중이지 않으면 시작
                    _isAttackCoroutineRunning = true;
                    StartCoroutine(AttackCoroutine());
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 충돌한 상대 게임오브젝트의 레이어가
        // _targetLayerMask에 포함되면
        if (_targetLayerMask.Contains(collision.gameObject.layer))
        {
            // 충돌한 상대 게임오브젝트의 Enemy 컴포넌트를 가져온다.
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            // Enemy 컴포넌트가 실제로 있으면
            if (enemy != null)
            {
                // 적을 리스트에서 제거한다.
                _detectedEnemies.Remove(enemy);
            }
        }
    }

    public void Update()
    {
        // 메테오가 날아가는 시간동안은 날아가게 하고
        // 그 이후에는 멈추게 한다.
        if (_meteoStopDuration > 0f)
        {
            _meteoStopDuration -= Time.deltaTime;
            if (_meteoStopDuration <= 0f)
            {
                // 날아가는 시간이 다 되면 속도를 0으로 바꿔서 멈추게 한다.
                SetSpeed(0f);
                // 거대화 오케이 이제 시작해
                isGrowing = true;
            }
        }
        // 거대화 허락을 맡으면
        if( isGrowing)
        {
            // 거대화 시작
            Grow();
        }
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

    IEnumerator AttackCoroutine()
    {
        //yield return AttackCoroutine();   // 코루틴이 끝날 때까지 기다린다.

        while (true)
        {
            if(_detectedEnemies.Count == 0)
            {
                // 공격할 적이 없다면 코루틴을 종료한다.
                _isAttackCoroutineRunning = false;
                yield break;
            }
            // 공격할 적이 있다면 공격한다.
            if (_detectedEnemies != null)
            {
                for(int i = 0; i < _detectedEnemies.Count; i++)
                {
                    // 공격할 적이 있다면 공격한다.
                    Attack(_detectedEnemies[i]);
                }
            }
            // 공격 후에는 대기 시간을 기다린다.
            yield return new WaitForSeconds(_attackSpan);
        }
    }

    // 사실 생략해도 되는데 Attack()함수를 굳이써주고 싶었음
    protected override void Attack(Enemy enemy)
    {
        base.Attack(enemy);
    }
}
