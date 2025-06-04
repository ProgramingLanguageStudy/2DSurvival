using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 업그레이드 선택 시스템을 관리하는 클래스
/// </summary>
public class Upgrader : MonoBehaviour
{
    IUpgradable[] _upgradables;     // 전체 업그레이드 가능한 대상들
    List<IUpgradable> _shuffledUpgradables = new();

    [SerializeField] GameObject _upgradePanel; // 업그레이드 패널
    [SerializeField] SelectionView[] _selectionViews;  // 업그레이드 선택 뷰 배열

    int _upgradeCount = 0; // 남은 업그레이드 선택 횟수

    // Awake() 함수는 Start(), Update()처럼 유니티에서
    // 자동으로 호출해 주는 함수
    // 호출 시점: Start() 이전, 이 객체가 갓 로드되었을 때
    // Start()보다 빠른 시점에 작동하는 함수
    // 인스펙터뷰 연결결 대신, 컴포넌트 참조를 코드로 처리할 때 유용
    private void Awake()
    {
        // 자식 게임오브젝트들로부터 모든 IUpgradable을
        // 자식게임오브젝트 순서대로 가져온다.
        _upgradables = GetComponentsInChildren<IUpgradable>();
    }

    private void Update()
    {
        // 숫자 1키 누르면 장갑 업그레이드
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _upgradables[0].Upgrade();
        }

        // 숫자 2키 누르면 부츠 업그레이드
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _upgradables[1].Upgrade();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _upgradables[2].Upgrade();
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _upgradables[3].Upgrade();
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            _upgradables[4].Upgrade();
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            _upgradables[5].Upgrade();
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            _upgradables[6].Upgrade();
        }
    }

    /// <summary>
    /// 레벨업을 했을 때 호출되는 함수
    /// </summary>
    /// <param name="increasedLevel">이번에 증가한 레벨</param>
    public void OnLevelUp(int increasedLevel)
    {
        if (increasedLevel > 0)
        {
            _upgradeCount = increasedLevel;
            BeginSelection();
        }
    }

    /// <summary>
    /// 업그레이드 선택을 시작하는 함수
    /// 업그레이드 후보를 셔플하고 UI에 표시.
    /// </summary>
    void BeginSelection()
    {
        // 1. 업그레이드 가능한 업그레이드 후보만 선택
        // 2. 선별된 후보들을 셔플
        // 3. UI에 표시
        // 4. 게임 일시 정지
        // 5. 남은 업그레이드 횟수 카운트 1 감소

        // 1. 업그레이드 가능한 업그레이드 후보만 선택
        _shuffledUpgradables.Clear();
        foreach (var upgradable in _upgradables)
        {
            // 업그레이드 대상 객체가 최대 레벨이 아니면
            if (upgradable.IsMaxLevel == false)
            {
                // 업그레이드 후보 리스트에 포함한다.
                _shuffledUpgradables.Add(upgradable);
            }
        }

        // 2. 선별된 후보들을 셔플
        for (int i = 0; i < +_shuffledUpgradables.Count; i++)
        {
            int randomIndex = Random.Range(0, _shuffledUpgradables.Count);
            IUpgradable temp = _shuffledUpgradables[i];
            _shuffledUpgradables[i] = _shuffledUpgradables[randomIndex];
            _shuffledUpgradables[randomIndex] = temp;
        }

        // 3. UI에 표시
        for (int i = 0; i < _selectionViews.Length; i++)
        {
            if (i < _shuffledUpgradables.Count)
            {
                _selectionViews[i].Initialize(this, _shuffledUpgradables[i]);
                _selectionViews[i].gameObject.SetActive(true);
            }
            else
            {
                _selectionViews[i].gameObject.SetActive(false);
            }
        }
        _upgradePanel.SetActive(true);

        // 4. 게임 일시 정지
        Time.timeScale = 0;

        // 5. 남은 업그레이드 횟수 카운트 1 감소
        _upgradeCount--;
    }

    /// <summary>
    /// 업그레이드 선택을 종료하는 함수
    /// </summary>
    public void EndSelection()
    {
        // 남은 업그레이드 수가 있으면 다시 업그레이드 선택 시작
        if (_upgradeCount > 0)
        {
            BeginSelection();
        }
        else
        {
            // 업그레이드 창 끄기
            _upgradePanel.SetActive(false);

            // 게임 일시정지 해제
            Time.timeScale = 1.0f;
        }
    }
}
