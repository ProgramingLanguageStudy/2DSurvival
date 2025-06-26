using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 영웅의 스킬과 관련된 데이터
/// </summary>
[CreateAssetMenu(fileName = "HeroSkillData", menuName = "GameSettings/HeroSkillData")]
public class HeroSkillData : ScriptableObject
{
    [SerializeField] string _skillName;                        // 영웅 스킬이름
    [SerializeField] Sprite _skillIcon;                        // 영웅 스킬 아이콘
    [TextArea(3, 5)][SerializeField] string _skillDescription; // 영웅 스킬 설명
    [SerializeField] string _skillMessage;                     // 영웅 스킬 사용 시 출력대사
    [SerializeField] string _passiveSkillDesc;                 // 영웅 패시브스킬 설명

    public string SkillName => _skillName;
    public Sprite SkillIcon => _skillIcon;
    public string SkillDescription => _skillDescription;
    public string SKillMessage => _skillMessage;
    public string PassiveSkillDesc => _passiveSkillDesc;
}
