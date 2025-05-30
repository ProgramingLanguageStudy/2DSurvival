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
    [SerializeField] VoluspaSkill _skill;

    Coroutine _spawnVoluspa;

    public void Initialize()
    {
        _model.Initialize();

        _mover.OnMoved += OnMoved;
        _model.OnHpChanged += _hud.SetHpBar;
        _model.OnSpeedChanged += _mover.SetSpeed;

        _spawnVoluspa = StartCoroutine(SpawnVoluspa());
    }

    IEnumerator SpawnVoluspa()
    {
        while (true)
        {
            _skill.Activate();
            yield return new WaitForSeconds(3.0f);
        }
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
        _animator.SetFloat(AnimatorParameters.MoveSpeed,velocity.magnitude);
    }

    public void TakeHit(float damage)
    {
        _model.TakeDamage(damage);
    }

}
