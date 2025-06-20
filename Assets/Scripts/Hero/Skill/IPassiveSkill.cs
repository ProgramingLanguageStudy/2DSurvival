using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 영웅마다 갖고있는 고유스킬(패시브, 30초마다 발동)을 사용하게 해주는 인터페이스
/// </summary>
public interface IPassiveSkill
{
    void Activate(GameObject owner);  // 소유자에게 스킬 발동
}
