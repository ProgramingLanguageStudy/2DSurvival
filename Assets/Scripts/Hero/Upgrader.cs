using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 업그레이드 선택 시스템을 관리하는 클래스
/// </summary>
public class Upgrader : MonoBehaviour
{
    IUpgradable[] _upgradables;     // 전체 업그레이드 가능한 대상들

    // Awake() 함수는 Start(), Update()처럼 유니티에서
    // 자동으로 호출해 주는 함수
    // 호출 시점: Start() 이전, 이 객체가 갓 로드되었을 때
    // Start()보다 빠른 시점에 작동하는 함수
    // 인스펙터뷰 연결결 대신, 컴포넌트 참조를 코드로 처리할 때 유용
    private void Awake()
    {
        // 자식 게임오브제긑들로부터 모든 IUpgradable을
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
}
