using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 주인공 캐릭터 담당 역할
/// </summary>
public class Hero : MonoBehaviour
{
    [Header("----- 컴포넌트 -----")]
    [SerializeField] HeroModel _model;
    [SerializeField] Mover _mover;
    [SerializeField] Animator _animator;
    [SerializeField] SpriteRenderer _renderer;
    

    public void Initialize()
    {
        _model.Initialize();

        _mover.OnMoved += OnMoved;
    }

    public void Move(Vector3 direction)
    {
        _mover.Move(direction);
    }

    void OnMoved(Vector3 velocity)
    {
        if(velocity.x > 0)
        {
            _renderer.flipX = false;
        }
        if(velocity.x < 0)
        {
            _renderer.flipX = true;
        }
        _animator.SetFloat(AnimatorParameters.MoveSpeed,
            velocity.magnitude);
    }

    public void TakeHit(float damage)
    {
        _model.TakeDamage(damage);
    }
}
