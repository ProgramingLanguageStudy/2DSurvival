using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventStudy : MonoBehaviour
{
    // 이벤트 - 함수를 변수처럼 저장
    // (관련 키워드 - 델리게이트, 대리자, delegate)

    // UnityEvent: 인스펙터뷰에서 함수 연결 가능
    public UnityEvent OnAKeyDown;   // A 키가 눌렸을 때

    // UnityAction: 코드로 함수 연결
    // 함수들을 변수처럼 저장해 놓고 원하는 때에 실행할 수 있는 기능
    public UnityAction OnSKeyDown;  // S 키가 눌렸을 때

    private void Start()
    {
        // OnSKeyDown 이벤트를 SkeyDown() 함수가 구독
        OnSKeyDown += SKeyDown;

        // 구독 해제
        OnSKeyDown -= SKeyDown;

        // 모든 구독 전면 해제
        OnSKeyDown = null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            // 이벤트를 활용할 때는 null 체크를 해 줘야 한다.
            if(OnAKeyDown != null)
            {
                // 이벤트에 연결되어 있는 함수들을 전부 실행시키는 코드
                OnAKeyDown.Invoke();
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            // ?: null이 아니면 실행하고 null이면 넘어간다.
            // null 체크를 대체
            OnSKeyDown?.Invoke();
        }
    }

    // A 키가 눌렸을 때 실행되는 함수
    public void AKeyDown()
    {
        Debug.Log("A키 누름!");
    }

    // S 키가 눌렸을 때 실행되는 함수
    void SKeyDown()
    {
        Debug.Log("S키 누름!");
    }

}
