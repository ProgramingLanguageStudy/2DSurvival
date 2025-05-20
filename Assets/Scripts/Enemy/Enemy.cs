using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("----- 컴포넌트 참조 -----")]
    [SerializeField] EnemyModel _model;
    [SerializeField] Mover _mover;
    [SerializeField] Animator _animtor;
    [SerializeField] SpriteRenderer _renderer;

    [Header("----- 공격 -----")]
    // 공격 타겟 레이어 마스크
    [SerializeField] LayerMask _targetLayerMask;

    // 임시
    [Header("----- 임시 -----")]
    [SerializeField] Transform _target;

    private void Start()
    {
        _mover.OnMoved += onMoved;
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

    void onMoved(Vector3 moveVec)
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
        // TODO: LayerMask 사용 방법

        // 2. 주인공 캐릭터 확인
        if (collision.gameObject.layer == LayerMask.NameToLayer("Hero"))
        {
            Hero hero = collision.gameObject.GetComponent<Hero>();
            if (hero != null) 
            {
                hero.TakeHit(_model.Damage);
            }
        }
    }

}
