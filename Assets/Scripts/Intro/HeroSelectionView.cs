using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 캐릭터 선택창 프리펩에 각종 UI들이 있는데
/// 그 UI들(텍스트나 아이콘, 버튼 등)을 연결해서 보여주는 클래스
/// </summary>
public class HeroSelectionView : MonoBehaviour
{
    [Header("----- 연결해줘야함 -----")]
    [SerializeField] Button _heroSelectionView;     // 영웅 선택 버튼

    [SerializeField] Image _heroIcon;                   // 영웅 아이콘
    [SerializeField] TextMeshProUGUI _nameText;     // 영웅 이름 텍스트
    [SerializeField] TextMeshProUGUI _passiveDescText;     // 영웅 능력치 텍스트
    [SerializeField] TextMeshProUGUI _skillNameText;// 영웅 스킬이름 텍스트

    [SerializeField] Image _skillIcon;              // 영웅 스킬 아이콘
    [SerializeField] TextMeshProUGUI _skillDescriptionText; // 영웅 스킬 설명 텍스트

    [SerializeField] GameObject _tryBuying;         // 영웅 구매 전에 띄울 텍스트가 보일 공간
    [SerializeField] GameObject _afterBuying;       // 영웅 구매 후에 띄울 텍스트가 보일 공간

    [SerializeField] TextMeshProUGUI _tryBuyingText;    // 영웅 구매 전에 보여질 텍스트
    [SerializeField] TextMeshProUGUI _afterBuyingText;  // 영웅 구매 후에 보여질 텍스트

    HeroData _heroData;
    UIText _uiText;

    public event Action<HeroData> OnClickSelected;
    
    public void Initialize(HeroData heroData, UIText uiText)
    {
        _heroData = heroData;
        _uiText = uiText;

        // 버튼 초기화 후 선택 시 호출될 함수 연결
        _heroSelectionView.onClick.RemoveAllListeners();
        _heroSelectionView.onClick.AddListener(OnClickSelect);

        // 구매 버튼 리스너 설정
        _tryBuying.GetComponent<Button>().onClick.RemoveAllListeners();
        _tryBuying.GetComponent<Button>().onClick.AddListener(() =>
        {
            GameManager.Instance.TryBuy(_heroData.UnlockCost);
        });

        // 각종 UI들 연결
        _heroIcon.sprite = _heroData.HeroIcon;
        _nameText.text = _heroData.Name;
        _passiveDescText.text = _heroData.PassiveDesc;
        _skillNameText.text = _heroData.SkillName;
        
        _skillIcon.sprite = _heroData.SkillIcon;
        _skillDescriptionText.text = _heroData.SkillDescription;

        // UIText에 적어놓은 문장들 가져오기
        _tryBuyingText.text = string.Format(_uiText.buyingText, _heroData.UnlockCost);
        _afterBuyingText.text = _uiText.afterBuyingText;

        // 영웅 구매 전과 후에 보여질 텍스트 관리
        bool isUnlocked = _heroData.IsUnlocked;

        // 영웅을 구매한 상태라면
        if (isUnlocked == true)
        {
            // 구매 전 텍스트는 끄고
            _tryBuying.SetActive(false);

            // 구매 후 텍스트는 켠다.
            _afterBuying.SetActive(true);
        }
        else
        {
            _tryBuying.SetActive(true);
            _afterBuying.SetActive(false);
        }
    }

    /// <summary>
    /// 영웅 선택창에서 버튼을 누르면 호출될 함수 -> 이벤트로 할까?
    /// </summary>
    void OnClickSelect()
    {
        OnClickSelected?.Invoke(_heroData);
    }

    void OnClickBuy()
    {

    }
}
