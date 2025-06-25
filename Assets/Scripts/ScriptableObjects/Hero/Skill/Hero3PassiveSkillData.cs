using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Hero3의 패시브 스킬을 구현하는 클래스
/// 무기 데미지 20% 증가
/// </summary>
[CreateAssetMenu(fileName = "Hero3PassiveSkillData", menuName = "Hero/Skill/Hero3PassiveSkillData")]
public class Hero3PassiveSkillData : HeroPassiveSkillData
{
    public override void ApplyPassiveSkill(GameObject hero)
    {
        IAttackable[] attackables = hero.GetComponentsInChildren<IAttackable>();
        foreach (var attackable in attackables)
        {
            attackable.SetDamage(0.2f);
        }
    }
}
