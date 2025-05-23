using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 적 캐릭터 담당 역할
/// </summary>
public class Enemy : MonoBehaviour
{
    [Header("----- 컴포넌트 참조 -----")]
    [SerializeField] EnemyModel _model;
    [SerializeField] CharacterHud _hud;
    [SerializeField] Mover _mover;
    [SerializeField] Animator _animtor;
    [SerializeField] SpriteRenderer _renderer;

    [Header("----- 공격 -----")]
    // 공격 타겟 레이어 마스크
    [SerializeField] LayerMask _targetLayerMask;

    // 임시
    [Header("----- 임시 -----")]
    Transform _target;

    public void Initialize(Transform target)
    {
        _target = target;

        _model.Initialize();

        _mover.OnMoved += OnMoved;
        //_model.OnDeath += OnDeath;
        _model.OnHpChanged += _hud.SetHpBar;
    }

    private void FixedUpdate()
    {
        FollowTarget();
    }

    void FollowTarget()
    {
        // 적 캐릭터에서 타겟(주인공) 위치로 향하는 방향 벡터 구하기
        Vector3 dir = (_target.position - transform.position).normalized;
        _mover.Move(dir);
    }

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
                hero.TakeHit(_model.Damage);
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

    public void TakeHit(float damage)
    {
        if (_model.CurrentHp <= 0) return;

        _model.TakeDamage(damage);
    }

}
