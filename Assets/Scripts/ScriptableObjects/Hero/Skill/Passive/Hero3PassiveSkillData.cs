using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Hero3�� �нú� ��ų�� �����ϴ� Ŭ����
/// ���� ������ 20% ����
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
