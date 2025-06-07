using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Play 씬을 총괄하는 역할
/// </summary>
public class PlayScene : MonoBehaviour
{
    [Header("----- 스테이지 데이터 -----")]
    [SerializeField] StageData _stageData;

    [Header("----- 컴포넌트 참조 -----")]
    [SerializeField] InputHandler _inputHandler;
    [SerializeField] Hero _hero;
    [SerializeField] EnemySpawner _enemySpawner;
    [SerializeField] StatusView _statusView;
    [SerializeField] Upgrader _upgrader;
    [SerializeField] TimeChecker _timeChecker;

    void Start()
    {
        // 이동 입력 이벤트 구독
        _inputHandler.OnMoveInput += OnMoveInput;

        // 킬 수 변화 이벤트 구독
        _enemySpawner.OnKillCountChanged += _statusView.SetKillCountText;

        // 경험치 획득 이벤트 구독
        _enemySpawner.OnExpGained += _hero.AddExp;

        // 남은 시간 변화 이벤트 구독
        _enemySpawner.OnRemainingTimeChanged += _statusView.SetRemainingTimeText;
        _enemySpawner.OnRemainingTimeChanged += _timeChecker.CheckRemainingTime;

        // 주인공 경험치 변화 이벤트 구독
        _hero.OnExpChanged += _statusView.SetExpBar;

        // 주인공 레벨 변화 이벤트 구독
        _hero.OnLevelChanged += OnHeroLevelChanged;

        _hero.Initialize();
        _enemySpawner.Initialize(_stageData);
    } 

    /// <summary>
    /// 이동 입력이 들어왔을 때 실행하는 함수
    /// </summary>
    /// <param name="inputVec"></param>
    public void OnMoveInput(Vector2 inputVec)
    {
        _hero.Move(inputVec);
    }

    /// <summary>
    /// 주인공 레벨이 변화했을 때 자동으로 호출되는 함수
    /// </summary>
    /// <param name="preLevel">이전 레벨</param>
    /// <param name="level">레벨</param>
    void OnHeroLevelChanged(int preLevel, int level)
    {
        // UI에 레벨 표시
        _statusView.SetLevelText(level);

        // 레벨 업에 따른 업그레이드 UI 제공
        _upgrader.OnLevelUp(level - preLevel);
    }
}
