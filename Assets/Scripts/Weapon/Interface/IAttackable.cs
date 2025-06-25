using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum WeaponType
//{
//    Shovel,
//    Gun
//}

/// <summary>
/// 공격력을 가지고 공격하는 무기들의 인터페이스
/// </summary>
public interface IAttackable
{
    //WeaponType WeaponType { get; }
    float Damage { get; }
    /// <summary>
    /// 무기들의 공격력을 변경할 때 사용하는 함수
    /// </summary>
    /// <param name="additionalDamage">변화할 데미지비율(ex:20% 증가면 0.2f, 감소면 -0.2f)</param>
    public void SetDamage(float additionalDamage);
}
