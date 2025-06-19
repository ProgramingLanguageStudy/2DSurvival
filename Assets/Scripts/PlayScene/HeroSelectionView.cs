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
    [SerializeField] Image _icon;                   // 영웅 아이콘
    [SerializeField] TextMeshProUGUI _nameText;     // 영웅 이름 텍스트
    [SerializeField] TextMeshProUGUI _descText;     // 영웅 능력치 텍스트
    [SerializeField] TextMeshProUGUI _skillNameText;// 영웅 스킬이름 텍스트
    [SerializeField] Image _skillIcon;              // 영웅 스킬 아이콘

    HeroData _heroData;

    public void Initialize(HeroData heroData)
    {
        
    }

    
}
