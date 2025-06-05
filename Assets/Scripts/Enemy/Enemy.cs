using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
AI 구현에서 상태 관리가 필요할 때
유한 상태 머신(Finite State Machine)
- 유한한 개수의 상태를 기반으로 동작을 제어하는 디자인 패턴

사앹 패턴(State Pattern)
1) 인터페이스나 추상 클래스로 동일한 방식으로 작동하는 상태 클래스의 틀 만들기
2) 각 상태의 구현은 해당 인터페이스나 추상 클래스를 상속받는 개별 상태 클래스에서 작업

*/

/// <summary>
/// 적 캐릭터 담당 역할
/// </summary>
public class Enemy : MonoBehaviour
{
    [Header("----- 컴포넌트 참조 -----")]
    [SerializeField] EnemyModel _model; // 적 런타임 데이터 모델
    [SerializeField] CharacterHud _hud; // HUD UI
    [SerializeField] Mover _mover;
    [SerializeField] Animator _animator;
    [SerializeField] SpriteRenderer _renderer;
    [SerializeField] Collider2D _collider;
    [SerializeField] Rigidbody2D _rigid;

    [Header("----- 공격 -----")]
    // 공격 타겟 레이어 마스크
    [SerializeField] LayerMask _targetLayerMask;
    // 공격 간격(초)
    [SerializeField] float _attackSpan;

    // 임시
    [Header("----- 임시 -----")]
    Transform _target;  // 추적할 대상의 Transform(Hero)
    float _repositionDistance = 5.0f; // 재배치 거리

    public event UnityAction<float> OnDeathEvent;   // 적 캐릭터 사망 이벤트

    // 현재 상태 객체를 가리키는 인터페이스 변수
    IEnemyState _currentState;

    // 공격 코루틴 참조 변수
    Coroutine _attackRoutine;

    

    /// <summary>
    /// 적 캐릭터를 초기화하는 함수
    /// </summary>
    /// <param name="target">추적 대상의 Transform</param>
    public void Initialize(Transform target, WaveData waveData)
    {
        _target = target;

        _model.Initialize(waveData);

        _mover.OnMoved += OnMoved;
        _model.OnDeath += OnDeath;
        _model.OnHpChanged += _hud.SetHpBar;

        ChangeState(EnemyState.Idle);
    }

    /// <summary>
    /// 상태를 변경하는 함수. 기존 상태가 사망 상태인 경우나
    /// 기존 상태와 동일한 상태로 변경하려는 경우에는 무시
    /// </summary>
    /// <param name="state"></param>
    void ChangeState(EnemyState state)
    {
        if (_currentState != null)
        {
            // 현재 상태가 죽은 상태이면 다른 상태로 전환하지 않는다.
            if (_currentState.State == EnemyState.Death) return;

            // 현재 상태가 바꾸려는 상태와 동일하면 상태 전환하지 않는다.
            if (_currentState.State == state) return;

            // 현재 상태를 종료한다.
            _currentState.Exit();
        }

        // 바꾸려는 상태로 현재 상태를 전환
        switch (state)
        {
            case EnemyState.Stagger:
                _currentState = new StaggerState(this, 0.3f);
                break;
            case EnemyState.Death:
                _currentState = new DeathState(this, 1.0f);
                break;
            default:
                _currentState = new IdleState(this);
                break;
        }

        // 새 현재 상태 시작
        _currentState.Enter();
    }

    private void FixedUpdate()
    {
        //FollowTarget();

        // 현재 상태 객체의 업데이트 함수를 호출한다.
        if (_currentState == null) return;

        _currentState.Update();
    }

    /// <summary>
    /// 추적 대상 방향으로 이동하는 함수
    /// </summary>
    void FollowTarget()
    {
        // 적 캐릭터에서 타겟(주인공) 위치로 향하는 방향 벡터 구하기
        Vector3 dir = (_target.position - transform.position).normalized;
        _mover.Move(dir);
        
        // 적과 주인공 사이의 거리 구하기
        float dist = Vector2.Distance(transform.position, _target.position);
        if (dist > _repositionDistance)
        {
            Vector2 newPos = (Vector2)_target.position + Random.insideUnitCircle * _repositionDistance;
            transform.position = newPos;
        }
    }

