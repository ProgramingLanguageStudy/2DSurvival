using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 주인공 기본 능력치를 포함하는 설정 데이터 클래스
/// </summary>
[CreateAssetMenu(fileName = "HeroData", menuName = "GameSettings/HeroData")]
public class HeroData : ScriptableObject
{
    [SerializeField] float _maxHp;      // 기본 최대 체력
    [SerializeField] float _speed;      // 기본 이동 속력
    [SerializeField] float _baseExp;    // 기본 경험치
    [SerializeField] float _expIncrementRate; // 경험치 배수

    [Header("----- 해금 정보 -----")]
    [SerializeField] int _heroId;
    [SerializeField] int _unlockCost;
    public bool IsUnlocked;

    public int HeroId => _heroId;
    public int UnlockCost => _unlockCost;

    // 저장값은 ScriptableObject에 직접 포함하지 않고 PlayerPrefs 등으로 관리

    [Header("----- UI용 -----")]
    [SerializeField] Sprite _heroIcon;               // 영웅 아이콘
    [SerializeField] string _name;          // 영웅 이름 텍스트
    [SerializeField] string _passiveDesc;          // 영웅 능력치 텍스트
    [SerializeField] string _skillName;     // 영웅 스킬이름 텍스트
    [SerializeField] Sprite _skillIcon;         // 영웅 스킬 아이콘
    [TextArea(3, 5)] [SerializeField] string _skillDescription;  // 영웅 스킬 설명
    [SerializeField] string _skillMessage;      // 영웅 스킬 사용 시 출력대사

    public float MaxHp => _maxHp;
    public float Speed => _speed;
    public Sprite HeroIcon => _heroIcon;
    public string Name => _name;
    public string PassiveDesc => _passiveDesc;
    public string SkillName => _skillName;
    public Sprite SkillIcon => _skillIcon;
    public string SkillDescription => _skillDescription;
    public string SKillMessage => _skillMessage;
    
    /// <summary>
    /// 레벨에 따른 필요 경험치를 반환해 주는 함수
    /// </summary>
    /// <param name="level">레벨</param>
    /// <returns></returns>
    public float GetExp(int level)
    {
        if(level <= 0)
            return _baseExp;

        return _baseExp * Mathf.Pow(_expIncrementRate, level - 1);
    }
}
