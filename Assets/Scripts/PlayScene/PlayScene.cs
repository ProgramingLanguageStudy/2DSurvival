using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/// <summary>
/// Play 씬을 총괄하는 역할
/// </summary>
public class PlayScene : MonoBehaviour
{
    [Header("----- PlayScene테스트용 -----")]
    [SerializeField] int _testHeroId = 0;  // Inspector에서 지정 가능

    [Header("----- HeroDatabase -----")]
    [SerializeField] HeroDatabase _heroDatabase;

    [Header("----- EnemyDatabase -----")]
    [SerializeField] EnemyDatabase _enemyDatabase;

    [Header("----- 스테이지 데이터 -----")]
    [SerializeField] StageData _stageData;

    [Header("----- 컴포넌트 참조 -----")]
    [SerializeField] InputHandler _inputHandler;
    [SerializeField] Hero _hero;
    [SerializeField] EnemySpawner _enemySpawner;
    [SerializeField] StatusView _statusView;
    [SerializeField] Upgrader _upgrader;
    [SerializeField] TimeChecker _timeChecker;
    [SerializeField] CinemachineVirtualCamera _virtualCamera;

    int _heroId;

    private void Awake()
    {
        // 테스트용으로 플레이씬에서 바로 시작할 시
        if (GameManager.Instance == null)
        {
            _heroId = _testHeroId;
        }
        // 인트로씬에서 넘어올 시 선택한 HeroId를 받음
        else
        {
            GameManager.Instance.HeroSelectSuccessed += GetHeroId;
            Debug.Log(_heroId);
            //_heroId = GameManager.Instance.HeroId;
        }

        // 정해진 ID를 이용하여 HeroPrefabs 배열에 저장된 프리펩들 중 해당 Hero 프리펩 생성 후 heroObj에 저장
        GameObject heroObj = Instantiate(_heroDatabase.heroDataBundleList[_heroId].HeroPrefab, Vector3.zero, Quaternion.identity);

        _upgrader.SetHero(heroObj);
        // GetComponent로 컴포넌트 가져오기
        _hero = heroObj.GetComponent<Hero>();
        _virtualCamera.Follow = _hero.transform;
    }

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

        _hero.Initialize(_heroDatabase.heroDataBundleList[_heroId].HeroStatData);

        //_enemySpawner.Initialize(_stageData, _hero.transform, _enemyDatabase.enemyDataBundleList.EnemyStatData);

        _statusView.Initialize(_heroDatabase.heroDataBundleList[_heroId].HeroDisplayData);
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

    void GetHeroId(int heroId)
    {
        _heroId= heroId;
    }
}
