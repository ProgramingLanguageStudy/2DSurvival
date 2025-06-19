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

    [Header("----- UI용 -----")]
    [SerializeField] Sprite _heroIcon;               // 영웅 아이콘
    [SerializeField] string _nameText;          // 영웅 이름 텍스트
    [SerializeField] string _descText;          // 영웅 능력치 텍스트
    [SerializeField] string _skillNameText;     // 영웅 스킬이름 텍스트
    [SerializeField] Sprite _skillIcon;          // 영웅 스킬 아이콘
    [TextArea(3, 5)] [SerializeField] string _skillDescription;  // 영웅 스킬 설명

    public float MaxHp => _maxHp;
    public float Speed => _speed;
    
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
