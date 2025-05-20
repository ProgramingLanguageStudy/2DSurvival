using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("----- 컴포넌트 참조 -----")]
    [SerializeField] Mover _mover;
    [SerializeField] Animator _animtor;
    [SerializeField] SpriteRenderer _renderer;

    // 임시
    [Header("----- 임시 -----")]
    [SerializeField] Transform _target;


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
}
