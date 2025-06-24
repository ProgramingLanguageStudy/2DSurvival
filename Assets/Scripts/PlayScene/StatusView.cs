using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// PlayScene의 상태 UI 표시 클래스(경험치 바, 레벨, 킬 수, 남은 시간)
/// </summary>
public class StatusView : MonoBehaviour
{
    [Header("----- 컴포넌트 참조 -----")]
    [SerializeField] Image _heroIcon;
    [SerializeField] Image _expBar;                     // 경험치 바 이미지
    [SerializeField] TextMeshProUGUI _levelText;        // 레벨 텍스트
    [SerializeField] TextMeshProUGUI _killCountText;    // 킬 수 텍스트
    [SerializeField] TextMeshProUGUI _remainingTimeText;// 남은 시간 텍스트

    HeroDisplayData _heroDisplayData;

    public void Initialize(HeroDisplayData heroDisplayData)
    {
        _heroDisplayData = heroDisplayData;
        _heroIcon.sprite = _heroDisplayData.HeroIcon;
    }

    /// <summary>
    /// 경험치 바 UI를 설정하는 함수
    /// </summary>
    /// <param name="currentExp">현재 경험치</param>
    /// <param name="maxExp">레벨업에 필요한 최대 경험치</param>
    public void SetExpBar(float currentExp, float maxExp)
    {
        // 경험치 바의 fillAmount를 설정
        _expBar.fillAmount = currentExp / maxExp;
    }

    /// <summary>
    /// 레벨 텍스트 UI를 설정하는 함수. 
    /// +1을 하는 이유는 레벨이 0부터 시작하기 때문
    /// </summary>
    /// <param name="level"></param>
    public void SetLevelText(int level)
    {
        _levelText.text = (level + 1).ToString();
    }

    /// <summary>
    /// 킬 수 텍스트 UI를 설정하는 함수
    /// </summary>
    /// <param name="killCount"></param>
    public void SetKillCountText(int killCount)
    {
        _killCountText.text = killCount.ToString();
    }

    /// <summary>
    /// 남은 시간 텍스트 UI를 mm:ss(분:초)로 설정하는 함수
    /// </summary>
    /// <param name="remainingTime"></param>
    public void SetRemainingTimeText(float remainingTime)
    {
        // 남은 시간을 분:초 형식으로 변환하여 텍스트로 설정
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        _remainingTimeText.text = string.Format("{0:D2} : {1:D2}", minutes, seconds);
    }
}
