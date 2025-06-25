
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PassiveSkill에 해당하는 녀석들이 갖게될 인터페이스.
/// 패시브 스킬을 사용하는 함수를 통해 패시브스킬을 적용할 수 있다.
/// </summary>
public interface IPassiveSkill
{
    void ApplyPassiveSkill(GameObject hero);
}
