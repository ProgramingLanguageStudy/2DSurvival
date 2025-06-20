using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Intro에서 게임시작 버튼을 누르면 나오는 영웅 선택창 UI
/// </summary>
public class HeroSelectionView : MonoBehaviour
{
    [Header("----- 컴포넌트 참조 -----")]
    [SerializeField] HeroData _heroData;

    [Header("----- 연결해줘야함 -----")]
    [SerializeField] Image _heroIcon;                   // 영웅 아이콘
    [SerializeField] TextMeshProUGUI _nameText;     // 영웅 이름 텍스트
    [SerializeField] TextMeshProUGUI _passiveDescText;     // 영웅 능력치 텍스트
    [SerializeField] TextMeshProUGUI _skillNameText;// 영웅 스킬이름 텍스트
    [SerializeField] Image _skillIcon;              // 영웅 스킬 아이콘
    [SerializeField] TextMeshProUGUI _skillDescriptionText; // 영웅 스킬 설명 텍스트
    
    private void Start()
    {
        _heroIcon.sprite = _heroData.HeroIcon;
        _nameText.text = _heroData.Name;
        _passiveDescText.text = _heroData.PassiveDesc;
        _skillNameText.text = _heroData.SkillName;
        _skillIcon.sprite = _heroData.SkillIcon;
        _skillDescriptionText.text = _heroData.SkillDescription;
    }
}
