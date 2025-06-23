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
    [SerializeField] UIText _uIText;
    [SerializeField] UIIcon _uiIcon;

    [SerializeField] Image _goldIcon;
    [SerializeField] TextMeshProUGUI _goldText;

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
        _goldIcon.sprite = _uiIcon._goldIcon;
        _goldText.text = GameManager.Instance.Gold.ToString();
    }

    public void UpdateGold(int amount)
    {
        _goldText.text = GameManager.Instance.Gold.ToString();
    }
}
