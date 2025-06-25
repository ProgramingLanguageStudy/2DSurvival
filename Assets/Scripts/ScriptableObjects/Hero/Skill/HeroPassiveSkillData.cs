using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 영웅이 게임 시작시 갖게되는 패시브 스킬을 관리하는 SO
/// 게임 내내 지속되어 강력한 능력치를 제공한다.
/// </summary>
public abstract class HeroPassiveSkillData : ScriptableObject, IPassiveSkill
{
    public abstract void ApplyPassiveSkill(GameObject hero);
}
