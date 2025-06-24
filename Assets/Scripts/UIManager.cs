using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI에 필요한 컴포넌트들을 연결해줄 매개체 클래스.
/// 싱글톤으로 게임 내내 지속된다.
/// 게임 내내 지속되는 UI들을 관리한다.
/// </summary>
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    
    // 스크립터블오브젝트 연결
    [SerializeField] UIText _uIText;                // UI에 출력될 Text들 관리
    [SerializeField] UIIcon _uiIcon;                // UI에 출력될 Icon들 관리

    [SerializeField] Image _goldIcon;               // 골드 아이콘이 배치될 이미지
    [SerializeField] TextMeshProUGUI _goldText;     // 골드 텍스트가 배치될 텍스트

    [SerializeField] GameObject _infoPopupPrefab;   // 인포팝업 프리펩
    [SerializeField] RectTransform _panel;          // 인포팝업 생성할 패널위치

    GameObject _infoPopup;

    public UIText UIText => _uIText;
    public UIIcon UIIcon => _uiIcon;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        GameManager.Instance.OnGoldChanged += UpdateGold;
        GameManager.Instance.HeroBuyFailed += BuyFailMessage;
        GameManager.Instance.HeroBuySuccessed += BuySuccessMessage;
        GameManager.Instance.HeroSelectFailed += HeroSelectFailMessage;

        GameObject infoPopup = Instantiate(_infoPopupPrefab, _panel);
        _infoPopup = infoPopup;
        _infoPopup.SetActive(false);

        _goldIcon.sprite = _uiIcon._goldIcon;
        _goldText.text = GameManager.Instance.Gold.ToString();
    }

    public void UpdateGold(int amount)
    {
        _goldText.text = GameManager.Instance.Gold.ToString();
    }

    public void BuyFailMessage()
    {
        _infoPopup.GetComponent<InfoPopup>().Show(_uIText.FailedBuyingText);
    }

    public void BuySuccessMessage()
    {
        _infoPopup.GetComponent<InfoPopup>().Show(_uIText.SuccessBuyingText);
    }

    public void HeroSelectFailMessage()
    {
        _infoPopup.GetComponent<InfoPopup>().Show(_uIText.HeroSelectFailText);
    }
}