    /// <summary>
    /// 이동을 중단하는 함수
    /// </summary>
    void Stop()
    {
        _mover.Move(Vector3.zero);
    }

    /// <summary>
    /// Mover가 이동했을 때 자동으로 호출되는 함수
    /// </summary>
    /// <param name="moveVec"></param>
    void OnMoved(Vector3 moveVec)
    {
        if (moveVec.x > 0)
        {
            _renderer.flipX = false;
        }
        if (moveVec.x < 0)
        {
            _renderer.flipX = true;
        }
    }

    // 1. 충돌 감지
    // 2. 주인공 캐릭터 확인
    // 3. 주인공 캐릭터 공격
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 1) 만들어 놓은 함수로 Layer 체크하는 방법
        if (_targetLayerMask.Contains(collision.gameObject.layer))
        {
            Hero hero = collision.gameObject.GetComponent<Hero>();
            if (hero != null)
            {
                // 3. 주인공 캐릭터 공격
                _attackRoutine = StartCoroutine(AttackRoutine(hero));
            }
        }
        
        //// 2) Layer가 LayerMask에 포함되는지 확인하는 방법 ('비트 연산')

        //// Enemy 입장에서
        //// 내가 부딪힌 상대 게임오브젝트의 레이어가
        //// 내가 목표로 하는 레이어마스크(_targetMaskLayer_)에 해당되면
        //if (((1 << collision.gameObject.layer) & _targetLayerMask.value) != 0)
        //{
        //    Hero hero = collision.gameObject.GetComponent<Hero>();
        //    if (hero != null)
        //    {
        //        // 3. 주인공 캐릭터 공격
        //        hero.TakeHit(_model.Damage);
        //    }
        //}

        //// 3) Layer 자체를 비교하는 방법
        //// 2. 주인공 캐릭터 확인
        //if (collision.gameObject.layer == LayerMask.NameToLayer("Hero"))
        //{
        //    Hero hero = collision.gameObject.GetComponent<Hero>();
        //    if (hero != null) 
        //    {
        //        // 3. 주인공 캐릭터 공격
        //        hero.TakeHit(_model.Damage);
        //    }
        //}
    }

    /// <summary>
    /// 피격을 처리하는 함수
    /// </summary>
    /// <param name="damage"></param>
    public void TakeHit(float damage)
    {
        if (_model.CurrentHp <= 0) return;

        ChangeState(EnemyState.Stagger);
        _model.TakeDamage(damage);
    }

    /// <summary>
    /// 죽었을 때 자동으로 호출되는 함수
    /// </summary>
    void OnDeath()
    {
        // 적 캐릭터 사망 이벤트 발행
        OnDeathEvent?.Invoke(_model.ExpReward);

        // 사망 처리(사망 상태로 변경)
        ChangeState(EnemyState.Death);
    }

    /// <summary>
    /// 이 적 캐릭터 게임오브젝트를 제거하는 함수
    /// </summary>
    void Remove()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hero"></param>
    /// <returns></returns>
    IEnumerator AttackRoutine(Hero hero)
    {
        while (true)
        {
            // 적이 주인공 캐릭터를 공격
            hero.TakeHit(_model.Damage);

            // _attackSpan 초만큼 대기
            yield return new WaitForSeconds(_attackSpan);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 공격 대상인 레이어마스크에 포함되는 레이어의 게임오브젝트와
        // 충돌이 끝나면
        if (_targetLayerMask.Contains(collision.gameObject.layer))
        {
            Hero hero = collision.gameObject.GetComponent<Hero>();
            if (hero != null)
            {
                // 코루틴 종료
                if(_attackRoutine != null)
                StopCoroutine(_attackRoutine);
            }
        }
    }

    //IEnumerator CheckEnemySenseHero()
    //{
    //    yield return new WaitForSeconds(1f);
    //    while (true)
    //    {
    //        EnemySenseHero();
    //        yield return new WaitForSeconds(1f);
    //    }
    //}

    //void EnemySenseHero()
    //{
    //    if (_target == null) return;

    //    float dist = Vector3.Distance(transform.position, _target.position);
    //    if (dist <= _attackRange)
    //    {
    //        Hero hero = _target.GetComponent<Hero>();
    //        if (hero != null)
    //        {
    //            hero.TakeHit(_model.Damage);
    //            Debug.Log("옆에있어서 데미지줌");
    //        }
    //    }
    //}

    // 적 캐릭터의 상태 종류 enum
    public enum EnemyState
    {
        Idle,       // 기본 상태
        Stagger,    // 피격 상태(휘청거리다)
        Death,      // 죽은 상태
    }

    // 적 캐릭터의 상태 인터페이스
    public interface IEnemyState
    {
        // 인터페이스는 프로퍼티도 멤버로 가질 수 있다.
        EnemyState State { get; }

        // 이 상태가 시작했을 때 동작하는 함수
        void Enter();

        // 이 상태가 진행 중일 때 반복적으로 동작하는 함수
        // (유니티 Update()와는 상관 X)
        void Update();

        // 이 상태가 끝났을 때 동작하는 함수
        void Exit();
    }

    /// <summary>
    /// 기본 상태 - 타겟을 지속적으로 추적
    /// </summary>
    public class IdleState : IEnemyState
    {
        public EnemyState State => EnemyState.Idle;
        Enemy _enemy;

        public IdleState(Enemy enemy)
        {
            _enemy = enemy;
        }

        public void Enter()
        {
            
        }

        public void Exit()
        {
            _enemy.Stop();
        }

        // 일반 상태인 동안에는 반복적으로 타겟 추적 실행
        public void Update()
        {
            // IdleState가 Enemy 클래스의 내부 클래스기 때문에
            // private 함수도 사용 가능
            _enemy.FollowTarget();
        }
    }

    /// <summary>
    /// 피격 상태 - 일정 시간 후 Idle 상태로 복귀
    /// </summary>
    public class StaggerState : IEnemyState
    {
        public EnemyState State => EnemyState.Stagger;
        Enemy _enemy;
        float _duration;        // 피격 상태 지속 시간
        float _timer;           // 피격 상태 타이머

        public StaggerState(Enemy enemy, float duration)
        {
            _enemy = enemy;
            _duration = duration;
        }

        public void Enter()
        {
            _enemy._animator.SetTrigger(AnimatorParameters.OnHit);
            _timer = _duration;
        }

        public void Exit()
        {

        }

        public void Update()
        {
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;
                if (_timer <= 0)
                {
                    // 기본 상태로 전환
                    _enemy.ChangeState(EnemyState.Idle);
                }
            }
        }
    }

    /// <summary>
    /// 사망 상태 - 일정 시간 후 적 캐릭터 게임오브젝트 제거
    /// </summary>
    public class DeathState : IEnemyState
    {
        public EnemyState State => EnemyState.Death;
        Enemy _enemy;
        float _duration;
        float _timer;

        public DeathState(Enemy enemy, float duration)
        {
            _enemy =enemy;
            _duration = duration;
        }

        public void Enter()
        {
            _enemy._animator.SetTrigger(AnimatorParameters.OnDeath);
            // 콜라이더 off
            // 죽어 있는 동안 주인공 캐릭터나 총알과 충돌하면 안 되니까
            _enemy._collider.enabled = false;
            // 리지드바디 off
            _enemy._rigid.simulated = false;

            // 공격 코루틴 종료
            if (_enemy._attackRoutine != null)
            {
                _enemy.StopCoroutine(_enemy._attackRoutine);
            }

            _timer = _duration;
        }

        public void Exit()
        {

        }

        public void Update()
        {
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;
                if (_timer <= 0)
                {
                    _enemy.Remove();
                }
            }
        }
    }
}
