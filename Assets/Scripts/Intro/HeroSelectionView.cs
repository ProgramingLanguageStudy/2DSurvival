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

    HeroDataBundle _heroDataBundle;
    UIText _uiText;

    // 영웅 선택 버튼을 눌렀을 때 발생시킬 이벤트
    public event Action<int> OnClickSelected;
    
    public void Initialize(HeroDataBundle heroDataBundle, UIText uiText)
    {
        _heroDataBundle = heroDataBundle;
        _uiText = uiText;

        // 버튼 초기화 후 선택 시 호출될 함수 연결
        _heroSelectionView.onClick.RemoveAllListeners();
        _heroSelectionView.onClick.AddListener(OnClickSelect);

        // 구매 버튼 리스너 설정
        _tryBuying.GetComponent<Button>().onClick.RemoveAllListeners();
        _tryBuying.GetComponent<Button>().onClick.AddListener(() =>
        {
            GameManager.Instance.TryBuy(_heroDataBundle, _heroDataBundle.HeroStatData.UnlockCost);
        });

        // 각종 UI들 연결
        _heroIcon.sprite = _heroDataBundle.HeroDisplayData.HeroIcon;
        _nameText.text = _heroDataBundle.HeroDisplayData.Name;
        _passiveDescText.text = _heroDataBundle.HeroSkillData.PassiveSkillDesc;
        _skillNameText.text = _heroDataBundle.HeroSkillData.SkillName;
        
        _skillIcon.sprite = _heroDataBundle.HeroSkillData.SkillIcon;
        _skillDescriptionText.text = _heroDataBundle.HeroSkillData.SkillDescription;

        // UIText에 적어놓은 문장들 가져오기
        _tryBuyingText.text = string.Format(_uiText.BuyingText, _heroDataBundle.HeroStatData.UnlockCost);
        _afterBuyingText.text = _uiText.AfterBuyingText;

        HeroUnlockManager.Instance.OnHeroUnlocked += HandleHeroBuyingUI;
        OnClickSelected += GameManager.Instance.OnHeroSelected;
    }

    /// <summary>
    /// 영웅 선택창에서 버튼을 누르면 호출될 함수 -> 이벤트로 할까?
    /// </summary>
    void OnClickSelect()
    {
        OnClickSelected?.Invoke(_heroDataBundle.HeroId);
    }

    void OnClickBuy()
    {

    }

    /// <summary>
    /// 영웅의 잠금여부에 따라 보여질 영웅 구매 버튼 관리
    /// </summary>
    /// <param name="heroId"></param>
    private void HandleHeroBuyingUI(int heroId)
    {
        if (heroId == _heroDataBundle.HeroId)
        {
            _tryBuying.SetActive(false);
            _afterBuying.SetActive(true);
        }
    }
}
