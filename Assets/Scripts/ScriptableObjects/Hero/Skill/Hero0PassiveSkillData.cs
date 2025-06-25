using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Hero0의 패시브 스킬을 구현하는 클래스
/// 기본 지급 무기의 레벨 + 1
/// </summary>
[CreateAssetMenu(fileName = "Hero0PassiveSkillData", menuName = "Hero/Skill/Hero0PassiveSkillData")]
public class Hero0PassiveSkillData : HeroPassiveSkillData
{
    public override void ApplyPassiveSkill(GameObject hero)
    {
        IUpgradable[] upgradables = hero.GetComponentsInChildren<IUpgradable>();
        if (upgradables.Length > 2)
        {
            upgradables[2].Upgrade();
        }
    }
}

