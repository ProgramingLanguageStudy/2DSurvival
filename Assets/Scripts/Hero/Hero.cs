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
    [SerializeField] CharacterHud _hud;
    [SerializeField] Mover _mover;
    [SerializeField] Animator _animator;
    [SerializeField] SpriteRenderer _renderer;

    public void Initialize()
    {
        _model.Initialize();

        _mover.OnMoved += OnMoved;
        _model.OnDeath += OnDeath;
        _model.OnHpChanged += _hud.SetHpBar;
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

    public void OnDeath()
    {
        _animator.SetTrigger(AnimatorParameters.OnDeath);
        StartCoroutine(WaitAndDestroy());
    }

    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(2f); // 애니메이션 길이만큼 대기
        Destroy(gameObject);
        
    }
}
