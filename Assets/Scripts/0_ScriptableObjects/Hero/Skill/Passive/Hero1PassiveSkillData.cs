using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Hero1의 패시브 스킬을 구현하는 클래스
/// 기본 최대체력 50% 증가
/// </summary>
[CreateAssetMenu(fileName = "Hero1PassiveSkillData", menuName = "Hero/Skill/Hero1PassiveSkillData")]
public class Hero1PassiveSkillData : HeroPassiveSkillData
{
    public override void ApplyPassiveSkill(GameObject hero)
    {
        HeroModel heromodel = hero.GetComponent<HeroModel>();
        heromodel.SetBonusMaxHp(heromodel.MaxHp / 2);
    }
}
