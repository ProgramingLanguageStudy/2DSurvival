using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Weapon의 역할에 대해 항상 정확하게 생각하고 인지해야함.
// Weapon은 Bullet에 담을 데이터 값만 갖고있고
// Bullet을 생성하는 역할까지만 한다.
// (날아가던 회전을 하던 이런건 Bullet에서 할 일이다.)
// Bullet을 생성할 때에 필요한 데이터 뿐만 아니라
// 적을 탐지할지? 아니면 일정 주기마다 할지에 대한
// '판단 여부'도 갖고 있어야 한다.
// abstract를 붙여놨으므로 이 클래스로는 객체를 생성하지 않겠다.

/// <summary>
/// 무기의 공통 기능을 포함하는 추상 클래스.
/// </summary>
public abstract class Weapon : MonoBehaviour, IUpgradable
{
    [Header("----- 스탯 데이터 -----")]
    // 무기 데이터
    [SerializeField] protected WeaponData _data;

    // 현재 무기 레벨
    [SerializeField] protected int _level;

    // 데미지
    [SerializeField] protected float _damage;

    public string UpgradeName => _data.WeaponName;
    public string Description => _data.Description;
    public Sprite IconSprite => _data.IconSprite;
    public int level => _level;
    public bool IsMaxLevel => _level >= _data.MaxLevel;

    ///// <summary>
    ///// 초기화 함수
    ///// </summary>
    //public abstract void Initialize();

    /// <summary>
    /// 레벨에 따른 무기 스텟을 계산하는 함수
    /// </summary>
    protected virtual void CalculateStats()
    {
        // 무기 레벨에 따른 데미지 계산
        _damage = _data.GetStat(WeaponStatType.Damage, _level);
    }

    public virtual void Upgrade()
    {
        _level++;  // 무기 레벨을 하나 올리고
        CalculateStats();  // 스텟을 다시 계산한다.
    }
}
