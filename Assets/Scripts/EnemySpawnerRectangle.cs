using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
public class EnemySpawnerRectangle : MonoBehaviour
{
    [Header("----- 스폰 기준 -----")]
    [SerializeField] Transform _target;
    //[SerializeField] GameObject _enemyPrefab;
    // 게임오브젝트 자료형 대신 아예 필요한 컴포넌트 자료형으로 프리펩 변수 선언
    [SerializeField] Enemy _enemyPrefab;

    [Header("----- 사각형 스폰 범위 -----")]
    [SerializeField] float _minXDistance;
    [SerializeField] float _maxXDistance;
    [SerializeField] float _minYDistance;
    [SerializeField] float _maxYDistance;

    [Header("----- 스폰 간격 -----")]
    [SerializeField] float _spawnSpan = 3.0f;

    //float _spawnTimer;
    Coroutine _spawnEnemyRoutine;

    public void Initialize()
    {
        //_spawntimer = _spawnspan;

        _spawnEnemyRoutine = StartCoroutine(SpawnEnemyRoutine());
    }

    /// <summary>
    /// 유니티 에디터에서 게임오브젝트 선택 시 스폰 영역 시각화(씬 뷰)
    /// 실제 게임에는 적용되지 않는다.
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Vector3 minSize = new Vector3(_minXDistance * 2, _minYDistance * 2, 0);
        Vector3 maxSize = new Vector3(_maxXDistance * 2, _maxYDistance * 2, 0);

        // center를 중심으로 하는 육면체를 그리는 함수
        // size 벡터의 x값만큼 좌우 크기
        // size 벡터의 y값만큼 상하 크기
        // size 벡터의 z값만큼 앞뒤 크기
        Gizmos.DrawWireCube(_target.position, minSize);
        Gizmos.DrawWireCube(_target.position, maxSize);

    }

    //private void Update()
    //{
    //    if (_spawnTimer > 0)
    //    {
    //        _spawnTimer -= Time.deltaTime;
    //        if (_spawnTimer <= 0f)
    //        {
    //            _spawnTimer = _spawnSpan;
    //            SpawnEnemy();
    //        }
    //    }
    //}

    IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnSpan);
            SpawnEnemy();
        }
    }

    /// <summary>
    /// 적을 생성하는 함수
    /// </summary>
    void SpawnEnemy()
    {
        // 적 프리펩 복제하여 생성
        // 복제본 게임오브젝트의 Enemy 컴포넌트를 enemy 변수로 받은 것
        Enemy enemy = Instantiate(_enemyPrefab, transform);

        // ----- 사각형 범위 내 위치 설정 ----- //
        Vector3 pos = _target.position;

        float xDist;
        float yDist;
        if (Random.value < 0.5f)
        {
            xDist = Random.Range(_minXDistance, _maxXDistance);
            yDist = Random.Range(0, _maxYDistance);
        }
        else
        {
            xDist = Random.Range(0, _maxXDistance);
            yDist = Random.Range(_minYDistance, _maxYDistance);
        }

        // 50% 확률로 곱하기 -1
        if(Random.value < 0.5f)
        {
            xDist *= -1;
        }
        // 50% 확률로 곱하기 -1
        if (Random.value < 0.5f)
        {
            yDist *= -1;
        }
        pos.x += xDist;
        pos.y += yDist;

        // 계산된 위치를 지금 생성한 적 캐릭터의 위치로 설정
        enemy.transform.position = pos;
        // ----- 사각형 범위 내 위치 설정 ----- //

        enemy.Initialize(_target);
    }
}
