using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    [Header("내가만든임시스킬")]
    [SerializeField] VoluspaSkill _skill;
    Coroutine _spawnVoluspa;

    // event 변수를 프로퍼티처럼 쓰는 방법
    // 외부에서 Hero의 OnExpChanged 이벤트를 구독/해제하게 되면
    // 사실은 _model(HeroModel)의 OnExpChanged 이벤트를 구독/해제하게 되는 것과 같음
    // 중개해주는 역할임
    public event UnityAction<float, float> OnExpChanged
    {
        // 구독 동작 설정
        add => _model.OnExpChanged += value;
        // 구독 해제 동작 설정
        remove => _model.OnExpChanged -= value;
    }

    public event UnityAction<int, int> OnLevelChanged
    {
        add => _model.OnLevelChanged += value;
        remove => _model.OnLevelChanged -= value;
    }

    public void Initialize(HeroStatData heroStatData)
    {
        _model.Initialize(heroStatData);

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

    /// <summary>
    /// 경험치를 획득하는 함수
    /// </summary>
    /// <param name="amount"></param>
    public void AddExp(float amount)
    {
        _model.AddExp(amount);
    }
}
