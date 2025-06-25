
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Hero2의 패시브 스킬을 구현하는 클래스
/// 기본 이동속도 20% 증가
/// </summary>
[CreateAssetMenu(fileName = "Hero2PassiveSkillData", menuName = "Hero/Skill/Hero2PassiveSkillData")]
public class Hero2PassiveSkillData : HeroPassiveSkillData
{
    public override void ApplyPassiveSkill(GameObject hero)
    {
        HeroModel heromodel = hero.GetComponent<HeroModel>();
        heromodel.SetBonusSpeedRation(0.2f);
    }
}
