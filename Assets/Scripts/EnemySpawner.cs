using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
게임오브젝트 동적 생성(프리펩을 복제해서 신에 게임오브젝트 생성) 지침
1) 프리펩에서 인스펙터뷰 컴포넌트 참조 변수 연결은
프리펩 자신 게임오브젝트나, 자식 게임오브젝트의 컴포넌트들만 가능하다.
(씬에 있는 컴포넌트를 프리펩 인스펙터뷰 변수에 연결할 수 없다.)

2) 프리펩을 복제하여 생성하는 클래스에서는
애초에 해당 프리펩 변수를 게임오브젝트 자료형 대신
초기화가 필요한 컴포넌트 자료형으로 하는 것이 편하다.

3) {자료형} {변수명} = Instantiate({프리펩 변수명});
이 형식으로 프리펩에서 복제되어 씬에 생성된
게임오브젝트(컴포넌트)를 변수로 받아 사용할 수 있다.
*/


/// <summary>
/// 일정 시간 간격으로 적을 타겟(주인공) 주변에 생성하는 역할
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    [Header("----- 스테이지 데이터 -----")]
    [SerializeField] StageData _stageData;

    [Header("----- 스폰 기준 -----")]
    [SerializeField] Transform _target;

    //[SerializeField] GameObject _enemyPrefab;
    // 게임오브젝트 자료형 대신 아예 필요한 컴포넌트 자료형으로 프리펩 변수 선언
    [SerializeField] Enemy[] _enemyPrefabs;

    [Header("----- 원형 스폰 범위 -----")]
    [SerializeField] float _minRadius;
    [SerializeField] float _maxRadius;

    [Header("----- 스폰 간격 -----")]
    [SerializeField] float _spawnSpan = 3.0f;

    public event UnityAction<int> OnKillCountChanged;    // 킬 수 변화 이벤트
    public event UnityAction<float> OnExpGained;         // 경험치 변화 이벤트
    public event UnityAction<float> OnRemainingTimeChanged; // 남은 시간 변화 이벤트

    int _killCount = 0;     // 킬 수
    EnemyStatData _enemyStatData;

    //float _spawnTimer;

    // 나중에 쓰려고 만들어놓기
    Coroutine _spawnEnemyRoutine;       // 적 생성 코루틴 변수
    WaveData _currentWaveData;          // 현재 웨이브 데이터
    float _playTime;                    // 스테이지 실제 플레이 시간

    public void Initialize(StageData stageData, Transform heroTransform, EnemyStatData enemyStatData)
    {
        _enemyStatData = enemyStatData;
        _target = heroTransform;

        _stageData = stageData;
        //_spawntimer = _spawnspan;
        OnKillCountChanged?.Invoke(_killCount); // 킬 수 변화 이벤트 초기화

        _spawnEnemyRoutine = StartCoroutine(SpawnEnemyRoutine());
    }

    /// <summary>
    /// 유니티 에디터에서 게임오브젝트 선택 시 스폰 영역 시각화(씬 뷰)
    /// 실제 게임에는 적용되지 않는다.
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        if (_target == null)
            return;

        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(_target.position, _minRadius);
        Gizmos.DrawWireSphere(_target.position, _maxRadius);
    }

    private void Update()
    {
        _playTime += Time.deltaTime;
        float remainingTime = _stageData.PlayTime - _playTime;

        // 스테이지가 끝났으면
        if (remainingTime < 0)
        {
            if (_spawnEnemyRoutine != null)
            {
                // 코루틴 정지(코루틴 변수로 받아서 사용)
                StopCoroutine(_spawnEnemyRoutine);
            }
        }
        else
        {
            // 남은 시간 변화 이벤트 발행
            OnRemainingTimeChanged?.Invoke(remainingTime);
        }
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            _currentWaveData = _stageData.GetWaveData(_playTime);
            _spawnSpan = _currentWaveData.SpawnSpan;

            yield return new WaitForSeconds(_spawnSpan);
            SpawnEnemy();
        }
    }

    /// <summary>
    /// 적을 생성하는 함수
    /// </summary>
    void SpawnEnemy()
    {
        // 확률에 따라 생성된 적의 순번을 가지는 변수
        int enemyIndex = _currentWaveData.GetRandomEnemyIndex();

        // 적 프리펩 복제하여 생성
        // 복제본 게임오브젝트의 Enemy 컴포넌트를 enemy 변수로 받은 것
        Enemy enemy = Instantiate(_enemyPrefabs[enemyIndex], transform);

        // ----- 원형 범위 내 위치 설정 ----- //
        Vector3 pos = _target.position;

        // 타겟(주인공) 위치로부터 떨어진 거리
        float dist = Random.Range(_minRadius, _maxRadius);

        // 타겟(주인공) 위치로부터 떨어져 있는 방향
        // 반지름이 1인 원 안의 임의의 지점으로 향하는 Vector2 반환
        Vector2 dir = Random.insideUnitCircle.normalized;

        pos.x += dir.x * dist;
        pos.y += dir.y * dist;
        
        enemy.transform.position = pos;
        // ----- 원형 범위 내 위치 설정 ----- //

        // 적 사망 이벤트 구독
        enemy.OnDeathEvent += OnEnemyDeath;

        enemy.Initialize(_target, _currentWaveData, _enemyStatData);
    }

    /// <summary>
    /// 적 캐릭터가 죽었을 때 자동으로 호출되는 함수
    /// </summary>
    /// <param name="expReward">보상 경험치</param>
    void OnEnemyDeath(float expReward)
    {
        // 적 한명 죽었으므로 킬 수 증가
        _killCount++;

        // 킬 수 변화 이벤트 발행
        OnKillCountChanged?.Invoke(_killCount);

        // 경험치 변화 이벤트 발행
        OnExpGained?.Invoke(expReward);
    }
}
